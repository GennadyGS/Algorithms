using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ReverseList
{
    public class TestReverseList
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
        [InlineData(new int[] { }, new int[] { })]
        void ListShoudBeRevertedCorrectly(IEnumerable<int> input, IEnumerable<int> output)
        {
            LinkedList<int> inputList = LinkedList.FromEnumerable(input);
            var outputList = inputList.ReverseList();
            outputList.ToEnumerable().ShouldAllBeEquivalentTo(output);
        }
    }
}