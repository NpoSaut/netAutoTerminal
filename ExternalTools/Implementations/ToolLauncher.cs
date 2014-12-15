using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    public class ToolLauncher : IToolLauncher
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        public StreamReader Execute(IToolBody ToolBody, IEnumerable<ILaunchParameter> Parameters)
        {
            var processStartInfo =
                new ProcessStartInfo(ToolBody.ExecutableFilePath, string.Join(" ", Parameters.Select(prm => prm.GetStringPresentation())))
                {
                    WorkingDirectory = ToolBody.WorkingDirectoryPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
            var p = new Process { StartInfo = processStartInfo };
            p.Start();
            return p.StandardOutput;
        }
    }
}
