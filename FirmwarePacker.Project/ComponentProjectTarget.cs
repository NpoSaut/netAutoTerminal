namespace FirmwarePacker.Project
{
    /// <summary>Цель компонента</summary>
    public class ComponentProjectTarget
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ComponentProjectTarget(int Cell, int Modification, int Channel)
        {
            this.Cell = Cell;
            this.Modification = Modification;
            this.Channel = Channel;
        }

        /// <summary>Идентификатор типа ячейки</summary>
        public int Cell { get; private set; }

        /// <summary>Номер модификации ячейки</summary>
        public int Modification { get; private set; }

        /// <summary>Номер канала (полукомплекта)</summary>
        public int Channel { get; private set; }
    }
}
