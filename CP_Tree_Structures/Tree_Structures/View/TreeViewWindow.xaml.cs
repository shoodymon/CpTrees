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
        public TreeViewWindow()
        {
            InitializeComponent();
            trees = new Dictionary<string, ITree>()
            {
                { "Бинарное дерево поиска", new BinarySearchTree() },
                { "АВЛ-дерево", new AVLTree() },
                { "КЧ-дерево", new RedBlackTree() },
                //{ "B-дерево", new BTree() },
                //{ "B+ дерево", new BPlusTree() },
                //{ "2-3-4 дерево", new TwoThreeFourTree() },
            };

            currentTree = trees["Бинарное дерево поиска"];
            treeDrawer = new TreeDrawer();

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
                        // Устанавливаем подсветку найденного узла
                        treeDrawer.SetHighlightedNode(value);
                        RedrawTree();
                        MessageBox.Show($"Значение {value} найдено в дереве");
                    }
                    else
                    {
                        // Сбрасываем подсветку если узел не найден
                        treeDrawer.SetHighlightedNode(null);
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
            if (treeDrawer == null)
            {
                treeDrawer = new TreeDrawer();
            }
            // Очистка холста перед отрисовкой
            TreeCanvas.Children.Clear();
            // Визуализация текущего дерева
            treeDrawer.DrawTree(currentTree, TreeCanvas);

            // Применяем цвета для разных типов деревьев
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

            // Применяем цвета для других элементов, например, стрелок и обводок
            treeDrawer.EllipseColor = (Brush)Application.Current.Resources["EllipseColorBrush"];
            treeDrawer.ArrowColor = (Brush)Application.Current.Resources["ArrowColorBrush"];
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTreeType = TreeTypeComboBox.SelectedItem as string;
            if (selectedTreeType != null && trees.ContainsKey(selectedTreeType))
            {
                currentTree = trees[selectedTreeType];
                treeDrawer.SetHighlightedNode(null); // Сбрасываем подсветку при смене типа дерева
                RedrawTree();
            }
        }
    }
}
