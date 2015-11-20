using System.IO;
using System.Reflection;
using FirmwareBurner.ImageFormatters.Binary.Exceptions;

namespace FirmwareBurner.ImageFormatters.Binary.BodyLoaders
{
    public class ResourceBodyLoader : IBodyLoader
    {
        private readonly Assembly _resourceAssembly;
        private readonly string _resourceName;

        public ResourceBodyLoader(Assembly ResourceAssembly, string ResourceName)
        {
            _resourceAssembly = ResourceAssembly;
            _resourceName = ResourceName;
        }

        public byte[] LoadBootloaderBody()
        {
            Stream bodyResourceStream = _resourceAssembly.GetManifestResourceStream(string.Format("{0}.{1}", _resourceAssembly.GetName().Name, _resourceName));
            if (bodyResourceStream == null)
                throw new BootloaderBodyNotFoundException();
            using (var body = new MemoryStream())
            {
                bodyResourceStream.CopyTo(body);
                return body.ToArray();
            }
        }
    }
}
