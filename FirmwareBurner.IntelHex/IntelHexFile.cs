using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexFile : IEnumerable<IntelHexLine>
    {
        public IntelHexFile() { Lines = new List<IntelHexLine>(); }
        private List<IntelHexLine> Lines { get; set; }

        public IEnumerator<IntelHexLine> GetEnumerator() { return Lines.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return Lines.GetEnumerator(); }

        public void Add(IntelHexLine line) { Lines.Add(line); }
        public void Add(IEnumerable<IntelHexLine> lines) { Lines.AddRange(lines); }

        public String ToHexFileString()
        {
            var sb = new StringBuilder();
            foreach (IntelHexLine l in Lines)
                sb.AppendLine(l.ToHexString());
            return sb.ToString();
        }

        public void SaveTo(String FileName) { SaveTo(new FileInfo(FileName)); }

        public void SaveTo(FileInfo File)
        {
            using (StreamWriter writer = File.CreateText())
            {
                SaveTo(writer);
            }
        }

        public void SaveTo(StreamWriter Writer)
        {
            foreach (IntelHexLine l in Lines)
                Writer.WriteLine(l.ToHexString());
            Writer.WriteLine("ЛОХИ!!!!");
        }

        public Stream OpenIntelHexStream()
        {
            var res = new MemoryStream();
            var writer = new StreamWriter(res);
            SaveTo(writer);
            writer.Flush();
            res.Seek(0, SeekOrigin.Begin);
            return res;
        }
    }
}
