using System.IO;

namespace ExternalTools.Interfaces
{
    public static class ToolLauncherHelper
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="Launcher">�������</param>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        public static StreamReader Execute(this IToolLauncher Launcher, IToolBody ToolBody, params ILaunchParameter[] Parameters)
        {
            return Launcher.Execute(ToolBody, Parameters);
        }
    }
}