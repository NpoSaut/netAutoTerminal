﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwarePacker.Models
{
    public class FirmwareTreeModel : ViewModel, IDataCheck
    {
        public DirectoryInfo RootDirectory { get; set; }

        public FirmwareTreeModel()
        { }
        public FirmwareTreeModel(DirectoryInfo di)
            : this()
        {
            RootDirectory = di;
        }
        public FirmwareTreeModel(String Path)
            : this(new DirectoryInfo(Path))
        { }

        public List<FileInfo> GetFiles()
        {
            return RootDirectory.EnumerateFiles("*", SearchOption.AllDirectories).ToList();
        }

        public string Totals
        {
            get
            {
                if (RootDirectory == null) return "";
                var fl = GetFiles();
                return string.Format("{0} файл{1} ({2}Б)", fl.Count, GetEnding(fl.Count), GetLetteredCount(fl.Sum(f => f.Length)));
            }
        }

        private static string GetEnding(int count)
        {
            var c = count % 10;
            if (count >= 11 && count <= 14) return "ов";
            else if (c == 1) return "";
            else if (c >= 2 && c <= 4) return "а";
            else return "ов";
        }

        private static string GetLetteredCount(double Count)
        {
            var letter =
                (new String[] { "", "К", "М", "Г", "Т" })
                .Select((l, i) => new { m = Math.Pow(1024, i), l = l })
                .First(l => Count < 4000 * l.m);
            return (Math.Round(Count * 10 / letter.m) / 10).ToString() + " " + letter.l;
        }

        public override string ToString()
        {
            if (RootDirectory != null) return RootDirectory.FullName;
            else return "папка не выбрана";
        }

        public bool Check()
        {
            return
                RootDirectory != null &&
                GetFiles().Any();
        }

        public FirmwareTreeModel DeepClone()
        {
            return RootDirectory != null ?
                new FirmwareTreeModel(RootDirectory.FullName) :
                new FirmwareTreeModel();
        }
    }
}
