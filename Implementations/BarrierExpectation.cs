﻿using System.Text.RegularExpressions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Ожидание-барьер</summary>
    /// <remarks>Просто завершает процесс поиска после обнаружения искомой фразы</remarks>
    public class BarrierExpectation : IExpectation
    {
        public BarrierExpectation(string Pattern) : this(new Regex(Pattern)) { }
        public BarrierExpectation(Regex Regex) { this.Regex = Regex; }
        public Regex Regex { get; private set; }
        public bool Activate(Match Match) { return true; }
    }
}
