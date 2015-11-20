using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.Imaging.PropertiesProviders
{
    public class CompositePropertiesProvider : IPropertiesProvider
    {
        private readonly IList<IPropertiesProvider> _children;

        public CompositePropertiesProvider(params IPropertiesProvider[] Children) : this((IList<IPropertiesProvider>)Children) { }
        public CompositePropertiesProvider(IList<IPropertiesProvider> Children) { _children = Children; }

        public IEnumerable<ParamRecord> GetProperties() { return _children.SelectMany(child => child.GetProperties()); }
    }
}
