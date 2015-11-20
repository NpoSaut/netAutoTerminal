using System;
using System.Collections.Generic;
using System.Threading;
using AsyncOperations.Progress;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    public class EmulationBurningReceiptsCatalog : IBurningReceiptsCatalog
    {
        /// <summary>Находит рецепты, применимые для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства для прошивания</param>
        /// <returns>Фабрики для изготовления нужных <see cref="IBurningReceipt" /></returns>
        public IEnumerable<IBurningReceiptFactory> GetBurningReceiptFactories(string DeviceName)
        {
            yield return new EmulationBurningReceiptFactory("Эмулировать прошивку");
        }

        private class EmulationBurningReceipt : IBurningReceipt
        {
            /// <summary>Название рецепта</summary>
            public string Name
            {
                get { return "Эмулировать прошивку"; }
            }

            public void Burn(FirmwareProject Project, IProgressToken Progress)
            {
                var r = new Random();
                Progress.Start();
                for (int i = 0; i < 50; i++)
                {
                    if (r.Next(1000) < 0.7 * 1000 / 50)
                        throw new BurningException(new Exception("Сработала эмуляция ошибки при прошивании"));
                    Progress.SetProgress(i / 50.0);
                    Thread.Sleep(50);
                }
                Progress.OnCompleated();
            }
        }

        private class EmulationBurningReceiptFactory : IBurningReceiptFactory
        {
            public EmulationBurningReceiptFactory(string ReceiptName) { this.ReceiptName = ReceiptName; }

            /// <summary>Имя изготавливаемого рецепта</summary>
            public string ReceiptName { get; private set; }

            /// <summary>Типы устройств, для которых может использоваться этот рецепт</summary>
            public IEnumerable<string> TargetDevices { get; private set; }

            /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
            /// <param name="DeviceName">Название типа прошиваемого устройства</param>
            public IBurningReceipt GetReceipt(string DeviceName)
            {
                return new EmulationBurningReceipt();
            }
        }
    }
}
