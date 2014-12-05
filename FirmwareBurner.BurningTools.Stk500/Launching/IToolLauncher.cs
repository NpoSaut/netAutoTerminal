using System.Collections.Generic;
using System.IO;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    /// <summary>������� �������������</summary>
    public interface IToolLauncher
    {
        /// <summary>��������� ������������ � ���������� ����������� � ���������� ����� ������ �� �������</summary>
        /// <param name="ToolBody">����� �������� ������ �������������</param>
        /// <param name="Parameters">��������� ��� ������� �������������</param>
        /// <returns>�����, ������� ������������ ������� �� �������</returns>
        StreamReader Execute(IToolBody ToolBody, IEnumerable<Stk500Parameter> Parameters);
    }
}