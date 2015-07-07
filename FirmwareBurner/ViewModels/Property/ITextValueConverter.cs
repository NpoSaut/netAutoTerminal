using System.Collections.Generic;

namespace FirmwareBurner.ViewModels.Property
{
    /// <summary>��������� �� ������ � ������</summary>
    /// <typeparam name="TValue">��� ������� ��� ���������</typeparam>
    public interface ITextValueConverter<TValue>
    {
        /// <summary>������������ ������ � ������</summary>
        /// <param name="Text">��������� ��������</param>
        /// <param name="Value">�������� ��������</param>
        /// <returns>������ ������ �����������</returns>
        IEnumerable<string> TryConvert(string Text, out TValue Value);

        /// <summary>�������� ��������� ������������� �������</summary>
        /// <param name="Value">�������� �������</param>
        /// <returns>��������� �������������</returns>
        string GetText(TValue Value);
    }
}
