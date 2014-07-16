﻿namespace Forest
{
    internal class Node<T>
    {
        public T Value { set; get; }
        public Node<T> LeftNode { set; get; }
        public Node<T> RightNode { set; get; }
        public Node<T> Parent { set; get; }
        public Node(T value, Node<T> parent)
        {
            Value = value;
            LeftNode = null;
            RightNode = null;
            Parent = parent;
        }
    }
}
