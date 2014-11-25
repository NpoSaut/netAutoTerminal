using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Attributes;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика, изготавливающая рецепты прошивки типа ImageToTool (<see cref="BurningReceipt{TImage}" />)</summary>
    /// <typeparam name="TImage">Тип создаваемого образа</typeparam>
    public abstract class BurningReceiptFactoryBase<TImage> : IBurningReceiptFactory where TImage : IImage
    {
        /// <summary>Имя изготавливаемого рецепта</summary>
        public string ReceiptName
        {
            get { return GetType().GetCustomAttributes(typeof (BurningReceiptFactoryAttribute), false).OfType<BurningReceiptFactoryAttribute>().First().Name; }
        }

        /// <summary>Типы устройств, для которых может использоваться этот рецепт</summary>
        public IEnumerable<string> TargetDevices
        {
            get { return GetType().GetCustomAttributes(typeof (TargetDeviceAttribute), false).OfType<TargetDeviceAttribute>().Select(a => a.DeviceName); }
        }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public abstract IBurningReceipt GetReceipt(string DeviceName);
    }
}
