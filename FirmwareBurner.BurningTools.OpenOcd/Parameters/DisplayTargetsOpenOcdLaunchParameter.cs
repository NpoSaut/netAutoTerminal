namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>Выводит список целей и активную цель</summary>
    [ParameterKind(OpenOcdParameterKind.Command)]
    internal class DisplayTargetsOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        /// <summary>Возвращает содержимое параметра</summary>
        protected override string GetParameterContent() { return "targets"; }
    }
}
