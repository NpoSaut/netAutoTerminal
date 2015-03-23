using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace FirmwareBurner.Settings
{
    /// <summary>Сервис настроек, хранящий настройки в <see cref="FirmwareBurner.Properties" />
    /// </summary>
    public class PropertiesSettingsService : ISettingsService
    {
        private readonly IDictionary<int, string> _preferredBurningMethod;

        public PropertiesSettingsService()
        {
            _preferredBurningMethod = Properties.Settings.Default.PreferredBurningMethods == null
                                          ? new Dictionary<int, string>()
                                          : Properties.Settings.Default.PreferredBurningMethods
                                                      .OfType<String>()
                                                      .Select(line => line.Split(':').Select(x => x.Trim()).ToList())
                                                      .ToDictionary(ll => int.Parse(ll[0]),
                                                                    ll => ll[1]);
        }

        /// <summary>Находит имя предопочитаемого метода прошивки для указанного типа ячейки</summary>
        /// <param name="CellId">Тип ячейки</param>
        /// <returns>Имя предпочитаемого метода прошивки</returns>
        public string GetPreferredBurningMethod(int CellId)
        {
            string value;
            return _preferredBurningMethod.TryGetValue(CellId, out value) ? value : null;
        }

        /// <summary>Задаёт предопочитаемого метода прошивки для указанного типа ячейки</summary>
        /// <param name="CellId">Тип ячейки</param>
        /// <param name="MethodName">Имя предпочитаемого метода прошивки</param>
        public void SetPreferredBurningMethod(int CellId, string MethodName)
        {
            if (_preferredBurningMethod.ContainsKey(CellId))
                _preferredBurningMethod[CellId] = MethodName;
            else
                _preferredBurningMethod.Add(CellId, MethodName);
        }

        /// <summary>Сохраняет настройки приложения</summary>
        public void Save()
        {
            (Properties.Settings.Default.PreferredBurningMethods = new StringCollection())
                .AddRange(_preferredBurningMethod.Select(s => string.Format("{0}: {1}", s.Key, s.Value)).ToArray());
            Properties.Settings.Default.Save();
        }
    }
}
