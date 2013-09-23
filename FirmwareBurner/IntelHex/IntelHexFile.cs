using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexFile : IEnumerable<IntelHexLine>
    {
        private List<IntelHexLine> Lines { get; set; }

        public IntelHexFile()
        {
            Lines = new List<IntelHexLine>();
        }

        public IEnumerator<IntelHexLine> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        public void Add(IntelHexLine line)
        {
            Lines.Add(line);
        }
        public void Add(IEnumerable<IntelHexLine> lines)
        {
            Lines.AddRange(lines);
        }

        public String ToHexFileString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var l in Lines)
                sb.AppendLine(l.ToHexString());
            return sb.ToString();
        }

        public void SaveTo(FileInfo File)
        {
            using (var writer = File.CreateText())
            {
                foreach (var l in Lines)
                    writer.WriteLine(l.ToHexString());
            }
        }
    }
}
