using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Irony.Parsing;
using NUnit.Framework;

namespace NotamLib.Parsing.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        private Parser parser;
        
        [SetUp]
        public void Setup()
        {
            var grammar = new CoordinateGrammar();
            var language = new LanguageData(grammar);
            parser = new Parser(language);
        }

        [Test]
        public void AFullCoordinateShouldBeParsedCorrectly()
        {
            var tree = parser.Parse("7 5' 4\" N 1 1' 6\" E");
            tree.ParserMessages.Should().BeEmpty();
        }
    }
}
