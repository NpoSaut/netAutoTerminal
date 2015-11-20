namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>Параметр, сигнализирующий о том, что OpenOCD должна закрыться после совершения операции</summary>
    [ParameterKind(OpenOcdParameterKind.Command)]
    internal class ShutdownOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        /// <summary>Возвращает содержимое параметра</summary>
        protected override string GetParameterContent() { return "shutdown"; }
    }
}