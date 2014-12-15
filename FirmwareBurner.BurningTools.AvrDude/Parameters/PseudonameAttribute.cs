using System;
using System.Linq;
using System.Reflection;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    public class PseudonameAttribute : Attribute
    {
        public PseudonameAttribute(string Pseudoname) { this.Pseudoname = Pseudoname; }
        public String Pseudoname { get; private set; }

        public static string GetPseudoname(Enum Value)
        {
            FieldInfo fi = Value.GetType().GetField(Value.ToString());
            return fi.GetCustomAttributes(typeof (PseudonameAttribute), false).OfType<PseudonameAttribute>().Select(a => a.Pseudoname).FirstOrDefault();
        }
    }
}
