using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System;
using Tree_Structures.Model;

public class BTreeDrawer
{
    private readonly Dictionary<string, Brush> nodeBrushes;
    private int? highlightedValue;
    private readonly int degree = 2;
    private const double VERTICAL_SPACING = 120;
    private const double NODE_HEIGHT = 35;
    private const double ROOT_VERTICAL_OFFSET = 20;

    public BTreeDrawer(int degree = 2)
    {
        this.degree = degree;
        nodeBrushes = new Dictionary<string, Brush>
        {
            { "BTree", (Brush)Application.Current.Resources["BTreeNodeColorBrush"] }
        };
    }

    public void SetHighlightedNode(int? value)
    {
        highlightedValue = value;
    }

    public void DrawTree(BTree tree, Canvas canvas)
    {
        canvas.Children.Clear();
        if (tree.root == null) return;

        // Вычисляем все позиции узлов перед отрисовкой
        var nodePositions = new Dictionary<IBTreeNode, Point>();
        double cellWidth = 35;
        double totalWidth = CalculateRequiredWidth(tree.root as IBTreeNode);

        // Распределяем позиции, начиная с листовых узлов
        AssignNodePositions(tree.root as IBTreeNode, 0, 0, canvas.ActualWidth, nodePositions);

        // Отрисовываем узлы, используя предварительно вычисленные позиции
        DrawTreeWithPositions(tree.root as IBTreeNode, nodePositions, cellWidth, canvas);
    }

    private double CalculateRequiredWidth(IBTreeNode node)
    {
        if (node == null) return 0;
        if (node.IsLeaf) return node.Keys.Count * 35 + 20; // Базовая ширина узла

        double childrenWidth = 0;
        foreach (var child in node.Children)
        {
            childrenWidth += CalculateRequiredWidth(child);
        }
        return Math.Max(node.Keys.Count * 35 + 20, childrenWidth);
    }

    private void AssignNodePositions(IBTreeNode node, int depth, double left, double right,
        Dictionary<IBTreeNode, Point> positions)
    {
        if (node == null) return;

        double x = (left + right) / 2;
        double y = depth * VERTICAL_SPACING + (depth == 0 ? ROOT_VERTICAL_OFFSET : 0); // Добавляем отступ для корня
        positions[node] = new Point(x, y);

        if (!node.IsLeaf)
        {
            double segmentWidth = (right - left) / node.Children.Count;
            for (int i = 0; i < node.Children.Count; i++)
            {
                double childLeft = left + (i * segmentWidth);
                double childRight = childLeft + segmentWidth;
                AssignNodePositions(node.Children[i], depth + 1, childLeft, childRight, positions);
            }
        }
    }

    private void DrawTreeWithPositions(IBTreeNode node, Dictionary<IBTreeNode, Point> positions,
        double cellWidth, Canvas canvas)
    {
        if (node == null) return;

        var position = positions[node];
        int maxKeys = (2 * degree) - 1;
        double nodeWidth = cellWidth * maxKeys;

        // Рисуем текущий узел
        DrawNode(node.Keys, maxKeys, position.X, position.Y, nodeWidth,
            nodeBrushes["BTree"], canvas, cellWidth);

        // Рисуем стрелки к дочерним узлам
        if (!node.IsLeaf)
        {
            foreach (var child in node.Children)
            {
                var childPos = positions[child];
                DrawArrow(position.X, position.Y + NODE_HEIGHT,
                    childPos.X, childPos.Y, 10, canvas);
                DrawTreeWithPositions(child, positions, cellWidth, canvas);
            }
        }
    }

    private void DrawNode(List<int> values, int maxCells, double x, double y,
        double width, Brush brush, Canvas canvas, double cellWidth)
    {
        Rectangle rect = new Rectangle
        {
            Width = width,
            Height = NODE_HEIGHT,
            Stroke = (Brush)Application.Current.Resources["EllipseColorBrush"],
            StrokeThickness = 2,
            Fill = brush,
            RadiusX = 5,
            RadiusY = 5
        };

        Canvas.SetLeft(rect, x - width / 2);
        Canvas.SetTop(rect, y);
        canvas.Children.Add(rect);

        // Рисуем разделители
        for (int i = 1; i < maxCells; i++)
        {
            Line separator = new Line
            {
                X1 = x - (width / 2) + (i * cellWidth),
                Y1 = y,
                X2 = x - (width / 2) + (i * cellWidth),
                Y2 = y + NODE_HEIGHT,
                Stroke = (Brush)Application.Current.Resources["EllipseColorBrush"],
                StrokeThickness = 1
            };
            canvas.Children.Add(separator);
        }

        // Отрисовка значений и пустых ячеек
        for (int i = 0; i < maxCells; i++)
        {
            bool hasValue = i < values.Count;

            if (!hasValue)
            {
                Rectangle emptyCell = new Rectangle
                {
                    Width = cellWidth,
                    Height = NODE_HEIGHT,
                    Fill = Brushes.LightGray,
                    Opacity = 0.3
                };

                Canvas.SetLeft(emptyCell, x - (width / 2) + (i * cellWidth));
                Canvas.SetTop(emptyCell, y);
                canvas.Children.Add(emptyCell);
            }
            else
            {
                TextBlock text = new TextBlock
                {
                    Text = values[i].ToString(),
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Foreground = (Brush)Application.Current.Resources["NodeTextColorBrush"],
                    TextAlignment = TextAlignment.Center
                };

                text.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                double textX = x - (width / 2) + (i * cellWidth) +
                    (cellWidth - text.DesiredSize.Width) / 2;
                double textY = y + (NODE_HEIGHT - text.DesiredSize.Height) / 2;

                Canvas.SetLeft(text, textX);
                Canvas.SetTop(text, textY);
                canvas.Children.Add(text);

                if (highlightedValue.HasValue && values[i] == highlightedValue.Value)
                {
                    Rectangle highlight = new Rectangle
                    {
                        Width = cellWidth,
                        Height = NODE_HEIGHT,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                        Fill = Brushes.Transparent,
                        RadiusX = 5,
                        RadiusY = 5
                    };

                    Canvas.SetLeft(highlight, x - (width / 2) + (i * cellWidth));
                    Canvas.SetTop(highlight, y);
                    canvas.Children.Add(highlight);
                }
            }
        }
    }

    private void DrawArrow(double x1, double y1, double x2, double y2,
        double arrowLength, Canvas canvas)
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
                new Point(
                    x2 - arrowLength * Math.Cos(theta - phi),
                    y2 - arrowLength * Math.Sin(theta - phi)
                ),
                new Point(
                    x2 - arrowLength * Math.Cos(theta + phi),
                    y2 - arrowLength * Math.Sin(theta + phi)
                )
            }
        };
        canvas.Children.Add(arrowHead);
    }
}