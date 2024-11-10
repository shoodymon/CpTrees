using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public class BTreeNode : IBTreeNode
    {
        public List<int> Keys { get; set; }
        public List<IBTreeNode> Children { get; set; }
        public bool IsLeaf { get; set; }

        // Реализация ITreeNode (для совместимости с визуализатором)
        public int Value { get; set; }
        public ITreeNode Left { get; set; }
        public ITreeNode Right { get; set; }

        public BTreeNode(bool isLeaf = true)
        {
            Keys = new List<int>();
            Children = new List<IBTreeNode>();
            IsLeaf = isLeaf;
        }
    }

    public class BTree : ITree
    {
        private readonly int degree; // Минимальная степень (минимальное количество ключей = degree-1)
        private IBTreeNode _root;

        public ITreeNode root => _root;

        public BTree(int degree = 2)
        {
            this.degree = degree;
            _root = new BTreeNode();
        }

        public void Insert(int value)
        {
            IBTreeNode r = _root;

            // Если корень полный, создаем новый корень
            if (r.Keys.Count == (2 * degree - 1))
            {
                IBTreeNode newRoot = new BTreeNode(false);
                newRoot.Children.Add(r);
                SplitChild(newRoot, 0);
                InsertNonFull(newRoot, value);
                _root = newRoot;
            }
            else
            {
                InsertNonFull(r, value);
            }
        }

        private void InsertNonFull(IBTreeNode node, int value)
        {
            int i = node.Keys.Count - 1;

            if (node.IsLeaf)
            {
                // Найти позицию для вставки и сдвинуть все большие ключи
                while (i >= 0 && value < node.Keys[i])
                {
                    i--;
                }
                node.Keys.Insert(i + 1, value);
            }
            else
            {
                // Найти потомка для вставки
                while (i >= 0 && value < node.Keys[i])
                {
                    i--;
                }
                i++;

                // Если потомок полный, разделить его
                if (node.Children[i].Keys.Count == 2 * degree - 1)
                {
                    SplitChild(node, i);
                    if (value > node.Keys[i])
                    {
                        i++;
                    }
                }
                InsertNonFull(node.Children[i], value);
            }
        }

        private void SplitChild(IBTreeNode parent, int index)
        {
            IBTreeNode child = parent.Children[index];
            IBTreeNode newChild = new BTreeNode(child.IsLeaf);

            // Перемещаем ключи и потомков
            for (int j = 0; j < degree - 1; j++)
            {
                newChild.Keys.Add(child.Keys[j + degree]);
            }

            if (!child.IsLeaf)
            {
                for (int j = 0; j < degree; j++)
                {
                    newChild.Children.Add(child.Children[j + degree]);
                }
                child.Children.RemoveRange(degree, degree);
            }

            // Вставляем средний ключ в родителя
            parent.Keys.Insert(index, child.Keys[degree - 1]);
            parent.Children.Insert(index + 1, newChild);

            // Удаляем перемещенные ключи из старого узла
            child.Keys.RemoveRange(degree - 1, degree);
        }

        public bool Search(int value)
        {
            return SearchRecursive(_root, value);
        }

        private bool SearchRecursive(IBTreeNode node, int value)
        {
            int i = 0;
            while (i < node.Keys.Count && value > node.Keys[i])
            {
                i++;
            }

            if (i < node.Keys.Count && value == node.Keys[i])
            {
                return true;
            }

            if (node.IsLeaf)
            {
                return false;
            }

            return SearchRecursive(node.Children[i], value);
        }

        public void Delete(int value)
        {
            if (_root.Keys.Count == 0)
            {
                return;
            }

            DeleteRecursive(_root, value);

            // Если корень пустой и не является листом, сделать его первого потомка новым корнем
            if (_root.Keys.Count == 0 && !_root.IsLeaf)
            {
                _root = _root.Children[0];
            }
        }

        private void DeleteRecursive(IBTreeNode node, int value)
        {
            int index = FindKey(node, value);

            if (index < node.Keys.Count && node.Keys[index] == value)
            {
                if (node.IsLeaf)
                {
                    node.Keys.RemoveAt(index);
                }
                else
                {
                    DeleteFromNonLeaf(node, index);
                }
            }
            else
            {
                if (node.IsLeaf)
                {
                    return;
                }

                bool isLastChild = (index == node.Keys.Count);

                if (node.Children[index].Keys.Count < degree)
                {
                    FillChild(node, index);
                }

                if (isLastChild && index > node.Keys.Count)
                {
                    DeleteRecursive(node.Children[index - 1], value);
                }
                else
                {
                    DeleteRecursive(node.Children[index], value);
                }
            }
        }

        private void DeleteFromNonLeaf(IBTreeNode node, int index)
        {
            int value = node.Keys[index];

            if (node.Children[index].Keys.Count >= degree)
            {
                int pred = GetPred(node, index);
                node.Keys[index] = pred;
                DeleteRecursive(node.Children[index], pred);
            }
            else if (node.Children[index + 1].Keys.Count >= degree)
            {
                int succ = GetSucc(node, index);
                node.Keys[index] = succ;
                DeleteRecursive(node.Children[index + 1], succ);
            }
            else
            {
                MergeNodes(node, index);
                DeleteRecursive(node.Children[index], value);
            }
        }

        private int FindKey(IBTreeNode node, int value)
        {
            int index = 0;
            while (index < node.Keys.Count && node.Keys[index] < value)
            {
                index++;
            }
            return index;
        }

        private int GetPred(IBTreeNode node, int index)
        {
            IBTreeNode curr = node.Children[index];
            while (!curr.IsLeaf)
            {
                curr = curr.Children[curr.Children.Count - 1];
            }
            return curr.Keys[curr.Keys.Count - 1];
        }

        private int GetSucc(IBTreeNode node, int index)
        {
            IBTreeNode curr = node.Children[index + 1];
            while (!curr.IsLeaf)
            {
                curr = curr.Children[0];
            }
            return curr.Keys[0];
        }

        private void FillChild(IBTreeNode node, int index)
        {
            if (index != 0 && node.Children[index - 1].Keys.Count >= degree)
            {
                BorrowFromPrev(node, index);
            }
            else if (index != node.Keys.Count && node.Children[index + 1].Keys.Count >= degree)
            {
                BorrowFromNext(node, index);
            }
            else
            {
                if (index != node.Keys.Count)
                {
                    MergeNodes(node, index);
                }
                else
                {
                    MergeNodes(node, index - 1);
                }
            }
        }

        private void BorrowFromPrev(IBTreeNode node, int index)
        {
            IBTreeNode child = node.Children[index];
            IBTreeNode sibling = node.Children[index - 1];

            // Сдвигаем все ключи в child на одну позицию вперед
            child.Keys.Insert(0, node.Keys[index - 1]);

            if (!child.IsLeaf)
            {
                child.Children.Insert(0, sibling.Children[sibling.Children.Count - 1]);
                sibling.Children.RemoveAt(sibling.Children.Count - 1);
            }

            node.Keys[index - 1] = sibling.Keys[sibling.Keys.Count - 1];
            sibling.Keys.RemoveAt(sibling.Keys.Count - 1);
        }

        private void BorrowFromNext(IBTreeNode node, int index)
        {
            IBTreeNode child = node.Children[index];
            IBTreeNode sibling = node.Children[index + 1];

            child.Keys.Add(node.Keys[index]);

            if (!child.IsLeaf)
            {
                child.Children.Add(sibling.Children[0]);
                sibling.Children.RemoveAt(0);
            }

            node.Keys[index] = sibling.Keys[0];
            sibling.Keys.RemoveAt(0);
        }

        private void MergeNodes(IBTreeNode node, int index)
        {
            IBTreeNode child = node.Children[index];
            IBTreeNode sibling = node.Children[index + 1];

            child.Keys.Add(node.Keys[index]);

            // Копируем ключи из sibling в child
            for (int i = 0; i < sibling.Keys.Count; i++)
            {
                child.Keys.Add(sibling.Keys[i]);
            }

            if (!child.IsLeaf)
            {
                for (int i = 0; i < sibling.Children.Count; i++)
                {
                    child.Children.Add(sibling.Children[i]);
                }
            }

            node.Keys.RemoveAt(index);
            node.Children.RemoveAt(index + 1);
        }
    }
}
