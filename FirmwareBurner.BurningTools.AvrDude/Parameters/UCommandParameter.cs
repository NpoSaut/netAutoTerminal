using System;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    /// <summary>Тип памяти AVRDude</summary>
    public enum AvrDudeMemoryType
    {
        /// <summary>Flash-память</summary>
        [Pseudoname("flash")]
        Flash,

        /// <summary>EEPROM</summary>
        [Pseudoname("eeprom")]
        Eeprom,

        /// <summary>Старший конфигурационный байт</summary>
        [Pseudoname("hfuse")]
        HFuse,

        /// <summary>Младший конфигурационный байт</summary>
        [Pseudoname("lfuse")]
        LFuse,

        /// <summary>Дополнительный конфигурационный байт</summary>
        [Pseudoname("efuse")]
        EFuse
    }

    /// <summary>Тип операции с памятью</summary>
    public enum AvrDudeMemoryOperationType
    {
        /// <summary>Чтение памяти из микроконтроллера и запись в файл</summary>
        [Pseudoname("r")]
        Read,

        /// <summary>Чтение прошивки из файла и запись в память микроконтроллера</summary>
        [Pseudoname("w")]
        Write,

        /// <summary>Чтение памяти из микроконтроллера и сравнение её с прошивкой</summary>
        [Pseudoname("v")]
        Verify
    }

    public enum AvrDudeInputFormat
    {
        /// <summary>Intel Hex</summary>
        [Pseudoname("i")]
        IntelHex,

        /// <summary>Motorola s-Record</summary>
        [Pseudoname("m")]
        MotorolaSRecord,

        /// <summary>Raw</summary>
        [Pseudoname("r")]
        Raw,

        /// <summary>
        ///     Фактические значения байтов для записи указываются в командной строке. Байты помещаются в поле filename и
        ///     разделяются запятыми или пробелами.
        /// </summary>
        [Pseudoname("m")]
        Manual,

        /// <summary>Авто</summary>
        [Pseudoname("a")]
        Auto,

        /// <summary>Десятичный формат. Числа разделяются запятыми.</summary>
        [Pseudoname("d")]
        Decimal,

        /// <summary>Шестнадцатеричный формат. Перед числами ставится 0x.</summary>
        [Pseudoname("h")]
        Hex
    }

    /// <summary>Работа с памятью</summary>
    [ParameterKey('U')]
    public class UCommandParameter : AvrDudeParameter
    {
        /// <summary>Создаёт параметр команды на выполнение операции</summary>
        /// <param name="MemoryType">Тип оперируемой памяти</param>
        /// <param name="Operation">Тип операции</param>
        /// <param name="Input">Входные данные</param>
        /// <param name="InputFormat">Формат входных данных</param>
        public UCommandParameter(AvrDudeMemoryType MemoryType, AvrDudeMemoryOperationType Operation, string Input,
                                 AvrDudeInputFormat InputFormat = AvrDudeInputFormat.Auto)
        {
            this.Input = Input;
            this.MemoryType = MemoryType;
            this.Operation = Operation;
            this.InputFormat = InputFormat;
        }

        /// <summary>Входные данные</summary>
        public String Input { get; private set; }

        /// <summary>Тип оперируемой памяти</summary>
        public AvrDudeMemoryType MemoryType { get; private set; }

        /// <summary>Тип операции</summary>
        public AvrDudeMemoryOperationType Operation { get; private set; }

        /// <summary>Формат входных данных</summary>
        public AvrDudeInputFormat InputFormat { get; private set; }

        /// <summary>Значение параметра</summary>
        protected override string Value
        {
            get
            {
                return String.Format("{0}:{1}:\"{2}\":{3}",
                                     PseudonameAttribute.GetPseudoname(MemoryType),
                                     PseudonameAttribute.GetPseudoname(Operation),
                                     Input,
                                     PseudonameAttribute.GetPseudoname(InputFormat));
            }
        }

        public override string ToString()
        {
            switch (Operation)
            {
                case AvrDudeMemoryOperationType.Read:
                    return String.Format("Read:  {0} --> {1} (format: {2})", Input, MemoryType, InputFormat);
                case AvrDudeMemoryOperationType.Write:
                    return String.Format("Write: {0} --> {1} (format: {2})", MemoryType, Input, InputFormat);
                case AvrDudeMemoryOperationType.Verify:
                    return String.Format("Verify: {0} <--> {1} (format: {2})", MemoryType, Input, InputFormat);
                default:
                    return String.Format("Unknown operation: {0}", Operation);
            }
        }
    }
}
