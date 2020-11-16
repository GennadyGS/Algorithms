using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DoublyLinkedList.Tests
{
    public class DoublyLinkedListTests
    {
        [Fact]
        public void NewlyCreatedList_ShouldBeEmpty()
        {
            var list = new DblLinkedList<int>();
            list.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void ListWithOneItem_ShouldNotBeEmpty()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertHead(node);
            list.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void InsertedToHeadNode_ShouldBeHeadOfList()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertHead(node);
            list.Head.Should().Be(node);
        }

        [Fact]
        public void InsertedToHeadOfEmptyListNode_ShouldBeTailOfList()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertHead(node);
            list.Tail.Should().Be(node);
        }

        [Fact]
        public void InsertedToTailNode_ShouldBeTailOfList()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertTail(node);
            list.Tail.Should().Be(node);
        }

        [Fact]
        public void InsertedToTailOfEmptyListNode_ShouldBeHeadOfList()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertTail(node);
            list.Head.Should().Be(node);
        }

        [Fact]
        public void InsertedToHeadTwoNodes_ShouldBeInListInRightOrder()
        {
            var list = new DblLinkedList<int>();
            list.InsertHead(DblLinkedList.CreateNode(1));
            list.InsertHead(DblLinkedList.CreateNode(2));
            list.Head.Value.Should().Be(2);
            list.Head.Next.Value.Should().Be(1);
        }

        [Fact]
        public void InsertedToTailTwoNodes_ShouldBeInListInRightOrder()
        {
            var list = new DblLinkedList<int>();
            list.InsertTail(DblLinkedList.CreateNode(1));
            list.InsertTail(DblLinkedList.CreateNode(2));
            list.Tail.Value.Should().Be(2);
            list.Tail.Prev.Value.Should().Be(1);
        }

        [Fact]
        public void ListAfterInsertingAndRemovingNode_ShouldBeEmpty()
        {
            var list = new DblLinkedList<int>();
            var node = DblLinkedList.CreateNode(1);
            list.InsertTail(node);
            list.Remove(node);
            list.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void AfterRemovingHeadNode_HeadShouldBeChanged()
        {
            var list = new DblLinkedList<int>();
            var node1 = DblLinkedList.CreateNode(1);
            var node2 = DblLinkedList.CreateNode(2);
            list.InsertHead(node1);
            list.InsertHead(node2);
            list.Remove(node2);
            list.Head.Value.Should().Be(node1.Value);
        }

        [Fact]
        public void InsertedBeforeNode_ShouldBeInsertedInsertedInRightPlace()
        {
            var list = new DblLinkedList<int>();
            var node1 = DblLinkedList.CreateNode(1);
            var node2 = DblLinkedList.CreateNode(2);
            list.InsertHead(node1);
            list.InsertHead(node2);
            var newNode = DblLinkedList.CreateNode(3);
            list.InsertBefore(node1, newNode);
            list.Head.Next.Value.Should().Be(newNode.Value);
        }

        [Fact]
        public void InsertedBeforeHeadNode_ShouldBecomeTheHeadOfList()
        {
            var list = new DblLinkedList<int>();
            list.InsertHead(DblLinkedList.CreateNode(1));
            var newNode = DblLinkedList.CreateNode(3);
            list.InsertBefore(list.Head, newNode);
            list.Head.Value.Should().Be(newNode.Value);
        }

        [Fact]
        public void InsertedAfterNode_ShouldBeInsertedInsertedInRightPlace()
        {
            var list = new DblLinkedList<int>();
            var node1 = DblLinkedList.CreateNode(1);
            list.InsertHead(node1);
            var newNode = DblLinkedList.CreateNode(2);
            list.InsertAfter(node1, newNode);
            list.Head.Next.Value.Should().Be(newNode.Value);
        }

        [Fact]
        public void InsertedAfterTailNode_ShouldBecomeTheTailOfList()
        {
            var list = new DblLinkedList<int>();
            list.InsertTail(DblLinkedList.CreateNode(1));
            var newNode = DblLinkedList.CreateNode(3);
            list.InsertAfter(list.Tail, newNode);
            list.Tail.Value.Should().Be(newNode.Value);
        }


        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertHead_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            foreach (var i in input.Reverse())
            {
                list.InsertHead(DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertTail_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            foreach (var i in input)
            {
                list.InsertTail(DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertBeforeHead_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            foreach (var i in input.Reverse())
            {
                list.InsertBefore(list.Head, DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertAfterTail_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            foreach (var i in input)
            {
                list.InsertAfter(list.Tail, DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertAfter_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            var currentNode = list.Tail;
            foreach (var i in input)
            {
                currentNode = list.InsertAfter(currentNode, DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        [InlineData(new[] {1, 2, 3})]
        public void IterativeInsertBefore_ShouldProduceCorrectList(IList<int> input)
        {
            var list = new DblLinkedList<int>();
            var currentNode = list.Head;
            foreach (var i in input.Reverse())
            {
                currentNode = list.InsertBefore(currentNode, DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Fact]
        public void IterativeInsertBefore_ShouldProduceCorrectList()
        {
            var input = new string[] {"1", "2", "3"};
            var list = new DblLinkedList<string>();
            var currentNode = list.Head;
            foreach (var i in input.Reverse())
            {
                currentNode = list.InsertBefore(currentNode, DblLinkedList.CreateNode(i));
            }
            VerifyList(list, input);
        }

        [Fact]
        public void InsertHeadNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertHead(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void InsertTailNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertTail(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void InsertBeforeTailNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertBefore(l.Tail, null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void InsertAfterHeadNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertAfter(l.Head, null))
                .ShouldThrow<ArgumentNullException>();
        }


        [Fact]
        public void InsertBeforeNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertBefore(null, DblLinkedList.CreateNode("1")))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void InsertAfterNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertAfter(null, DblLinkedList.CreateNode("1")))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void InsertAfterHeadOfEmptyList_ShouldThrowInvalidOperationException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertAfter(l.Head, DblLinkedList.CreateNode("1")))
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void InsertBeforeTailOfEmptyList_ShouldThrowInvalidOperationException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.InsertBefore(l.Tail, DblLinkedList.CreateNode("1")))
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void RemoveNull_ShouldThrowArgumentException()
        {
            var list = new DblLinkedList<string>();
            list.Invoking(l => l.Remove(null))
                .ShouldThrow<ArgumentNullException>();
        }

        private void VerifyList<T>(DblLinkedList<T> list, IList<T> expectedList)
        {
            if (!expectedList.Any())
            {
                list.Head.IsNull.Should().BeTrue();
                list.Tail.IsNull.Should().BeTrue();
                list.IsEmpty().Should().BeTrue();
                return;
            }

            list.Head.IsNull.Should().BeFalse();
            list.Tail.IsNull.Should().BeFalse();

            list.Head.Value.Should().Be(expectedList.First());
            list.Tail.Value.Should().Be(expectedList.Last());

            VerifyListNode(list.Head, expectedList);
        }

        private void VerifyListNode<T>(DblLinkedList<T>.Node node, IList<T> expectedList)
        {
            if (!expectedList.Any())
            {
                node.IsNull.Should().BeTrue();
                return;
            }

            node.Value.Should().Be(expectedList.First());

            node.Prev.Next.Should().Be(node);
            node.Next.Prev.Should().Be(node);

            VerifyListNode(node.Next, expectedList.Skip(1).ToList());
        }
    }
}