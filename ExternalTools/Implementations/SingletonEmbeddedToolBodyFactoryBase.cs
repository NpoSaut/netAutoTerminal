using System;
using System.Reflection;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    /// <summary>Отложенно создаёт синглетон типа <see cref="EmbeddedToolBody" />
    /// </summary>
    public class SingletonEmbeddedToolBodyFactoryBase : IToolBodyFactory, IDisposable
    {
        private readonly Lazy<IToolBody> _toolBody;

        /// <summary>Создаёт синглетон-фабрику для производства <see cref="EmbeddedToolBody" />, основываясь на типе-маркере</summary>
        /// <param name="ExecutableFileName">Относительный путь к выполняемому файлу утилиты</param>
        /// <param name="MarkerType">
        ///     Тип из сборки, содержащей ресурсы с телом утилиты и лежащий в пространстве в одном
        ///     пространстве имён с ресурсами утилиты
        /// </param>
        public SingletonEmbeddedToolBodyFactoryBase(Type MarkerType, string ExecutableFileName)
            : this(MarkerType, MarkerType.Namespace, ExecutableFileName) { }

        /// <summary>Создаёт синглетон-фабрику для производства <see cref="EmbeddedToolBody" /></summary>
        /// <param name="BodyNamespacePath">Путь к к ресурсу папки, содержащей утилиту</param>
        /// <param name="ExecutableFileName">Относительный путь к выполняемому файлу утилиты</param>
        /// <param name="MarkerType">Тип из сборки, содержащей ресурсы с телом утилиты</param>
        public SingletonEmbeddedToolBodyFactoryBase(Type MarkerType, string BodyNamespacePath, string ExecutableFileName)
            : this(Assembly.GetAssembly(MarkerType), BodyNamespacePath, ExecutableFileName) { }

        /// <summary>Создаёт синглетон-фабрику для производства <see cref="EmbeddedToolBody" /></summary>
        /// <param name="ContainingAssembly">Сборка, включающая ресурсы с телом утилиты</param>
        /// <param name="BodyNamespacePath">Путь к к ресурсу папки, содержащей утилиту</param>
        /// <param name="ExecutableFileName">Относительный путь к выполняемому файлу утилиты</param>
        public SingletonEmbeddedToolBodyFactoryBase(Assembly ContainingAssembly, string BodyNamespacePath, string ExecutableFileName)
        {
            _toolBody = new Lazy<IToolBody>(() => new EmbeddedToolBody(ContainingAssembly, BodyNamespacePath, ExecutableFileName));
        }

        public void Dispose() { if (_toolBody.IsValueCreated) _toolBody.Value.Dispose(); }

        public IToolBody GetToolBody() { return _toolBody.Value; }
    }
}
