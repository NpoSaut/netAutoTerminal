using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Ожидание с делегатом на выполнение</summary>
    /// <remarks>При срабатывании вызывает переданный делегат</remarks>
    public class DelegateExpectation : IExpectation
    {
        private readonly Func<Match, bool> _activationDelegate;

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Pattern">Регулярное выражение для поиска</param>
        /// <param name="FinalizesSequence">Следует ли остановить поиск после срабатывания ожидания</param>
        /// <param name="ActivationDelegate">Делегат, который следует вызвать при срабатывании ожидания</param>
        public DelegateExpectation([NotNull] String Pattern, bool FinalizesSequence, [NotNull] Action<Match> ActivationDelegate)
            : this(Pattern, DefaultDelegate(ActivationDelegate, FinalizesSequence)) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Pattern">Регулярное выражение для поиска</param>
        /// <param name="ActivationDelegate">
        ///     Делегат, который следует вызвать при срабатывании ожидания. Делегат должен вернуть
        ///     True, если поиск можно остановить
        /// </param>
        public DelegateExpectation([NotNull] String Pattern, [NotNull] Func<Match, bool> ActivationDelegate)
            : this(new Regex(Pattern, RegexOptions.Multiline | RegexOptions.Compiled), ActivationDelegate) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Regex">Регулярное выражение для поиска</param>
        /// <param name="FinalizesSequence">Следует ли остановить поиск после срабатывания ожидания</param>
        /// <param name="ActivationDelegate">Делегат, который следует вызвать при срабатывании ожидания</param>
        public DelegateExpectation([NotNull] Regex Regex, bool FinalizesSequence, [NotNull] Action<Match> ActivationDelegate)
            : this(Regex, DefaultDelegate(ActivationDelegate, FinalizesSequence)) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Regex">Регулярное выражение для поиска</param>
        /// <param name="ActivationDelegate">
        ///     Делегат, который следует вызвать при срабатывании ожидания. Делегат должен вернуть
        ///     True, если поиск можно остановить
        /// </param>
        public DelegateExpectation([NotNull] Regex Regex, [NotNull] Func<Match, bool> ActivationDelegate)
        {
            this.Regex = Regex;
            _activationDelegate = ActivationDelegate;
        }

        public Regex Regex { get; private set; }
        public bool Activate(Match Match) { return _activationDelegate(Match); }

        private static Func<Match, bool> DefaultDelegate(Action<Match> ActivationDelegate, bool FinalizesSequence)
        {
            return Match =>
                   {
                       ActivationDelegate(Match);
                       return FinalizesSequence;
                   };
        }
    }
}
