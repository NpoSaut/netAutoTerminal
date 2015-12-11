using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.Imaging
{
    public class ImageFormattersProvider<TImage> : IImageFormatterFactoryProvider<TImage> where TImage : IImage
    {
        private readonly ICollection<IImageFormattersCatalog<TImage>> _formattersCatalogs;
        private readonly IIndexHelper _indexHelper;

        public ImageFormattersProvider(IImageFormattersCatalog<TImage>[] FormattersCatalogs, IIndexHelper IndexHelper)
        {
            _formattersCatalogs = FormattersCatalogs;
            _indexHelper = IndexHelper;
        }

        public IImageFormatterFactory<TImage> GetFormatterFactory(string DeviceName, int Cell, int Modification, IList<BootloaderRequirement> Requirements)
        {
            // Получаем список всех загрузчиков, с которыми можем поработать
            List<IImageFormatterFactory<TImage>> appropriatedFormatterFactories =
                _formattersCatalogs.SelectMany(catalog => catalog.GetAppropriateFormatterFactories(DeviceName, Requirements))
                                   .ToList();

            // Если он один -- то его и возвращаем
            if (appropriatedFormatterFactories.Count == 1)
                return appropriatedFormatterFactories.Single();

            // Если вариантов несколько -- ищем тот, который совпадает с дефолтным загрузчиком
            // И выбираем самую свежую версию
            ModificationKind modification = _indexHelper.GetModification(Cell, Modification);
            int defaultBootloaderId = int.Parse(modification.CustomProperties["default-bootloader"]);
            IImageFormatterFactory<TImage> defaultFormatterFactory = appropriatedFormatterFactories
                .Where(catalog => catalog.Information.BootloaderApi.BootloaderId == defaultBootloaderId)
                .OrderByDescending(catalog => catalog.Information.BootloaderApi.BootloaderVersion)
                .FirstOrDefault();

            if (defaultFormatterFactory != null)
                return defaultFormatterFactory;

            // Если ничего не нашлось -- то говорим, что не умеем с таким работать
            throw new ImageFormatterNotFoundException();
        }
    }
}
