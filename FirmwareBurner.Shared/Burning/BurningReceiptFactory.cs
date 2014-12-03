using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Attributes;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Burning
{
    /// <summary>
    ///     Фабрика, изготавливающая рецепты прошивки типа ImageToTool (<see cref="BurningReceipt{TImage}" />) и
    ///     использующая конкретный тип фасада <typeparamref name="TBurningToolFacadeFactory" /> для записи образа
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         За счёт использования конкретного шаблонного параметра <typeparamref name="TBurningToolFacadeFactory" />
    ///         имеется возможность создать конкретный экземпляр фабрики рецептов для конкретного класса фасада инструмента
    ///         прошивки.
    ///     </para>
    ///     <para>
    ///         Это даёт возможность доставать этот инструмент из контейнера. При этом, используется инструмент создания
    ///         образа, зарегистрированный по-умолчанию
    ///     </para>
    ///     <para>
    ///         Рекомендуется использовать метод расширения
    ///         <see
    ///             cref="BurningReceiptFactoryHelper.RegisterBurningReceiptFactory{TImage,TBurningToolFacadeFactory}(Microsoft.Practices.Unity.IUnityContainer)" />
    ///         для регистрации фабрики рецептов
    ///     </para>
    /// </remarks>
    /// <typeparam name="TImage">Тип создаваемого образа</typeparam>
    /// <typeparam name="TBurningToolFacadeFactory">Конкретный тип фасада, используемый для записи образа</typeparam>
    public class BurningReceiptFactory<TImage, TBurningToolFacadeFactory> : IBurningReceiptFactory
        where TImage : IImage
        where TBurningToolFacadeFactory : class, IBurningToolFacadeFactory<TImage>
    {
        private readonly IImageFormatterFactory<TImage> _imageFormatterFactory;
        private readonly TBurningToolFacadeFactory _toolFacadeFactory;

        public BurningReceiptFactory(IImageFormatterFactory<TImage> ImageFormatterFactory, TBurningToolFacadeFactory ToolFacadeFactory)
        {
            _imageFormatterFactory = ImageFormatterFactory;
            _toolFacadeFactory = ToolFacadeFactory;
        }

        /// <summary>Имя изготавливаемого рецепта</summary>
        public string ReceiptName
        {
            get
            {
                return
                    typeof (TBurningToolFacadeFactory).GetCustomAttributes(typeof (BurningReceiptFactoryAttribute), false)
                                                      .OfType<BurningReceiptFactoryAttribute>()
                                                      .First()
                                                      .Name;
            }
        }

        /// <summary>Типы устройств, для которых может использоваться этот рецепт</summary>
        public IEnumerable<string> TargetDevices
        {
            get
            {
                return
                    typeof (TBurningToolFacadeFactory).GetCustomAttributes(typeof (TargetDeviceAttribute), false)
                                                      .OfType<TargetDeviceAttribute>()
                                                      .Select(a => a.DeviceName);
            }
        }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningReceipt GetReceipt(string DeviceName)
        {
            return new BurningReceipt<TImage>(ReceiptName,
                                              _imageFormatterFactory.GetFormatter(DeviceName),
                                              _toolFacadeFactory.GetBurningToolFacade(DeviceName));
        }
    }
}
