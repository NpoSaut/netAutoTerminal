using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    public class ToolLauncher : IToolLauncher
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        public StreamReader Execute(IToolBody ToolBody, IEnumerable<Stk500Parameter> Parameters)
        {
            var processStartInfo =
                new ProcessStartInfo(ToolBody.ExecutableFilePath, string.Join(" ", Parameters.Where(prm => prm != null).Select(prm => prm.Get())))
                {
                    WorkingDirectory = ToolBody.WorkingDirectoryPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
            var p = new Process { StartInfo = processStartInfo };
            p.Start();
            return p.StandardOutput;
        }
    }
}