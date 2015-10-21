using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExternalTools.Interfaces;

namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    internal enum OpenOcdParameterKind
    {
        Help,
        Version,
        ConfigurationFile,
        Search,
        Debug,
        Log,
        Command
    }

    /// <summary>Параметр программы OpenOCD</summary>
    internal abstract class OpenOcdLaunchParameter : ILaunchParameter
    {
        private static readonly IDictionary<OpenOcdParameterKind, char> _parameterKeys =
            new Dictionary<OpenOcdParameterKind, char>
            {
                { OpenOcdParameterKind.Help, 'h' },
                { OpenOcdParameterKind.Version, 'v' },
                { OpenOcdParameterKind.ConfigurationFile, 'f' },
                { OpenOcdParameterKind.Search, 's' },
                { OpenOcdParameterKind.Debug, 'd' },
                { OpenOcdParameterKind.Log, 'l' },
                { OpenOcdParameterKind.Command, 'c' }
            };

        /// <summary>Получает строковое представление параметра, которое можно подставить в строку запуска утилиты</summary>
        public string GetStringPresentation()
        {
            OpenOcdParameterKind parameterKind =
                GetType().GetCustomAttributes(typeof (ParameterKindAttribute), true).OfType<ParameterKindAttribute>().First().ParameterKind;
            return string.Format("-{0} \"{1}\"", _parameterKeys[parameterKind], GetParameterContent());
        }

        /// <summary>Возвращает содержимое параметра</summary>
        protected abstract String GetParameterContent();

        protected string ProcessFilePath(string LocalPath) { return LocalPath.Replace(Path.DirectorySeparatorChar, '/'); }
    }
}
