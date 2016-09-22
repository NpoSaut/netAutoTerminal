using System.Text.RegularExpressions;
using NUnit.Framework;
using Saut.AutoTerminal.Implementations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Tests
{
    [TestFixture]
    public class RegexSurferTests
    {
        private const string SampleOutput =
            @"CPU:     S5PC100@666MHz
	          Fclk = 1332MHz, Hclk = 166MHz, Pclk = 66MHz, Serial = PCLK 
	          Board:   SMDKC100
	          DRAM:    256 MB
	          Flash:   0 kB
	          SD/MMC:  NAND:    256 MB 
	          s3c-nand: 4 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          s3c-nand: 1 bit(s) error detected, corrected successfully
	          In:      serial
	          Out:     serial
	          Err:     serial
	          Hit any key to stop autoboot:  0";

        private class TestExpectation : IExpectation
        {
            private readonly bool _finalizes;

            public TestExpectation(string Pattern, bool Finalizes = true, SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
            {
                _finalizes = Finalizes;
                this.RequiredSurfingMethod = RequiredSurfingMethod;
                Regex = new Regex(Pattern, RegexOptions.Multiline);
            }

            public Match Match { get; set; }
            public SurfingMethod RequiredSurfingMethod { get; private set; }
            public Regex Regex { get; private set; }

            public bool Activate(Match Match)
            {
                this.Match = Match;
                return _finalizes;
            }
        }

        [Test]
        public void FindAnything()
        {
            var seeker = new RegexSurfer();
            var terminal = new Terminal(new ConstantTerminalCore(SampleOutput));
            var expectation = new TestExpectation(@"Hit any key to stop autoboot:\s+\d+\s*$");
            seeker.SeekForMatches(terminal, expectation);
            Assert.That(expectation.Match.Value, Is.EqualTo("Hit any key to stop autoboot:  0"));
        }

        [Test]
        public void FindOneAndThenAnother()
        {
            var seeker = new RegexSurfer();
            var terminal = new Terminal(new ConstantTerminalCore(SampleOutput));

            var expectation1 = new TestExpectation(@"DRAM:\s+\d+ MB");
            seeker.SeekForMatches(terminal, expectation1);
            Assert.That(expectation1.Match.Value, Is.EqualTo("DRAM:    256 MB"));

            var expectation2 = new TestExpectation(@"Flash:\s+\d+ kB");
            seeker.SeekForMatches(terminal, expectation2);
            Assert.That(expectation2.Match.Value, Is.EqualTo("Flash:   0 kB"));
        }
    }
}
