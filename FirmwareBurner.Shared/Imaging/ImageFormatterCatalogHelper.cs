using Microsoft.Practices.Unity;

namespace FirmwareBurner.Imaging
{
    public static class ImageFormatterCatalogHelper
    {
        public static IUnityContainer RegisterImageFormattersCatalog<TImage, TCatalog>(this IUnityContainer Container)
            where TCatalog : IImageFormattersCatalog<TImage>
            where TImage : IImage
        {
            return Container.RegisterType<IImageFormattersCatalog<TImage>, TCatalog>(typeof (TCatalog).FullName, new ContainerControlledLifetimeManager());
        }
    }
}
