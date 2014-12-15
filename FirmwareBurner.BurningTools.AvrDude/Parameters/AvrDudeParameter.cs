using System;
using System.Linq;
using ExternalTools.Interfaces;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    /// <summary>Параметр для AvrDude</summary>
    public abstract class AvrDudeParameter : ILaunchParameter
    {
        /// <summary>Значение параметра</summary>
        protected abstract String Value { get; }

        /// <summary>Имя параметра</summary>
        private char Key
        {
            get { return GetType().GetCustomAttributes(typeof (ParameterKeyAttribute), true).OfType<ParameterKeyAttribute>().First().Name; }
        }

        /// <summary>Получает строковое представление параметра, которое можно подставить в строку запуска утилиты</summary>
        public string GetStringPresentation() { return String.Format("-{0}{1}", Key, Value); }
    }
}
