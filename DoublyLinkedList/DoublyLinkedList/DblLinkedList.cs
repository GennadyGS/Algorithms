using System;

namespace DoublyLinkedList
{
    public class DblLinkedList<T>
    {
        private readonly Node _head;
        private readonly Node _tail;

        public DblLinkedList()
        {
            _head = new Node(default(T), true);
            _tail = new Node(default(T), true);
            _head.Next = _tail;
            _tail.Prev = _head;
        }

        public Node Head => _head.Next;

        public Node Tail => _tail.Prev;

        public bool IsEmpty()
        {
            return Head.IsNull && Tail.IsNull;
        }

        public Node InsertHead(Node newNode)
        {
            return InsertAfter(_head, newNode);
        }

        public Node InsertTail(Node newNode)
        {
            return InsertBefore(_tail, newNode);
        }

        public Node InsertBefore(Node node, Node newNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }
            if (node == _head)
            {
                throw new InvalidOperationException("Cannot insert before tail of empty list");
            }

            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev.Next = newNode;
            node.Prev = newNode;

            return newNode;
        }

        public Node InsertAfter(Node node, Node newNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }
            if (node == _tail)
            {
                throw new InvalidOperationException("Cannot insert after head of empty list");
            }

            newNode.Next = node.Next;
            newNode.Prev = node;
            node.Next.Prev = newNode;
            node.Next = newNode;

            return newNode;
        }

        public void Remove(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        public class Node
        {
            internal Node(T value, bool isNull)
            {
                Value = value;
                IsNull = isNull;
            }

            public T Value { get; }

            public Node Next { get; internal set; }

            public Node Prev { get; internal set; }

            public bool IsNull { get; }
        }
    }

    public static class DblLinkedList
    {
        public static DblLinkedList<T>.Node CreateNode<T>(T value)
        {
            return new DblLinkedList<T>.Node(value, false);
        }
    }
}