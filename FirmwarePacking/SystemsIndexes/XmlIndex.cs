﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public class XmlIndex : Index
    {
        private ReadOnlyCollection<SystemKind> _Systems;
        public override ReadOnlyCollection<SystemKind> Systems { get { return _Systems; } }

        public XmlIndex(String Filename)
            : this(XDocument.Load(Filename).Root)
        { }
        public XmlIndex(XElement XRoot)
        {
            _Systems = new ReadOnlyCollection<SystemKind>(
                XRoot.Elements("system").Select(XSystem =>
                    new SystemKind()
                    {
                        Id = (int)XSystem.Attribute("id"),
                        Name = (String)XSystem.Attribute("name"),
                        Blocks = XSystem.Elements("block").Select(XBlock =>
                            new BlockKind()
                            {
                                Id = (int)XBlock.Attribute("id"),
                                Name = (String)XBlock.Attribute("name"),
                                ChannelsCount = (int)XBlock.Attribute("channels"),
                                Modules = XBlock.Elements("module").Select(XModule =>
                                    new ModuleKind()
                                    {
                                        Id = (int)XModule.Attribute("id"),
                                        Name = (String)XModule.Attribute("name")
                                    }).ToList()
                            }).ToList()
                    })
                    .ToList());
        }
    }
}
