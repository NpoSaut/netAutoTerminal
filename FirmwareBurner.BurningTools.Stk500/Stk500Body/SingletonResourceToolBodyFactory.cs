using System;
using FirmwareBurner.BurningTools.Stk500.Launching;

namespace FirmwareBurner.BurningTools.Stk500.Stk500Body
{
    /// <summary>��������� ������ ��������� ���� <see cref="ResourcedToolBody" />
    /// </summary>
    public class SingletonResourceToolBodyFactory : IToolBodyFactory, IDisposable
    {
        private readonly Lazy<IToolBody> _toolBody;

        public SingletonResourceToolBodyFactory() { _toolBody = new Lazy<IToolBody>(() => new ResourcedToolBody()); }

        /// <summary>
        ///     ��������� ������������ ����������� ������, ��������� � ���������, �������������� ��� ������� �������������
        ///     ��������.
        /// </summary>
        public void Dispose() { if (_toolBody.IsValueCreated) _toolBody.Value.Dispose(); }

        public IToolBody GetToolBody() { return _toolBody.Value; }
    }
}
