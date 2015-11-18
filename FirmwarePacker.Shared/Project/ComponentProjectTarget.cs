namespace FirmwarePacker.Project
{
    /// <summary>Цель компонента</summary>
    public class ComponentProjectTarget
    {
        public ComponentProjectTarget(int Cell, int Modification, int Module, int Channel)
        {
            this.Module = Module;
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

        /// <summary>Номер программного модуля</summary>
        public int Module { get; private set; }

        public override string ToString()
        {
            return string.Format("Cell: {0}, Modification: {1}, Channel: {2}, Module: {3}", Cell, Modification, Channel, Module);
        }
    }
}
