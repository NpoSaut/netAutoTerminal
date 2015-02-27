using System.Collections.Generic;
using System.Diagnostics;

namespace ExternalTools.Interfaces
{
    /// <summary>������� �������������</summary>
    public interface IToolLauncher
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>���������� � �������� ���������� �������</returns>
        Process Execute(IToolBody ToolBody, IEnumerable<ILaunchParameter> Parameters);
    }
}
