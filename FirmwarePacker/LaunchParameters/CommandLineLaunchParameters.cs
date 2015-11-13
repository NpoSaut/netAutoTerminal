using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacker.LaunchParameters
{
    internal class CommandLineLaunchParameters : ILaunchParameters
    {
        private readonly string _outputFileName;
        private readonly Dictionary<string, string> _parameters;
        private readonly string _projectFileName;

        public CommandLineLaunchParameters(IList<string> Arguments)
        {
            if (Arguments.Count > 0 && !Arguments[0].StartsWith("-"))
                _projectFileName = Arguments[0];
            if (Arguments.Count > 1 && !Arguments[1].StartsWith("-"))
                _outputFileName = Arguments[1];
            _parameters = Arguments.Where(p => p.StartsWith("-"))
                                   .Select(p => p.Split('='))
                                   .ToDictionary(pa => pa[0].Substring(1), pa => pa.Length > 1 ? pa[1] : null);
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
        }

        public bool SilentMode
        {
            get { return _parameters.ContainsKey("silent"); }
        }

        public string ProjectFileName
        {
            get { return _projectFileName; }
        }

        public int? VersionMajor
        {
            get { return GetNullableInt("version-major"); }
        }

        public int? VersionMinor
        {
            get { return GetNullableInt("version-minor"); }
        }

        public string VersionLabel
        {
            get { return GetNullableString("version-label"); }
        }

        public DateTime? ReleaseDate
        {
            get { return GetNullableDate("release-date"); }
        }

        private DateTime? GetNullableDate(string PropertyKey)
        {
            string value;
            if (!_parameters.TryGetValue(PropertyKey, out value))
                return null;
            if (value == "today")
                return DateTime.Now;
            DateTime result;
            if (!DateTime.TryParse(value, out result))
                return null;
            return result;
        }

        private string GetNullableString(string PropertyKey)
        {
            string value;
            return _parameters.TryGetValue(PropertyKey, out value) ? value : null;
        }

        private int? GetNullableInt(string PropertyKey)
        {
            string value;
            if (!_parameters.TryGetValue(PropertyKey, out value))
                return null;
            int result;
            if (!Int32.TryParse(value, out result))
                return null;
            return result;
        }
    }
}
