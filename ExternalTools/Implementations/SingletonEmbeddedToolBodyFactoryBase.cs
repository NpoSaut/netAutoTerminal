using System;
using System.Reflection;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    /// <summary>��������� ������ ��������� ���� <see cref="EmbeddedToolBody" />
    /// </summary>
    public class SingletonEmbeddedToolBodyFactoryBase : IToolBodyFactory, IDisposable
    {
        private readonly Lazy<IToolBody> _toolBody;

        /// <summary>������ ���������-������� ��� ������������ <see cref="EmbeddedToolBody" />, ����������� �� ����-�������</summary>
        /// <param name="ExecutableFileName">������������� ���� � ������������ ����� �������</param>
        /// <param name="MarkerType">
        ///     ��� �� ������, ���������� ������� � ����� ������� � ������� � ������������ � �����
        ///     ������������ ��� � ��������� �������
        /// </param>
        public SingletonEmbeddedToolBodyFactoryBase(Type MarkerType, string ExecutableFileName)
            : this(MarkerType, MarkerType.Namespace, ExecutableFileName) { }

        /// <summary>������ ���������-������� ��� ������������ <see cref="EmbeddedToolBody" /></summary>
        /// <param name="BodyNamespacePath">���� � � ������� �����, ���������� �������</param>
        /// <param name="ExecutableFileName">������������� ���� � ������������ ����� �������</param>
        /// <param name="MarkerType">��� �� ������, ���������� ������� � ����� �������</param>
        public SingletonEmbeddedToolBodyFactoryBase(Type MarkerType, string BodyNamespacePath, string ExecutableFileName)
            : this(Assembly.GetAssembly(MarkerType), BodyNamespacePath, ExecutableFileName) { }

        /// <summary>������ ���������-������� ��� ������������ <see cref="EmbeddedToolBody" /></summary>
        /// <param name="ContainingAssembly">������, ���������� ������� � ����� �������</param>
        /// <param name="BodyNamespacePath">���� � � ������� �����, ���������� �������</param>
        /// <param name="ExecutableFileName">������������� ���� � ������������ ����� �������</param>
        public SingletonEmbeddedToolBodyFactoryBase(Assembly ContainingAssembly, string BodyNamespacePath, string ExecutableFileName)
        {
            _toolBody = new Lazy<IToolBody>(() => new EmbeddedToolBody(ContainingAssembly, BodyNamespacePath, ExecutableFileName));
        }

        public void Dispose() { if (_toolBody.IsValueCreated) _toolBody.Value.Dispose(); }

        public IToolBody GetToolBody() { return _toolBody.Value; }
    }
}
