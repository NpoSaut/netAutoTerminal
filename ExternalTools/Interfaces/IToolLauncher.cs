using System.Collections.Generic;
using System.IO;

namespace ExternalTools.Interfaces
{
    /// <summary>������� �������������</summary>
    public interface IToolLauncher
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        StreamReader Execute(IToolBody ToolBody, IEnumerable<ILaunchParameter> Parameters);
    }
}
