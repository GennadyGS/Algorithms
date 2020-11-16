using System;
using FluentAssertions;
using Xunit;

namespace PrimeFactors.Tests
{
    public class PrimeFactorTests
    {
        private readonly PrimeFactorGenerator _primeFactorGenerator = new PrimeFactorGenerator();

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void Generate_ShouldThrowExceptionForInvalidArguments(int number)
        {
            Action act = () => _primeFactorGenerator.Generate(number);
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Generate_ShouldReturnNonEmptyList(int number)
        {
            _primeFactorGenerator.Generate(number).Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(2, new[] { 2 })]
        [InlineData(3, new[] { 3 })]
        [InlineData(4, new[] { 2, 2 })]
        [InlineData(5, new[] { 5 })]
        public void Generate_ShouldReturnCorrectList(int number, int[] expectedResult)
        {
            _primeFactorGenerator.Generate(number)
                .ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}
