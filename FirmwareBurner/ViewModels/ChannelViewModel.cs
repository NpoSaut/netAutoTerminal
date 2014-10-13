using System;

namespace FirmwareBurner.ViewModels
{
    public class ChannelViewModel
    {
        public ChannelViewModel(int Number) : this(Number, string.Format("Канал {0}", Number)) { }

        public ChannelViewModel(int Number, string Name)
        {
            this.Number = Number;
            this.Name = Name;
        }

        public int Number { get; private set; }
        public String Name { get; private set; }
    }
}
