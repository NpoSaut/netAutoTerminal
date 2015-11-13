﻿using AsyncOperations.Progress;
using ExternalTools;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.OpenOcd;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Receipts.Cortex.Tools;

namespace FirmwareBurner.Receipts.Cortex.BurnerFacades
{
    public class CortexOverOpenOcdToolFacade : IBurningToolFacade<CortexImage>
    {
        private readonly IOcdBurningParametersProvider _ocdBurningParametersProvider;
        private readonly OpenOcdToolFactory _openOcdToolFactory;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public CortexOverOpenOcdToolFacade(OpenOcdToolFactory OpenOcdToolFactory, IOcdBurningParametersProvider OcdBurningParametersProvider,
                                           IProgressControllerFactory ProgressControllerFactory)
        {
            _openOcdToolFactory = OpenOcdToolFactory;
            _ocdBurningParametersProvider = OcdBurningParametersProvider;
            _progressControllerFactory = ProgressControllerFactory;
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="Target">Цель прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        public void Burn(CortexImage Image, TargetInformation Target, IProgressToken ProgressToken)
        {
            using (IProgressController progress = _progressControllerFactory.CreateController(ProgressToken))
            {
                OpenOcdTool openOcd = _openOcdToolFactory.GetBurningTool();
                //openOcd.InitializeProgrammer();

                var hexStream = new IntelHexStream();
                Image.FlashBuffer.CopyTo(hexStream);
                IntelHexFile hexFile = hexStream.GetHexFile();
                using (var file = new TemporaryFile(hexFile.OpenIntelHexStream()))
                {
                    openOcd.Burn(_ocdBurningParametersProvider.GetBoardName(Target),
                                 _ocdBurningParametersProvider.GetTargetName(Target),
                                 file.FileInfo.FullName);
                }
            }
        }
    }
}
