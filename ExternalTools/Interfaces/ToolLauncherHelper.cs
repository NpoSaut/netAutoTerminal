using System.Diagnostics;

namespace ExternalTools.Interfaces
{
    public static class ToolLauncherHelper
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="Launcher">�������</param>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>���������� � �������� ���������� �������</returns>
        public static Process Execute(this IToolLauncher Launcher, IToolBody ToolBody, params ILaunchParameter[] Parameters)
        {
            return Launcher.Execute(ToolBody, Parameters);
        }
    }
}
