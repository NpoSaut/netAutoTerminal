using System;
using FirmwareBurner.BurningTools.Stk500.Launching;

namespace FirmwareBurner.BurningTools.Stk500.Stk500Body
{
    /// <summary>Отложенно создаёт синглетон типа <see cref="ResourcedToolBody" />
    /// </summary>
    public class SingletonResourceToolBodyFactory : IToolBodyFactory, IDisposable
    {
        private readonly Lazy<IToolBody> _toolBody;

        public SingletonResourceToolBodyFactory() { _toolBody = new Lazy<IToolBody>(() => new ResourcedToolBody()); }

        /// <summary>
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых
        ///     ресурсов.
        /// </summary>
        public void Dispose() { if (_toolBody.IsValueCreated) _toolBody.Value.Dispose(); }

        public IToolBody GetToolBody() { return _toolBody.Value; }
    }
}
