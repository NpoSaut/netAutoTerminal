using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.BootloadersCatalog
{
    /// <summary>XML-каталог загрузчиков</summary>
    /// <remarks>
    ///     Берёт каталог загрузчиков из папки, являющейся частью проекта. Подразумевается, что эта папка будет отдельным
    ///     git-подмодулем и разработчики загрузчиков смогут сами выкладывать туда свои вкусняшки
    /// </remarks>
    [UsedImplicitly]
    public class XmlBootloadersCatalog : IBootloadersCatalog
    {
        public const string BootloadersCatalogPath = "Bootloaders";
        public const string BootloadersIndexFileName = "Bootloaders.xml";

        private readonly ILookup<string, BootloaderCatalogRecord> _catalog;

        public XmlBootloadersCatalog()
        {
            XDocument doc = XDocument.Load(Path.Combine(BootloadersCatalogPath, BootloadersIndexFileName));
            _catalog = doc.Root.Elements()
                          .ToLookup(r => r.Name.LocalName,
                                    r => new BootloaderCatalogRecord((int)r.Attribute("Id"),
                                                                     (string)r.Attribute("Device"),
                                                                     (int)r.Attribute("Version"),
                                                                     (int)r.Attribute("CompatibleVersion"),
                                                                     Path.GetFullPath(Path.Combine(BootloadersCatalogPath, (string)r.Attribute("File"))),
                                                                     r.Attributes().ToDictionary(a => a.Name.LocalName, a => (string)a)));
        }

        public IEnumerable<BootloaderCatalogRecord> GetBootloaders(string BootloaderKind) { return _catalog[BootloaderKind]; }
    }
}
