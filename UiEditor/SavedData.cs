using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UiEditor
{
    public class UiEditorRect
    {
        public UiEditorRect() { }
        public UiEditorRect(double top, double left, double width, double height)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }

        public UiEditorRect(Rectangle r)
        {
            top = Canvas.GetTop(r);
            left = Canvas.GetLeft(r);
            width = r.Width;
            height = r.Height;
            fill_color = r.Fill;
            thickness = r.StrokeThickness;
            stroke = r.Stroke;
            name = r.Name;
        }

        public Rectangle GetWpfUiElement()
        {
            Rectangle rect = new Rectangle();
            rect.Name = name;
            rect.Fill = FillColor;
            Canvas.SetTop(rect, top);
            Canvas.SetLeft(rect, left);
            rect.Width = width;
            rect.Height = height;
            Canvas.SetRight(rect, left + width);
            Canvas.SetBottom(rect, top + height);
            rect.StrokeThickness = thickness;
            rect.Stroke = stroke;

            return rect;
        }

        private string name;
        private double top;
        private double left;
        private double width;
        private double height;
        private Brush fill_color;
        private double thickness;
        private Brush stroke;

        public string Name { get => name; set => name = value; }
        public double Top { get => top; set => top = value; }
        public double Left { get => left; set => left = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public Brush FillColor { get => fill_color; set => fill_color = value; }
        public double Thickness { get => thickness; set => thickness = value; }
        public Brush Stroke { get => stroke; set => stroke = value; }
    }

    public class UiEditorText
    {
        private string name;
        string text;
        Brush text_brush;
        FontFamily font_family;
        double font_size;
        bool bold;
        TextAlignment align;
        Brush bg_brush;
        double top;
        double left;
        double width;
        double height;

        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
        public Brush TextBrush { get => text_brush; set => text_brush = value; }
        public FontFamily FontFamily { get => font_family; set => font_family = value; }
        public double FontSize { get => font_size; set => font_size = value; }
        public TextAlignment Align { get => align; set => align = value; }
        public Brush BgBrush { get => bg_brush; set => bg_brush = value; }
        public double Top { get => top; set => top = value; }
        public double Left { get => left; set => left = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public bool Bold { get => bold; set => bold = value; }

        public UiEditorText()
        {
        }

        public UiEditorText(TextBlock text_block)
        {
            name = text_block.Name;
            text = text_block.Text.ToString();
            text_brush = text_block.Foreground;
            font_family = text_block.FontFamily;
            font_size = text_block.FontSize;
            bold = (text_block.FontWeight == FontWeights.Bold);
            align = text_block.TextAlignment;
            bg_brush = text_block.Background;
            top = Canvas.GetTop(text_block);
            left = Canvas.GetLeft(text_block);
            width = text_block.Width;
            height = text_block.Height;
        }

        public TextBlock GetWpfUiElement()
        {
            TextBlock text_block = new TextBlock();
            text_block.Name = name;
            text_block.Text = text;
            text_block.Foreground = text_brush;
            text_block.FontFamily = font_family;
            text_block.FontSize = font_size;
            if (bold)
                text_block.FontWeight = FontWeights.Bold;
            else
                text_block.FontWeight = FontWeights.Normal;
            text_block.TextAlignment = align;
            text_block.Background = bg_brush;
            Canvas.SetTop(text_block, top);
            Canvas.SetLeft(text_block, left);
            text_block.Width = width;
            text_block.Height = height;
            Canvas.SetRight(text_block, left + width);
            Canvas.SetBottom(text_block, top + height);

            return text_block;
        }
    }

    public class UiEditorPolygon
    {
        private string name;
        double top;
        double left;
        double width;
        double height;
        double thickness;
        PointCollection points = new PointCollection();
        Brush stroke;
        Brush fill_color;

        public string Name { get => name; set => name = value; }
        public double Top { get => top; set => top = value; }
        public double Left { get => left; set => left = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public double Thickness { get => thickness; set => thickness = value; }
        public PointCollection Points { get => points; set => points = value; }
        public Brush Stroke { get => stroke; set => stroke = value; }
        public Brush FillColor { get => fill_color; set => fill_color = value; }

        public UiEditorPolygon()
        {
        }

        public UiEditorPolygon(Polygon p)
        {
            name = p.Name;
            top = Canvas.GetTop(p);
            left = Canvas.GetLeft(p);
            width = p.Width;
            height = p.Height;

            thickness = p.StrokeThickness;
            points.Clear();
            foreach (var p2 in p.Points)
                points.Add(new Point(p2.X, p2.Y));

            stroke = p.Stroke;
            fill_color = p.Fill;
        }

        public Polygon GetWpfUiElement()
        {
            Polygon p = new Polygon();
            p.Name = name;
            Canvas.SetTop(p, top);
            Canvas.SetLeft(p, left);
            p.Width = width;
            p.Height = height;
            Canvas.SetRight(p, left + width);
            Canvas.SetBottom(p, top + height);

            p.StrokeThickness = thickness;
            foreach (var p2 in points)
                p.Points.Add(new Point(p2.X, p2.Y));
            p.Stroke = stroke;
            p.Fill = fill_color;

            return p;
        }
    }

    public class SaveData
    {
        private List<UiEditorText> ui_editor_texts = new List<UiEditorText>();
        private List<UiEditorPolygon> ui_editor_polygons = new List<UiEditorPolygon>();
        private List<UiEditorRect> ui_editor_rects = new List<UiEditorRect>();
        public List<UiEditorText> UiEditorTexts { get => ui_editor_texts; set => ui_editor_texts = value; }
        public List<UiEditorRect> UiEditorRects { get => ui_editor_rects; set => ui_editor_rects = value; }
        public List<UiEditorPolygon> UiEditorPolygons { get => ui_editor_polygons; set => ui_editor_polygons = value; }
    }
}
