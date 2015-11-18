using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Lovatts.Controls.PathTrimmingTextBlock
{
    static class Extensions
    {
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }
    }
}
