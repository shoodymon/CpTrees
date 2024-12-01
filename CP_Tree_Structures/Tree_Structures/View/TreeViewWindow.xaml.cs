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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tree_Structures.Model;
using Tree_Structures.ViewModel;

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
            WindowSettings.TreeWindow = this;

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

            this.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            currentTree = trees["Бинарное дерево поиска"];
            treeDrawer = new TreeDrawer();
            bTreeDrawer = new BTreeDrawer();

            //TreeCanvas.Background = (Brush)Application.Current.Resources["CanvasBackgroundBrush"];

            // Привязка обработчиков событий
            AddNodeButton.Click += AddNodeButton_Click;
            DeleteNodeButton.Click += DeleteNodeButton_Click;
            SearchNodeButton.Click += SearchNodeButton_Click;
            BackButton.Click += BackButton_Click;

            // Заполнение ComboBox
            foreach (var treeType in trees.Keys)
            {
                TreeTypeComboBox.Items.Add(treeType);
            }
            TreeTypeComboBox.SelectedItem = "Бинарное дерево поиска";
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await App.WindowManager.SwitchToWindowAsync(App.WindowManager.MainWindow);
        }

        private void ShowErrorNotification(string message)
        {
            var notification = new ErrorNotification();
            notification.SetMessage(message);

            NotificationCanvas.Children.Add(notification);
            Canvas.SetTop(notification, NotificationCanvas.Children.Count * 50);

            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(4) // Ждем перед исчезновением
            };

            fadeOut.Completed += (s, e) => NotificationCanvas.Children.Remove(notification);
            notification.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void ShowSuccessNotification(string message)
        {
            var notification = new SuccessNotification();
            notification.SetMessage(message);

            NotificationCanvas.Children.Add(notification);
            Canvas.SetTop(notification, NotificationCanvas.Children.Count * 50);

            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(4) // Ждем перед исчезновением
            };

            fadeOut.Completed += (s, e) => NotificationCanvas.Children.Remove(notification);
            notification.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                try
                {
                    currentTree.Insert(value);
                    treeDrawer.SetHighlightedNode(null); // Сбрасываем подсветку
                    bTreeDrawer.SetHighlightedNode(null);
                    RedrawTree();
                    ValueTextBox.Clear();
                }
                catch (Exception ex)
                {
                    ShowErrorNotification($"Ошибка при добавлении узла: {ex.Message}");
                }
            }
            else
            {
                ShowErrorNotification("Пожалуйста, введите корректное числовое значение");
            }
        }

        private void DeleteNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                try
                {
                    currentTree.Delete(value);
                    treeDrawer.SetHighlightedNode(null);
                    bTreeDrawer.SetHighlightedNode(null);
                    RedrawTree();
                    ValueTextBox.Clear();
                }
                catch (Exception ex)
                {
                    ShowErrorNotification($"Ошибка при удалении узла: {ex.Message}");
                }
            }
            else
            {
                ShowErrorNotification("Пожалуйста, введите корректное числовое значение");
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
                        ShowSuccessNotification($"Значение {value} найдено в дереве");
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
                        ShowErrorNotification($"Значение {value} не найдено в дереве");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorNotification($"Ошибка при поиске узла: {ex.Message}");
                }
            }
            else
            {
                ShowErrorNotification("Пожалуйста, введите корректное числовое значение");
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
