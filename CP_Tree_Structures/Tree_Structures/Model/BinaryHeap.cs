using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public class HeapNode : ITreeNode
    {
        public int Value { get; set; }
        public ITreeNode Left { get; set; }
        public ITreeNode Right { get; set; }

        public HeapNode(int value) { Value = value; }

    }

    public abstract class BinaryHeapBase : ITree
    {
        protected List<int> heap;
        protected HeapNode rootNode;

        public ITreeNode root => rootNode;

        protected BinaryHeapBase()
        {
            heap = new List<int>();
        }

        public abstract void Insert(int value);
        public abstract void Delete(int value);

        public bool Search(int value)
        {
            return heap.Contains(value);
        }

        protected abstract void SiftUp(int index);
        protected abstract void SiftDown(int index);

        protected void RebuildTree()
        {
            if (heap.Count == 0)
            {
                rootNode = null;
                return;
            }

            rootNode = new HeapNode(heap[0]);
            BuildTreeRecursive(0, rootNode);
        }

        private void BuildTreeRecursive(int index, HeapNode node)
        {
            int leftIndex = 2 * index + 1;
            int rightIndex = 2 * index + 2;

            if (leftIndex < heap.Count)
            {
                node.Left = new HeapNode(heap[leftIndex]);
                BuildTreeRecursive(leftIndex, (HeapNode)node.Left);
            }

            if (rightIndex < heap.Count)
            {
                node.Right = new HeapNode(heap[rightIndex]);
                BuildTreeRecursive(rightIndex, (HeapNode)node.Right);
            }
        }

        public int[] GetHeapArray() => heap.ToArray();
    }

    public class MinHeap : BinaryHeapBase
    {
        public override void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);
            RebuildTree();
        }

        public override void Delete(int value)
        {
            int index = heap.IndexOf(value);
            if (index != -1)
            {
                heap[index] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);

                if (index < heap.Count)
                {
                    SiftDown(index);
                    SiftUp(index);
                }

                RebuildTree();
            }
        }

        protected override void SiftUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[parentIndex] > heap[index])
                {
                    // Swap with parent
                    int temp = heap[index];
                    heap[index] = heap[parentIndex];
                    heap[parentIndex] = temp;
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        protected override void SiftDown(int index)
        {
            while (true)
            {
                int minIndex = index;
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;

                if (leftChild < heap.Count && heap[leftChild] < heap[minIndex])
                {
                    minIndex = leftChild;
                }

                if (rightChild < heap.Count && heap[rightChild] < heap[minIndex])
                {
                    minIndex = rightChild;
                }

                if (minIndex == index)
                {
                    break;
                }

                int temp = heap[index];
                heap[index] = heap[minIndex];
                heap[minIndex] = temp;
                index = minIndex;
            }
        }
    }

    public class MaxHeap : BinaryHeapBase
    {
        public override void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);
            RebuildTree();
        }

        public override void Delete(int value)
        {
            int index = heap.IndexOf(value);
            if (index != -1)
            {
                heap[index] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);

                if (index < heap.Count)
                {
                    SiftDown(index);
                    SiftUp(index);
                }

                RebuildTree();
            }
        }

        protected override void SiftUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[parentIndex] < heap[index])  // 1
                {
                    // Swap 
                    int temp = heap[index];
                    heap[index] = heap[parentIndex];
                    heap[parentIndex] = temp;
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        protected override void SiftDown(int index)
        {
            while (true)
            {
                int maxIndex = index;
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;

                if (leftChild < heap.Count && heap[leftChild] > heap[maxIndex])  // 1
                {
                    maxIndex = leftChild;
                }

                if (rightChild < heap.Count && heap[rightChild] > heap[maxIndex])  // 1
                {
                    maxIndex = rightChild;
                }

                if (maxIndex == index)
                {
                    break;
                }

                int temp = heap[index];
                heap[index] = heap[maxIndex];
                heap[maxIndex] = temp;
                index = maxIndex;
            }
        }
    }
}
