using System;
using System.Linq;

namespace PrimeFactors
{
    public class PrimeFactorGenerator
    {
        public int[] Generate(int number)
        {
            if (number < 2)
            {
                throw new ArgumentException(nameof(number));
            }
            return InternalGenerate(number);
        }

        private int[] InternalGenerate(int number)
        {
            if (number < 2)
            {
                return new int[] {};
            }
            for (int i = 2; i <= number; i++)
            {
                if (number%i == 0)
                {
                    return new[] {i}
                        .Concat(InternalGenerate(number/i))
                        .ToArray();
                }
            }
            throw new InvalidOperationException("Code should never be reached");
        }
    }
}