using System;
using System.Collections.Generic;
using System.Threading;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in GetFibonacci())
            {
                Console.WriteLine(item);
                Thread.Sleep(100);
            }
        }

        private static IEnumerable<long> GetFibonacciFrom(long previous, long last)
        {
            long next = previous + last;
            yield return next;
            foreach (var item in GetFibonacciFrom(last, next))
            {
                yield return item;
            }
        }

        private static IEnumerable<long> GetFibonacci()
        {
            int previous = 1;
            int last = 2;
            yield return previous;
            yield return last;
            foreach (var item in GetFibonacciFrom(previous, last))
            {
                yield return item;
            }
        }
    }
}