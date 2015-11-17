using System;

namespace FirmwarePacker.RecentProjects
{
    [Serializable]
    public class RecentProject
    {
        public string FileName { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public override string ToString() { return string.Format("{0} (ver. {1}.{2})", FileName, MajorVersion, MinorVersion); }
    }
}
