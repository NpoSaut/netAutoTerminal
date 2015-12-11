﻿using System;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Imaging;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    /// <summary>Менеджер зашивки образа</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public class BurningReceipt<TImage> : IBurningReceipt where TImage : IImage
    {
        private readonly IBurningToolFacade<TImage> _burningToolFacade;
        private readonly string _deviceName;
        private readonly IImageFormatterFactoryProvider<TImage> _formatterFactoryProvider;

        public BurningReceipt(string Name, string DeviceName, IImageFormatterFactoryProvider<TImage> FormatterFactoryProvider,
                              IBurningToolFacade<TImage> BurningToolFacade)
        {
            _deviceName = DeviceName;
            _formatterFactoryProvider = FormatterFactoryProvider;
            _burningToolFacade = BurningToolFacade;
            this.Name = Name;
        }

        /// <summary>Название рецепта</summary>
        public string Name { get; private set; }

        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        /// <param name="Progress">Токен для отчёта о процессе прошивки</param>
        public void Burn(FirmwareProject Project, IProgressToken Progress)
        {
            var imageProgress = new SubprocessProgressToken(0.1);
            var burnProgress = new SubprocessProgressToken();

            using (new CompositeProgressManager(Progress, imageProgress, burnProgress))
            {
                TImage image;
                try
                {
                    IImageFormatter<TImage> formatter =
                        _formatterFactoryProvider.GetFormatterFactory(_deviceName, Project.Target.CellId, Project.Target.ModificationId,
                                                                      Project.Modules.Select(m => m.FirmwareContent.BootloaderRequirement).ToList())
                                                 .GetFormatter();
                    image = formatter.GetImage(Project, imageProgress);
                }
                catch (Exception e)
                {
                    throw new CreateImageException(e);
                }
                try
                {
                    _burningToolFacade.Burn(image, Project.Target, burnProgress);
                }
                catch (Exception e)
                {
                    throw new BurningException(e);
                }
            }
        }
    }
}
