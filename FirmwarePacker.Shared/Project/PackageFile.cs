namespace FirmwarePacker.Project
{
    public class PackageFile
    {
        public PackageFile(string Name, byte[] Content)
        {
            this.Name = Name;
            this.Content = Content;
        }

        public string Name { get; private set; }
        public byte[] Content { get; private set; }
    }
}
