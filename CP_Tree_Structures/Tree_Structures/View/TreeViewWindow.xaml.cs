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
using System.Windows.Shapes;
using Tree_Structures.Model;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для TreeViewWindow.xaml
    /// </summary>
    public partial class TreeViewWindow : Window
    {
        private ITree currentTree;
        private Dictionary<string, ITree> trees;
        private TreeDrawer treeDrawer;
        private BTreeDrawer bTreeDrawer;
        public TreeViewWindow()
        {
            InitializeComponent();
            trees = new Dictionary<string, ITree>()
            {
                { "Бинарное дерево поиска", new BinarySearchTree() },
                { "АВЛ-дерево", new AVLTree() },
                { "КЧ-дерево", new RedBlackTree() },
                { "Двоичная куча (min)", new MinHeap() },
                { "Двоичная куча (max)", new MaxHeap() },
                { "B-дерево", new BTree() },
                //{ "B+ дерево", new BPlusTree() },
                //{ "2-3-4 дерево", new TwoThreeFourTree() },
            };

            currentTree = trees["Бинарное дерево поиска"];
            treeDrawer = new TreeDrawer();
            bTreeDrawer = new BTreeDrawer();

            //TreeCanvas.Background = (Brush)Application.Current.Resources["CanvasBackgroundBrush"];

            // Привязка обработчиков событий
            AddNodeButton.Click += AddNodeButton_Click;
            DeleteNodeButton.Click += DeleteNodeButton_Click;
            SearchNodeButton.Click += SearchNodeButton_Click;
            BackButton.Click += (s, e) => Close();

            // Заполнение ComboBox
            foreach (var treeType in trees.Keys)
            {
                TreeTypeComboBox.Items.Add(treeType);
            }
            TreeTypeComboBox.SelectedItem = "Бинарное дерево поиска";
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                try
                {
                    currentTree.Insert(value);
                    treeDrawer.SetHighlightedNode(null); // Сбрасываем подсветку
                    RedrawTree();
                    ValueTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении узла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение");
            }
        }

        private void DeleteNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                try
                {
                    currentTree.Delete(value);
                    treeDrawer.SetHighlightedNode(null); // Сбрасываем подсветку
                    RedrawTree();
                    ValueTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении узла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение");
            }
        }

        private void SearchNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                try
                {
                    bool found = currentTree.Search(value);
                    if (found)
                    {
                        if (currentTree is BTree)
                        {
                            bTreeDrawer.SetHighlightedNode(value);
                        }
                        else
                        {
                            treeDrawer.SetHighlightedNode(value);
                        }
                        RedrawTree();
                        MessageBox.Show($"Значение {value} найдено в дереве");
                    }
                    else
                    {
                        if (currentTree is BTree)
                        {
                            bTreeDrawer.SetHighlightedNode(null);
                        }
                        else
                        {
                            treeDrawer.SetHighlightedNode(null);
                        }
                        RedrawTree();
                        MessageBox.Show($"Значение {value} не найдено в дереве");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при поиске узла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение");
            }
        }

        private void RedrawTree()
        {
            TreeCanvas.Children.Clear();

            // Определяем тип текущего дерева и выбираем соответствующий способ отрисовки
            if (currentTree is BTree bTree)
            {
                if (bTreeDrawer == null)
                {
                    bTreeDrawer = new BTreeDrawer();
                }
                bTreeDrawer.DrawTree(bTree, TreeCanvas);
            }
            else
            {
                if (treeDrawer == null)
                {
                    treeDrawer = new TreeDrawer();
                }

                // Настройка цветов для разных типов деревьев
                if (currentTree is BinarySearchTree)
                {
                    treeDrawer.NodeColor = (Brush)Application.Current.Resources["BSTNodeColorBrush"];
                }
                else if (currentTree is AVLTree)
                {
                    treeDrawer.NodeColor = (Brush)Application.Current.Resources["AVLNodeColorBrush"];
                }
                else if (currentTree is RedBlackTree)
                {
                    treeDrawer.NodeColor = (Brush)Application.Current.Resources["RBNodeRedColorBrush"];
                }

                treeDrawer.EllipseColor = (Brush)Application.Current.Resources["EllipseColorBrush"];
                treeDrawer.ArrowColor = (Brush)Application.Current.Resources["ArrowColorBrush"];
                treeDrawer.DrawTree(currentTree, TreeCanvas);
            }
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTreeType = TreeTypeComboBox.SelectedItem as string;
            if (selectedTreeType != null && trees.ContainsKey(selectedTreeType))
            {
                currentTree = trees[selectedTreeType];
                if (currentTree is BTree)
                {
                    bTreeDrawer.SetHighlightedNode(null);
                }
                else
                {
                    treeDrawer.SetHighlightedNode(null);    // Сбрасываем подсветку при смене типа дерева
                }
                RedrawTree();
            }
        }
    }
}
