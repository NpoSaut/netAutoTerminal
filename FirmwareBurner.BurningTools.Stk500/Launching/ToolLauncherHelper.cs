using System.IO;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    internal static class ToolLauncherHelper
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="Launcher">�������</param>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        public static StreamReader Execute(this IToolLauncher Launcher, IToolBody ToolBody, params Stk500Parameter[] Parameters)
        {
            return Launcher.Execute(ToolBody, Parameters);
        }
    }
}