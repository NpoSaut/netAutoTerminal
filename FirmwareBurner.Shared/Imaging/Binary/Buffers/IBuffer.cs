using FirmwareBurner.Annotations;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    /// <summary>Буфер бинарных данных</summary>
    public interface IBuffer
    {
        /// <summary>Записывает массив байт в указанное место буфера</summary>
        /// <param name="Position">Адрес, по которому следует разместить первый байт</param>
        /// <param name="Bytes">Данные для записи</param>
        void Write(int Position, [NotNull] params byte[] Bytes);
    }
}
