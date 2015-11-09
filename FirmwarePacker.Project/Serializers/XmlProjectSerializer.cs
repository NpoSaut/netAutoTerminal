using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FirmwarePacker.Project.FileMaps;

namespace FirmwarePacker.Project.Serializers
{
    public class XmlProjectSerializer : IProjectSerializer
    {
        private static readonly IDictionary<string, IFileMapFactory> _fileMapFactories =
            new Dictionary<string, IFileMapFactory>
            {
                { "MapFile", new SingleFileMapFactory() },
                { "MapFolder", new FolderMapFactory() }
            };

        private readonly string _fileName;
        public XmlProjectSerializer(string FileName) { _fileName = FileName; }

        public PackageProject Load()
        {
            XDocument document = XDocument.Load(_fileName);
            XElement xProject = document.Root;

            return
                new PackageProject(LoadVersion(xProject.Element("Version")),
                                   xProject.Elements("Component").Select(LoadComponent).ToList());
        }

        public void Save(PackageProject Project)
        {
            var xDoc = new XDocument(
                new XElement("Project",
                             new XElement("Version",
                                          new XAttribute("Major", Project.Version.Major),
                                          new XAttribute("Minor", Project.Version.Minor),
                                          Project.Version != null
                                              ? new XAttribute("Label", Project.Version.Label)
                                              : null),
                             Project.Components.Select(component =>
                                                       new XElement("Component",
                                                                    component.Targets.Select(target =>
                                                                                             new XElement("Target",
                                                                                                          new XAttribute("Cell", target.Cell),
                                                                                                          new XAttribute("Modification", target.Modification),
                                                                                                          new XAttribute("Channel", target.Channel))),
                                                                    new XElement("Files")))));
            throw new NotImplementedException("На самом деле не написал я :(");
            xDoc.Save(_fileName);
        }

        private static PackageVersion LoadVersion(XElement XVersion)
        {
            return new PackageVersion((int)XVersion.Attribute("Major"),
                                      (int)XVersion.Attribute("Minor"),
                                      (string)XVersion.Attribute("Label"));
        }

        private ComponentProject LoadComponent(XElement XComponent)
        {
            return new ComponentProject(XComponent.Elements("Target")
                                                  .Select(xTarget =>
                                                          new ComponentProjectTarget((int)xTarget.Attribute("Cell"),
                                                                                     (int)xTarget.Attribute("Modification"),
                                                                                     (int)xTarget.Attribute("Channel")))
                                                  .ToList(),
                                        XComponent.Element("Files").Elements().Select(LoadFileMap).ToList());
        }

        private IFileMap LoadFileMap(XElement XFileMap)
        {
            return _fileMapFactories[XFileMap.Name.LocalName].CreateFileMap(XFileMap.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value));
        }
    }
}
