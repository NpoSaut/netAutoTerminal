using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public class ConsecutiveSectionContent<TMemoryKind> : ISectionContent<TMemoryKind>
    {
        private readonly IList<ISectionContent<TMemoryKind>> _children;

        public ConsecutiveSectionContent(params ISectionContent<TMemoryKind>[] Children) : this((IList<ISectionContent<TMemoryKind>>)Children) { }
        public ConsecutiveSectionContent(IList<ISectionContent<TMemoryKind>> Children) { _children = Children; }

        public void WriteTo(IWriter Writer)
        {
            foreach (var child in _children)
                child.WriteTo(Writer);
        }
    }
}
