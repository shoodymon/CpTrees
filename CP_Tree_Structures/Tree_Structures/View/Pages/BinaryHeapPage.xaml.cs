using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tree_Structures.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BinaryHeapPage.xaml
    /// </summary>
    public partial class BinaryHeapPage : Page
    {
        public BinaryHeapPage()
        {
            InitializeComponent();
        }

        private void BHeapCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BHeapCodeEditor.Text = @"
    public class BinaryHeap
    {
        private List<int> list;

        public int heapSize
        {
            get
            {
                return this.list.Count();
            }
        }
    }
    ";
        }

        private void BHeapAddCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BHeapAddCodeEditor.Text = @"
    public void add(int value)
    {
        list.Add(value);
        int i = heapSize - 1;
        int parent = (i - 1) / 2;

        while (i > 0 && list[parent] < list[i])
        {
            int temp = list[i];
            list[i] = list[parent];
            list[parent] = temp;

            i = parent;
            parent = (i - 1) / 2;
        }
    }
    ";
        }

        private void BHeapHeapifyCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BHeapHeapifyCodeEditor.Text = @"
    public void heapify(int i)
    {
        int leftChild;
        int rightChild;
        int largestChild;

        for (; ; )
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            largestChild = i;

            if (leftChild < heapSize && list[leftChild] > list[largestChild]) 
            {
                largestChild = leftChild;
            }

            if (rightChild < heapSize && list[rightChild] > list[largestChild])
            {
                largestChild = rightChild;
            }

            if (largestChild == i) 
            {
                break;
            }

            int temp = list[i];
            list[i] = list[largestChild];
            list[largestChild] = temp;
            i = largestChild;
        }
    }
    ";
        }

        private void BuildBHeapCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BuildBHeapCodeEditor.Text = @"
    public void buildHeap(int[] sourceArray)
    {
        list = sourceArray.ToList();
        for (int i = heapSize / 2; i >= 0; i--)
        {
            heapify(i);
        }
    }
    ";
        }

        private void MaxBHeapCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            MaxBHeapCodeEditor.Text = @"
    public int getMax()
    {
        int result = list[0];
        list[0] = list[heapSize - 1];
        list.RemoveAt(heapSize - 1);
        return result;
    }
    ";
        }

        private void SortBHeapCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            SortBHeapCodeEditor.Text = @"
    public void heapSort(int[] array)
    {
        buildHeap(array);
        for (int i = array.Length - 1; i >= 0; i--)
        {
            array[i] = getMax();
            heapify(0);
        }
    }
    ";
        }
    }
}
