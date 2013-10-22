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
        private ReadOnlyCollection<BlockKind> _Blocks;
        public override ReadOnlyCollection<BlockKind> Blocks { get { return _Blocks; } }

        public XmlIndex(String Filename)
            : this(XDocument.Load(Filename).Root)
        { }
        public XmlIndex(XElement XRoot)
        {
            _Blocks =
                new ReadOnlyCollection<BlockKind>(
                XRoot.Elements("block").Select(XBlock =>
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
                                    }).ToList(),
                                Modifications = XBlock.Elements("modification").Select(XModule =>
                                    new ModificationKind()
                                    {
                                        Id = (int)XModule.Attribute("id"),
                                        Name = (String)XModule.Attribute("name")
                                    }).ToList()
                            }).ToList());
        }
    }
}
