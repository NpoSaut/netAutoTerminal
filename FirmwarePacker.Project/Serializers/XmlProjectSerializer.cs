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

        public PackageProject Load(string FileName)
        {
            XDocument document = XDocument.Load(FileName);
            XElement xProject = document.Root;

            return
                new PackageProject(xProject.Elements("Component").Select(LoadComponent).ToList());
        }

        public void Save(PackageProject Project, string FileName)
        {
            var xDoc = new XDocument(
                new XElement("Project",
                             Project.Components.Select(component =>
                                                       new XElement("Component",
                                                                    component.Targets.Select(target =>
                                                                                             new XElement("Target",
                                                                                                          new XAttribute("Cell", target.Cell),
                                                                                                          new XAttribute("Modification", target.Modification),
                                                                                                          new XAttribute("Module", target.Module),
                                                                                                          new XAttribute("Channel", target.Channel))),
                                                                    new XElement("Files")))));
            throw new NotImplementedException("На самом деле не написал я :(");
            xDoc.Save(FileName);
        }

        private ComponentProject LoadComponent(XElement XComponent)
        {
            return new ComponentProject(XComponent.Elements("Target")
                                                  .Select(xTarget =>
                                                          new ComponentProjectTarget((int)xTarget.Attribute("Cell"),
                                                                                     (int)xTarget.Attribute("Modification"),
                                                                                     (int)xTarget.Attribute("Module"),
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
