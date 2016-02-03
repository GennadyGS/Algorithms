using System;
using System.Linq;

namespace SelectKMax
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var sequenceSize = int.Parse(args[0]);
            var topItemCount = int.Parse(args[1]);
            int[] numberSequence = Enumerable.Range(0, sequenceSize).ToArray();
            int[] shuffledNumberSequence = new Shuffler<int>(numberSequence).ToArray();
            var dataSource = new DataSource(shuffledNumberSequence);
            int[] maxItems = MaxItemsSelector<int>.SelectMaxItems(new DataSourceEnumerable(dataSource), topItemCount);
            Console.WriteLine("Sequence:");
            Console.WriteLine(string.Join(",", shuffledNumberSequence.Select(i => i.ToString())));
            Console.WriteLine($"Top {topItemCount} items:");
            Console.WriteLine(string.Join(",", maxItems.Select(i => i.ToString())));
            Console.ReadLine();
        }
    }
}