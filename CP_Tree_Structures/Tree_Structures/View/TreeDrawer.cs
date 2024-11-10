using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using Tree_Structures.Model;

namespace Tree_Structures.View
{
    public class TreeDrawer
    {
        private readonly Dictionary<string, Brush> nodeBrushes;
        private int? highlightedValue; // Значение подсвеченного узла

        public Brush NodeColor { get; internal set; }
        public Brush EllipseColor { get; internal set; }
        public Brush ArrowColor { get; internal set; }
        public Brush HighlightColor { get; set; } = Brushes.Red; // Цвет подсветки

        public TreeDrawer()
        {
            nodeBrushes = new Dictionary<string, Brush>
            {
                { "BinarySearchTree", (Brush)Application.Current.Resources["BSTNodeColorBrush"] },
                { "AVLTree", (Brush)Application.Current.Resources["AVLNodeColorBrush"] },
                { "RedBlackTreeRed", (Brush)Application.Current.Resources["RBNodeRedColorBrush"] },
                { "RedBlackTreeBlack", (Brush)Application.Current.Resources["RBNodeBlackColorBrush"] },
                { "MinHeap", (Brush)Application.Current.Resources["MinHeapNodeColorBrush"] },
                { "MaxHeap", (Brush)Application.Current.Resources["MaxHeapNodeColorBrush"] },
            };
        }

        public void SetHighlightedNode(int? value)
        {
            highlightedValue = value;
        }

        public void DrawTree(ITree tree, Canvas canvas)
        {
            canvas.Children.Clear();
            //canvas.Background = (Brush)Application.Current.Resources["CanvasBackgroundBrush"];
            if (tree.root != null)
            {
                DrawNodeRecursive(tree.root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, tree, canvas);
            }
        }

        private void DrawNodeRecursive(ITreeNode node, double x, double y, double offsetX, double offsetY, ITree tree, Canvas canvas)
        {
            string treeType = tree.GetType().Name; // Получаем имя типа дерева

            // Выбор цвета узла в зависимости от типа дерева
            Brush nodeBrush = nodeBrushes.ContainsKey(treeType) ? nodeBrushes[treeType] : Brushes.Gray;

            // Если это красно-черное дерево, проверим цвет узла
            if (treeType == "RedBlackTree" && node is RBNode rbNode)
            {
                nodeBrush = rbNode.NodeColor == RBNode.Color.Black ? nodeBrushes["RedBlackTreeBlack"] : nodeBrushes["RedBlackTreeRed"];
            }

            DrawNode(node.Value, x, y, nodeBrush, canvas, node.Value == highlightedValue);

            if (node.Left != null)
            {
                DrawNodeRecursive(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, tree, canvas);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
            }
            if (node.Right != null)
            {
                DrawNodeRecursive(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, tree, canvas);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
            }
        }

        private void DrawNode(int nodeValue, double x, double y, Brush brush, Canvas canvas, bool isHighlighted)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 35,
                Height = 35,
                Stroke = isHighlighted ? HighlightColor : (Brush)Application.Current.Resources["EllipseColorBrush"],
                StrokeThickness = isHighlighted ? 3 : 2, // Увеличиваем толщину обводки для подсвеченного узла
                Fill = brush
            };
            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
            Canvas.SetTop(ellipse, y);
            canvas.Children.Add(ellipse);

            TextBlock text = new TextBlock
            {
                Text = nodeValue.ToString(),
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Foreground = (Brush)Application.Current.Resources["NodeTextColorBrush"],
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            // Измеряем размеры текста
            text.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));

            // Вычисляем позицию для точного центрирования
            double textX = x - text.ActualWidth / 2;
            double textY = y + (ellipse.Height - text.ActualHeight) / 2;

            Canvas.SetLeft(text, textX);
            Canvas.SetTop(text, textY);
            canvas.Children.Add(text);
        }

        private void DrawArrow(double x1, double y1, double x2, double y2, double arrowLength, Canvas canvas)
        {
            Line line = new Line
            {
                Stroke = (Brush)Application.Current.Resources["ArrowColorBrush"],
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = 2
            };
            canvas.Children.Add(line);

            double theta = Math.Atan2(y2 - y1, x2 - x1);
            double phi = Math.PI / 6;

            Polygon arrowHead = new Polygon
            {
                Fill = (Brush)Application.Current.Resources["ArrowColorBrush"],
                Points = new PointCollection
                {
                    new Point(x2, y2),
                    new Point(x2 - arrowLength * Math.Cos(theta - phi), y2 - arrowLength * Math.Sin(theta - phi)),
                    new Point(x2 - arrowLength * Math.Cos(theta + phi), y2 - arrowLength * Math.Sin(theta + phi))
                }
            };
            canvas.Children.Add(arrowHead);
        }
    }
}
