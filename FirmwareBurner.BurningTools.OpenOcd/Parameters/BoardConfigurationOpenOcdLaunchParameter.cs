using System;

namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>��������, ����������� �� ���������������� ���� Board-�</summary>
    [ParameterKind(OpenOcdParameterKind.ConfigurationFile)]
    internal class BoardConfigurationOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        private readonly string _boardName;
        public BoardConfigurationOpenOcdLaunchParameter(string BoardName) { _boardName = BoardName; }

        /// <summary>���������� ���������� ���������</summary>
        protected override string GetParameterContent() { return String.Format("/board/{0}.cfg", _boardName); }
    }
}