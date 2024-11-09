using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public class RBNode : ITreeNode
    {
        public enum Color { Red, Black }

        public int Value { get; set; }
        public ITreeNode Left { get; set; }
        public ITreeNode Right { get; set; }
        public Color NodeColor { get; set; }
        public RBNode Parent { get; set; }

        public RBNode(int value)
        {
            Value = value;
            NodeColor = Color.Red;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    public class RedBlackTree : ITree
    {
        public ITreeNode root { get; private set; }

        public void Insert(int value)
        {
            RBNode newNode = new RBNode(value);
            root = Insert((RBNode)root, newNode);
            FixViolation(newNode);
            Console.WriteLine($"Inserted node with value {value}");
        }

        private RBNode Insert(RBNode root, RBNode node)
        {
            if (root == null)
                return node;

            if (node.Value < root.Value)
            {
                root.Left = Insert((RBNode)root.Left, node);
                ((RBNode)root.Left).Parent = root;
            }
            else if (node.Value > root.Value)
            {
                root.Right = Insert((RBNode)root.Right, node);
                ((RBNode)root.Right).Parent = root;
            }
            return root;
        }

        private void FixViolation(RBNode node)
        {
            while (node != root && ((RBNode)node.Parent).NodeColor == RBNode.Color.Red)
            {
                RBNode parent = (RBNode)node.Parent;
                RBNode grandparent = parent.Parent;

                if (parent == grandparent.Left)
                {
                    RBNode uncle = (RBNode)grandparent.Right;
                    if (uncle != null && uncle.NodeColor == RBNode.Color.Red)
                    {
                        grandparent.NodeColor = RBNode.Color.Red;
                        parent.NodeColor = RBNode.Color.Black;
                        uncle.NodeColor = RBNode.Color.Black;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            LeftRotate(parent);
                            node = parent;
                            parent = (RBNode)node.Parent;
                        }
                        RightRotate(grandparent);
                        RBNode.Color tempColor = parent.NodeColor;
                        parent.NodeColor = grandparent.NodeColor;
                        grandparent.NodeColor = tempColor;
                        node = parent;
                    }
                }
                else
                {
                    RBNode uncle = (RBNode)grandparent.Left;
                    if (uncle != null && uncle.NodeColor == RBNode.Color.Red)
                    {
                        grandparent.NodeColor = RBNode.Color.Red;
                        parent.NodeColor = RBNode.Color.Black;
                        uncle.NodeColor = RBNode.Color.Black;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            RightRotate(parent);
                            node = parent;
                            parent = (RBNode)node.Parent;
                        }
                        LeftRotate(grandparent);
                        RBNode.Color tempColor = parent.NodeColor;
                        parent.NodeColor = grandparent.NodeColor;
                        grandparent.NodeColor = tempColor;
                        node = parent;
                    }
                }
            }
            ((RBNode)root).NodeColor = RBNode.Color.Black;
        }

        public void Delete(int value)
        {
            root = Delete((RBNode)root, value);
            if (root != null)
                ((RBNode)root).NodeColor = RBNode.Color.Black;
            Console.WriteLine($"Deleted node with value {value}");
        }

        private RBNode Delete(RBNode node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
            {
                node.Left = Delete((RBNode)node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete((RBNode)node.Right, value);
            }
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    return DeleteOneChild(node);
                }
                else
                {
                    RBNode successor = FindMax((RBNode)node.Left);
                    node.Value = successor.Value;
                    node.Left = Delete((RBNode)node.Left, successor.Value);
                }
            }
            return node;
        }

        private RBNode DeleteOneChild(RBNode node)
        {
            RBNode child = (RBNode)(node.Left != null ? node.Left : node.Right);

            if (node.NodeColor == RBNode.Color.Black)
            {
                if (child != null && child.NodeColor == RBNode.Color.Red)
                {
                    child.NodeColor = RBNode.Color.Black;
                }
                else
                {
                    FixDoubleBlack(child);
                }
            }

            ReplaceNode(node, child);
            return child;
        }

        private void ReplaceNode(RBNode node, RBNode child)
        {
            if (child != null)
            {
                child.Parent = node.Parent;
            }

            if (node.Parent == null)
            {
                root = child;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = child;
            }
            else
            {
                node.Parent.Right = child;
            }
        }

        private RBNode FixDoubleBlack(RBNode node)
        {
            if (node == root)
                return node;

            RBNode sibling = GetSibling(node);
            RBNode parent = node?.Parent;

            if (sibling == null)
            {
                if (parent != null)
                    FixDoubleBlack(parent);
            }
            else
            {
                if (sibling.NodeColor == RBNode.Color.Red)
                {
                    if (parent != null)
                    {
                        parent.NodeColor = RBNode.Color.Red;
                        sibling.NodeColor = RBNode.Color.Black;
                        if (sibling == parent.Left)
                            RightRotate(parent);
                        else
                            LeftRotate(parent);
                        FixDoubleBlack(node);
                    }
                }
                else
                {
                    RBNode leftChild = (RBNode)sibling.Left;
                    RBNode rightChild = (RBNode)sibling.Right;

                    if ((leftChild == null || leftChild.NodeColor == RBNode.Color.Black) &&
                        (rightChild == null || rightChild.NodeColor == RBNode.Color.Black))
                    {
                        sibling.NodeColor = RBNode.Color.Red;
                        if (parent != null && parent.NodeColor == RBNode.Color.Black)
                            FixDoubleBlack(parent);
                    }
                    else
                    {
                        if (sibling == parent?.Left &&
                            (rightChild == null || rightChild.NodeColor == RBNode.Color.Black) &&
                            (leftChild != null && leftChild.NodeColor == RBNode.Color.Red))
                        {
                            sibling.NodeColor = RBNode.Color.Red;
                            leftChild.NodeColor = RBNode.Color.Black;
                            RightRotate(sibling);
                        }
                        else if (sibling == parent?.Right &&
                                (leftChild == null || leftChild.NodeColor == RBNode.Color.Black) &&
                                (rightChild != null && rightChild.NodeColor == RBNode.Color.Red))
                        {
                            sibling.NodeColor = RBNode.Color.Red;
                            rightChild.NodeColor = RBNode.Color.Black;
                            LeftRotate(sibling);
                        }

                        sibling = GetSibling(node);
                        FixDoubleBlack(node);
                    }
                }
            }
            return node;
        }

        private RBNode GetSibling(RBNode node)
        {
            if (node == null || node.Parent == null)
                return null;

            if (node == node.Parent.Left)
                return (RBNode)node.Parent.Right;
            else
                return (RBNode)node.Parent.Left;
        }

        private void LeftRotate(RBNode node)
        {
            RBNode newRoot = (RBNode)node.Right;
            node.Right = newRoot.Left;
            if (newRoot.Left != null)
                ((RBNode)newRoot.Left).Parent = node;
            newRoot.Parent = node.Parent;
            if (node.Parent == null)
                root = newRoot;
            else if (node == node.Parent.Left)
                node.Parent.Left = newRoot;
            else
                node.Parent.Right = newRoot;
            newRoot.Left = node;
            node.Parent = newRoot;
        }

        private void RightRotate(RBNode node)
        {
            RBNode newRoot = (RBNode)node.Left;
            node.Left = newRoot.Right;
            if (newRoot.Right != null)
                ((RBNode)newRoot.Right).Parent = node;
            newRoot.Parent = node.Parent;
            if (node.Parent == null)
                root = newRoot;
            else if (node == node.Parent.Right)
                node.Parent.Right = newRoot;
            else
                node.Parent.Left = newRoot;
            newRoot.Right = node;
            node.Parent = newRoot;
        }

        private RBNode FindMax(RBNode node)
        {
            while (node.Right != null)
                node = (RBNode)node.Right;
            return node;
        }

        public bool Search(int value)
        {
            return Search((RBNode)root, value);
        }

        private bool Search(RBNode node, int value)
        {
            if (node == null)
                return false;

            if (value == node.Value)
                return true;

            if (value < node.Value)
                return Search((RBNode)node.Left, value);
            else
                return Search((RBNode)node.Right, value);
        }
    }
}
