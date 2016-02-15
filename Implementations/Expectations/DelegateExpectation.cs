using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations.Expectations
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
        /// <param name="RequiredSurfingMethod">Метод сканирования вывода, необходимый для работы этого ожидания</param>
        public DelegateExpectation([NotNull] String Pattern, bool FinalizesSequence, [NotNull] Action<Match> ActivationDelegate,
                                   SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
            : this(Pattern, DefaultDelegate(ActivationDelegate, FinalizesSequence), RequiredSurfingMethod) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Pattern">Регулярное выражение для поиска</param>
        /// <param name="ActivationDelegate">
        ///     Делегат, который следует вызвать при срабатывании ожидания. Делегат должен вернуть
        ///     True, если поиск можно остановить
        /// </param>
        /// <param name="RequiredSurfingMethod">Метод сканирования вывода, необходимый для работы этого ожидания</param>
        public DelegateExpectation([NotNull] String Pattern, [NotNull] Func<Match, bool> ActivationDelegate,
                                   SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
            : this(new Regex(Pattern, RegexOptions.Multiline | RegexOptions.Compiled), ActivationDelegate, RequiredSurfingMethod) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Regex">Регулярное выражение для поиска</param>
        /// <param name="FinalizesSequence">Следует ли остановить поиск после срабатывания ожидания</param>
        /// <param name="ActivationDelegate">Делегат, который следует вызвать при срабатывании ожидания</param>
        /// <param name="RequiredSurfingMethod">Метод сканирования вывода, необходимый для работы этого ожидания</param>
        public DelegateExpectation([NotNull] Regex Regex, bool FinalizesSequence, [NotNull] Action<Match> ActivationDelegate,
                                   SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
            : this(Regex, DefaultDelegate(ActivationDelegate, FinalizesSequence), RequiredSurfingMethod) { }

        /// <summary>Вызвать делегат после появления ожидания</summary>
        /// <param name="Regex">Регулярное выражение для поиска</param>
        /// <param name="ActivationDelegate">
        ///     Делегат, который следует вызвать при срабатывании ожидания. Делегат должен вернуть
        ///     True, если поиск можно остановить
        /// </param>
        /// <param name="RequiredSurfingMethod">Метод сканирования вывода, необходимый для работы этого ожидания</param>
        public DelegateExpectation([NotNull] Regex Regex, [NotNull] Func<Match, bool> ActivationDelegate,
                                   SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
        {
            this.Regex = Regex;
            _activationDelegate = ActivationDelegate;
            this.RequiredSurfingMethod = RequiredSurfingMethod;
        }

        public Regex Regex { get; private set; }
        public SurfingMethod RequiredSurfingMethod { get; private set; }
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
