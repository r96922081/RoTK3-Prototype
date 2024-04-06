using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UiEditor
{
    public abstract class Drawable
    {
        private UIElement ui_element;

        public UIElement UiElement { get => ui_element; set => ui_element = value; }

        public Drawable(UIElement ui_element)
        {
            this.ui_element = ui_element;
        }

        public void Show()
        {
            ui_element.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            ui_element.Visibility = Visibility.Hidden;
        }

        public virtual void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(ui_element);
        }

        public virtual void RemoveFromCanvas(Canvas canvas)
        {
            canvas.Children.Remove(ui_element);
        }

        public virtual void Shift(int diff_x, int diff_y)
        {
            Canvas.SetTop(ui_element, Canvas.GetTop(ui_element) + diff_y);
            Canvas.SetLeft(ui_element, Canvas.GetLeft(ui_element) + diff_x);
        }

        public virtual void MoveTo(int x, int y)
        {
            Canvas.SetTop(ui_element, y);
            Canvas.SetLeft(ui_element, x);
        }

        public virtual void SetZIndex(int z_index)
        {
            Canvas.SetZIndex(ui_element, z_index);
        }

        public abstract UiEditorRect GetBoundingRect();

        public double GetTop()
        {
            return GetBoundingRect().Top;
        }

        public double GetLeft()
        {
            return GetBoundingRect().Left;
        }

        public abstract string GetName();

        public abstract void SetName(string name);

        public override string ToString() { return GetName(); }

        public abstract Drawable Clone();

        public abstract void Save(SaveData save_data);

        public abstract string GetDefaultNamePrefix();

        public abstract void SetFill(Brush brush);

        public virtual void SetHeight(int height) { }

        public virtual void SetWidth(int width) { }

        public abstract void SetTop(double top);

        public abstract void SetLeft(double left);

        public virtual void SetTextAlignment(TextAlignment align) { }

        public virtual void SetText(String text) { }

        public virtual void SetTextColor(Brush color) { }

        public virtual void SetFontFamily(FontFamily font_family) { }

        public virtual void SetBold(bool enabled) { }

        public virtual void SetFontSize(int size) { }

        public virtual void SetBorderSize(int size) { }

        public virtual void SetBorderColor(Brush color) { }

        public virtual bool GetTextAlignment(ref TextAlignment align) { return false; }

        public virtual bool GetFontSize(ref double font_size) { return false; }

        public virtual bool GetBold(ref bool enabled) { return false; }

        public virtual bool GetFontFamily(ref FontFamily family) { return false; }

        public virtual bool GetTextColor(ref Brush color) { return false; }

        public virtual bool GetText(ref string text) { return false; }

        public virtual bool GetFillColor(ref Brush brush) { return false; }

        public virtual bool GetFilled(ref bool filled) { return false; }

        public virtual bool GetBorderSize(ref double border_size) { return false; }

        public virtual bool GetBorderColor(ref Brush color) { return false; }
    }

    public class TextDrawable : Drawable
    {
        public TextBlock text_block;

        public TextDrawable(TextBlock text_block) : base(text_block)
        {
            this.text_block = text_block;
        }

        public TextDrawable(UIElement ui_element) : base(ui_element)
        {
        }

        public override UiEditorRect GetBoundingRect()
        {
            return new UiEditorRect(Canvas.GetTop(text_block), Canvas.GetLeft(text_block), text_block.Width, text_block.Height);
        }

        public override Drawable Clone()
        {
            return new TextDrawable(new UiEditorText(text_block).GetWpfUiElement());
        }

        public override string GetName()
        {
            return text_block.Name;
        }

        public override void SetName(string name)
        {
            text_block.Name = name;
        }

        public override string GetDefaultNamePrefix()
        {
            return "text_";
        }

        public override void SetFill(Brush brush)
        {
            text_block.Background = brush;
        }

        public override void SetHeight(int height)
        {
            text_block.Height = height;
        }

        public override void SetWidth(int width)
        {
            text_block.Width = width;
        }

        public override void SetTextAlignment(TextAlignment align)
        {
            text_block.TextAlignment = align;
        }

        public override void SetText(String text)
        {
            text_block.Text = text;
        }

        public override void SetTextColor(Brush color)
        {
            text_block.Foreground = color;
        }

        public override void SetFontFamily(FontFamily font_family)
        {
            text_block.FontFamily = font_family;
        }

        public override void SetBold(bool enabled)
        {
            if (enabled)
                text_block.FontWeight = FontWeights.Bold;
            else
                text_block.FontWeight = FontWeights.Normal;
        }

        public override void SetFontSize(int size)
        {
            text_block.FontSize = size;
        }

        public override bool GetTextAlignment(ref TextAlignment align)
        {
            align = text_block.TextAlignment;
            return true;
        }

        public override bool GetFontSize(ref double font_size)
        {
            font_size = text_block.FontSize;
            return true;
        }

        public override bool GetBold(ref bool enabled)
        {
            enabled = text_block.FontWeight == FontWeights.Bold;
            return true;
        }

        public override bool GetFontFamily(ref FontFamily family)
        {
            family = text_block.FontFamily;
            return true;
        }

        public override bool GetTextColor(ref Brush color)
        {
            color = text_block.Foreground;
            return true;
        }

        public override bool GetText(ref string text)
        {
            text = text_block.Text;
            return true;
        }

        public override void Save(SaveData save_data)
        {
            save_data.UiEditorTexts.Add(new UiEditorText(text_block));
        }

        public override bool GetFillColor(ref Brush brush)
        {
            brush = text_block.Background;
            return true;
        }

        public override bool GetFilled(ref bool filled)
        {
            filled = text_block.Background != null;
            return true;
        }

        public override void SetTop(double top)
        {
            Canvas.SetTop(text_block, top);
        }

        public override void SetLeft(double left)
        {
            Canvas.SetLeft(text_block, left);
        }

        public override void SetBorderColor(Brush color)
        {
            base.SetBorderColor(color);
        }

        public override void SetBorderSize(int size)
        {
            base.SetBorderSize(size);
        }
    }

    public class RectangleDrawable : Drawable
    {
        public Rectangle rect;

        public RectangleDrawable(Rectangle rect) : base(rect)
        {
            this.rect = rect;
        }

        public override UiEditorRect GetBoundingRect()
        {
            return new UiEditorRect(Canvas.GetTop(rect), Canvas.GetLeft(rect), rect.Width, rect.Height);
        }

        public override Drawable Clone()
        {
            return new RectangleDrawable(new UiEditorRect(rect).GetWpfUiElement());
        }

        public override string GetName()
        {
            return rect.Name;
        }

        public override void SetName(string name)
        {
            rect.Name = name;
        }

        public override string GetDefaultNamePrefix()
        {
            return "rect_";
        }

        public override void SetFill(Brush brush)
        {
            rect.Fill = brush;
        }

        public override void SetHeight(int height)
        {
            rect.Height = height;
        }

        public override void SetWidth(int width)
        {
            rect.Width = width;
        }

        public override void Save(SaveData save_data)
        {
            save_data.UiEditorRects.Add(new UiEditorRect(rect));
        }

        public override bool GetFillColor(ref Brush brush)
        {
            brush = rect.Fill;
            return true;
        }

        public override bool GetFilled(ref bool filled)
        {
            filled = rect.Fill != null;
            return true;
        }

        public override void SetTop(double top)
        {
            Canvas.SetTop(rect, top);
        }

        public override void SetLeft(double left)
        {
            Canvas.SetLeft(rect, left);
        }

        public override void SetBorderSize(int size)
        {
            rect.StrokeThickness = size;
        }

        public override bool GetBorderSize(ref double border_size)
        {
            border_size = rect.StrokeThickness;
            return true;
        }

        public override void SetBorderColor(Brush color)
        {
            rect.Stroke = color;
        }

        public override bool GetBorderColor(ref Brush color)
        {
            color = rect.Stroke;
            return true;
        }
    }

    public class PolygonDrawable : Drawable
    {
        internal Polygon polygon;

        public PolygonDrawable(Polygon polygon) : base(polygon)
        {
            this.polygon = polygon;
            Canvas.SetTop(polygon, 0);
            Canvas.SetLeft(polygon, 0);
        }

        public override UiEditorRect GetBoundingRect()
        {
            double left2 = double.MaxValue;
            double top2 = double.MaxValue;
            double right2 = double.MinValue;
            double bottom2 = double.MinValue;

            foreach (var p in polygon.Points)
            {
                if (p.X < left2)
                    left2 = p.X;
                if (p.X > right2)
                    right2 = p.X;
                if (p.Y < top2)
                    top2 = p.Y;
                if (p.Y > bottom2)
                    bottom2 = p.Y;
            }

            double width = right2 - left2;
            double height = bottom2 - top2;

            top2 += (int)Canvas.GetTop(polygon);
            left2 += (int)Canvas.GetLeft(polygon);

            return new UiEditorRect(top2, left2, width, height);
        }

        public override Drawable Clone()
        {
            return new PolygonDrawable(new UiEditorPolygon(polygon).GetWpfUiElement());
        }

        public override string GetName()
        {
            return polygon.Name;
        }

        public override void SetName(string name)
        {
            polygon.Name = name;
        }

        public override string GetDefaultNamePrefix()
        {
            return "polygon_";
        }

        public override void SetFill(Brush brush)
        {
            polygon.Fill = brush;
        }

        public override bool GetFillColor(ref Brush brush)
        {
            brush = polygon.Fill;
            return true;
        }

        public override bool GetFilled(ref bool filled)
        {
            filled = polygon.Fill != null;
            return true;
        }

        public override void Shift(int diff_x, int diff_y)
        {
            PointCollection points = new PointCollection();
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                points.Add(new Point(polygon.Points[i].X + diff_x, polygon.Points[i].Y + diff_y));
            }

            polygon.Points = points;
        }

        public override void SetTop(double top)
        {
            var rect = GetBoundingRect();
            int diff_y = (int)(top - rect.Top);
            Shift(0, diff_y);
        }

        public override void SetLeft(double left)
        {
            var rect = GetBoundingRect();
            int diff_x = (int)(left - rect.Left);
            Shift(diff_x, 0);
        }

        public override void MoveTo(int x, int y)
        {
            SetTop(y);
            SetLeft(x);
        }

        public override void SetBorderSize(int size)
        {
            polygon.StrokeThickness = size;
        }

        public override bool GetBorderSize(ref double border_size)
        {
            border_size = polygon.StrokeThickness;
            return true;
        }

        public override void SetBorderColor(Brush color)
        {
            polygon.Stroke = color;
        }

        public override bool GetBorderColor(ref Brush color)
        {
            color = polygon.Stroke;
            return true;
        }

        public override void Save(SaveData save_data)
        {
            save_data.UiEditorPolygons.Add(new UiEditorPolygon(polygon));
        }
    }
}
