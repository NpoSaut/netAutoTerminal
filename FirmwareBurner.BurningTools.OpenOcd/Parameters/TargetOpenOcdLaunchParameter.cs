namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>Атрибут выбора цели прошивки</summary>
    [ParameterKind(OpenOcdParameterKind.Command)]
    internal class TargetOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        private readonly string _targetName;
        public TargetOpenOcdLaunchParameter(string TargetName) { _targetName = TargetName; }

        /// <summary>Возвращает содержимое параметра</summary>
        protected override string GetParameterContent() { return string.Format("\"targets {0}.cpu\"", _targetName); }
    }
}
