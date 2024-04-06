using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UiEditor
{
    public abstract class GameViewStrategy
    {
        public Canvas canvas;
        public double thickness = 1;
        public SolidColorBrush stroke = System.Windows.Media.Brushes.Black;

        public GameViewStrategy(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public virtual void MouseMove(object sender, MouseEventArgs e, int x, int y)
        {
            Gv.ui_editor_window.status_text.Text = String.Format("x: {0, -6}y = {1, -6}", (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y);
        }

        public abstract void MouseUp(object sender, MouseButtonEventArgs e, int x, int y);
        public abstract void MouseDown(object sender, MouseButtonEventArgs e, int x, int y);

        public void KeyDown(KeyEventArgs key)
        {
            if (key.Key == Key.Delete)
            {
                if (Gv.ui_editor_window.selections.Count > 0)
                {
                    List<Drawable> l = new List<Drawable>();

                    foreach (var s in Gv.ui_editor_window.selections)
                        l.Add(s);

                    foreach (var s in l)
                    {
                        Gv.ui_editor_window.DeleteDrawable(s);
                    }

                    Gv.ui_editor_window.selections.Clear();
                    Gv.ui_editor_window.update_selections();
                }
            }
            else if (key.Key == Key.Right)
            {
                foreach (var p in Gv.ui_editor_window.selections)
                    p.Shift(1, 0);

                Gv.ui_editor_window.update_selections();
            }
            else if (key.Key == Key.Left)
            {
                foreach (var p in Gv.ui_editor_window.selections)
                    p.Shift(-1, 0);

                Gv.ui_editor_window.update_selections();
            }
            else if (key.Key == Key.Up)
            {
                foreach (var p in Gv.ui_editor_window.selections)
                    p.Shift(0, -1);

                Gv.ui_editor_window.update_selections();
            }
            else if (key.Key == Key.Down)
            {
                foreach (var p in Gv.ui_editor_window.selections)
                    p.Shift(0, +1);

                Gv.ui_editor_window.update_selections();
            }
            else if (key.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Gv.ui_editor_window.copys.Clear();

                CopySelectionsToCopy();
            }
            else if (key.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Gv.ui_editor_window.selections.Clear();

                foreach (var c in Gv.ui_editor_window.copys)
                {
                    Gv.ui_editor_window.AddNewDrawable(c, c.GetDefaultNamePrefix());
                    Gv.ui_editor_window.selections.Add(c);
                }

                CopySelectionsToCopy();
                Gv.ui_editor_window.update_selections();
            }
        }

        private void CopySelectionsToCopy()
        {
            Gv.ui_editor_window.copys.Clear();

            foreach (var s in Gv.ui_editor_window.selections)
            {
                var s2 = s.Clone();
                s2.SetLeft(s2.GetLeft() + 20);
                s2.SetTop(s2.GetTop() + 20);
                Gv.ui_editor_window.copys.Add(s2);
            }
        }

        public abstract void Reset();
    }

    public class PolygonStrategy : GameViewStrategy
    {
        public Point prev_point;
        public Line prev_line;
        public List<Point> points;
        public List<Line> lines;
        bool first;

        public PolygonStrategy(Canvas canvas) : base(canvas)
        {
            prev_line = new Line();
            prev_point = new Point();
            points = new List<Point>();
            lines = new List<Line>();

            first = true;
        }

        public override void MouseMove(object sender, MouseEventArgs e, int x, int y)
        {
            int diff_x = 0;
            int diff_y = 0;

            if (points.Count > 0)
            {
                prev_line.X2 = x;
                prev_line.Y2 = y;
                diff_x = (int)(x - points[points.Count - 1].X);
                diff_y = (int)(y - points[points.Count - 1].Y);
            }

            Gv.ui_editor_window.status_text.Text = String.Format("x: {0, -6}y = {1, -6}diff_x: {2, -6}diff_y: {3, -6}", x, y, diff_x, diff_y);
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e, int x, int y)
        {
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                try
                {
                    if (points.Count < 2)
                        return;

                    Polygon p = new Polygon();
                    p.Stroke = stroke;
                    p.StrokeThickness = thickness;
                    p.Points = new PointCollection();
                    foreach (var p2 in points)
                        p.Points.Add(p2);

                    PolygonDrawable p3 = new PolygonDrawable(p);
                    Gv.ui_editor_window.AddNewDrawable(p3, "polygon_");
                }
                finally
                {
                    Reset();
                }

            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (first)
                {
                    canvas.Children.Add(prev_line);
                    stroke = System.Windows.Media.Brushes.Black;
                    prev_line.StrokeThickness = thickness;
                    prev_line.Stroke = stroke;

                    first = false;
                    prev_line.X1 = x;
                    prev_line.X2 = x;
                    prev_line.Y1 = y;
                    prev_line.Y2 = y;
                }
                else
                {
                    Line line = new Line();
                    canvas.Children.Add(line);
                    lines.Add(line);
                    line.X1 = prev_point.X;
                    line.Y1 = prev_point.Y;
                    line.X2 = x;
                    line.Y2 = y;
                    line.Stroke = stroke;
                    line.StrokeThickness = thickness;
                }

                prev_point.X = x;
                prev_point.Y = y;
                points.Add(prev_point);

                prev_line.X1 = x;
                prev_line.Y1 = y;
            }
        }

        public override void Reset()
        {
            if (canvas.Children.Contains(prev_line))
                canvas.Children.Remove(prev_line);
            points.Clear();
            foreach (Line l in lines)
                canvas.Children.Remove(l);
            lines.Clear();
            prev_line.X1 = 0;
            prev_line.X2 = 0;
            prev_line.Y1 = 0;
            prev_line.Y2 = 0;
            first = true;
            thickness = 1;
        }
    }

    public class TextStrategy : GameViewStrategy
    {
        bool resizing = false;
        int clickX = 0;
        int clickY = 0;
        Rectangle preview_rect = new Rectangle();

        public TextStrategy(Canvas canvas) : base(canvas)
        {

        }

        public override void MouseUp(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                try
                {
                    if (preview_rect.Width == 0 || preview_rect.Height == 0)
                        return;

                    TextBlock t = new TextBlock();
                    Canvas.SetTop(t, Canvas.GetTop(preview_rect));
                    Canvas.SetLeft(t, Canvas.GetLeft(preview_rect));
                    t.Text = "New Text";
                    t.Width = preview_rect.Width;
                    t.Height = preview_rect.Height;
                    t.Foreground = new SolidColorBrush(Colors.Black);
                    t.FontFamily = new FontFamily("標楷體");
                    t.FontSize = 10;
                    t.TextAlignment = TextAlignment.Center;
                    t.TextWrapping = TextWrapping.Wrap;
                    t.Background = null;

                    var text_drawable = new TextDrawable(t);

                    Gv.ui_editor_window.AddNewDrawable(text_drawable, "text_");
                }
                finally
                {
                    Reset();
                }
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e, int x, int y)
        {
            int diff_x = 0;
            int diff_y = 0;

            if (resizing)
            {
                int minX = (int)e.GetPosition(canvas).X;
                int minY = (int)e.GetPosition(canvas).Y;
                int maxX = clickX;
                int maxY = clickY;

                if (minX > maxX)
                {
                    int temp = minX;
                    minX = maxX;
                    maxX = temp;
                }

                if (minY > maxY)
                {
                    int temp = minY;
                    minY = maxY;
                    maxY = temp;
                }

                preview_rect.StrokeThickness = thickness;
                preview_rect.Width = maxX - minX;
                preview_rect.Height = maxY - minY;
                preview_rect.Stroke = stroke;
                Canvas.SetTop(preview_rect, minY);
                Canvas.SetLeft(preview_rect, minX);

                diff_x = (int)e.GetPosition(canvas).X - clickX;
                diff_y = (int)e.GetPosition(canvas).Y - clickY;
            }

            Gv.ui_editor_window.status_text.Text = String.Format("x: {0, -6}y = {1, -6}diff_x: {2, -6}diff_y: {3, -6}", (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y, diff_x, diff_y);
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                resizing = true;
                clickX = (int)e.GetPosition(canvas).X;
                clickY = (int)e.GetPosition(canvas).Y;

                preview_rect.Fill = Gv.ui_editor_window.fill_color_button.Background;
                canvas.Children.Add(preview_rect);
            }
        }

        public override void Reset()
        {
            if (canvas.Children.Contains(preview_rect))
                canvas.Children.Remove(preview_rect);
            resizing = false;
            preview_rect.Width = 0;
            preview_rect.Height = 0;
        }
    }

    public class RectangleStrategy : GameViewStrategy
    {
        bool resizing = false;
        int clickX = 0;
        int clickY = 0;
        Rectangle preview_rect = new Rectangle();

        public RectangleStrategy(Canvas canvas) : base(canvas)
        {

        }

        public override void MouseUp(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                try
                {
                    if (preview_rect.Width == 0 || preview_rect.Height == 0)
                        return;


                    Rectangle r = new Rectangle();
                    Canvas.SetTop(r, Canvas.GetTop(preview_rect));
                    Canvas.SetLeft(r, Canvas.GetLeft(preview_rect));
                    r.Width = preview_rect.Width;
                    r.Height = preview_rect.Height;
                    r.Fill = Gv.ui_editor_window.fill_color_button.Background;

                    var rect_drawable = new RectangleDrawable(r);

                    Gv.ui_editor_window.AddNewDrawable(rect_drawable, "rect_");
                }
                finally
                {
                    Reset();
                }
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e, int x, int y)
        {
            int diff_x = 0;
            int diff_y = 0;

            if (resizing)
            {
                int minX = (int)e.GetPosition(canvas).X;
                int minY = (int)e.GetPosition(canvas).Y;
                int maxX = clickX;
                int maxY = clickY;

                if (minX > maxX)
                {
                    int temp = minX;
                    minX = maxX;
                    maxX = temp;
                }

                if (minY > maxY)
                {
                    int temp = minY;
                    minY = maxY;
                    maxY = temp;
                }

                preview_rect.StrokeThickness = thickness;
                preview_rect.Width = maxX - minX;
                preview_rect.Height = maxY - minY;
                preview_rect.Stroke = stroke;
                Canvas.SetTop(preview_rect, minY);
                Canvas.SetLeft(preview_rect, minX);

                diff_x = (int)e.GetPosition(canvas).X - clickX;
                diff_y = (int)e.GetPosition(canvas).Y - clickY;
            }

            Gv.ui_editor_window.status_text.Text = String.Format("x: {0, -6}y = {1, -6}diff_x: {2, -6}diff_y: {3, -6}", (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y, diff_x, diff_y);
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                resizing = true;
                clickX = (int)e.GetPosition(canvas).X;
                clickY = (int)e.GetPosition(canvas).Y;

                preview_rect.Fill = Gv.ui_editor_window.fill_color_button.Background;
                canvas.Children.Add(preview_rect);
            }
        }

        public override void Reset()
        {
            if (canvas.Children.Contains(preview_rect))
                canvas.Children.Remove(preview_rect);
            resizing = false;
            preview_rect.Width = 0;
            preview_rect.Height = 0;
        }
    }

    public class SelectStrategy : GameViewStrategy
    {
        Rectangle select_rect = new Rectangle();
        int click_x = 0;
        int click_y = 0;
        bool selecting = false;

        public SelectStrategy(Canvas canvas) : base(canvas)
        {
            select_rect.StrokeThickness = thickness;
            select_rect.Stroke = stroke;
        }

        public override void MouseMove(object sender, MouseEventArgs e, int x, int y)
        {
            base.MouseMove(sender, e, x, y);

            if (!selecting)
                return;

            int min_x = x;
            int min_y = y;
            int max_x = click_x;
            int max_y = click_y;

            if (min_x > max_x)
            {
                int temp = min_x;
                min_x = max_x;
                max_x = temp;
            }

            if (min_y > max_y)
            {
                int temp = min_y;
                min_y = max_y;
                max_y = temp;
            }

            Canvas.SetTop(select_rect, min_y);
            Canvas.SetLeft(select_rect, min_x);
            select_rect.Width = max_x - min_x;
            select_rect.Height = max_y - min_y;

            Gv.ui_editor_window.selections.Clear();

            foreach (var d in Gv.ui_editor_window.drawables)
            {
                var r = d.GetBoundingRect();

                if (r.Left >= min_x && r.Top >= min_y &&
                    r.Left + r.Width <= min_x + select_rect.Width &&
                    r.Top + r.Height <= min_y + select_rect.Height)
                {
                    Gv.ui_editor_window.selections.Add(d);
                }
            }

            Gv.ui_editor_window.update_selections();
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e, int x, int y)
        {
            select_rect.Width = 0;
            select_rect.Height = 0;
            selecting = false;
            if (canvas.Children.Contains(select_rect))
                canvas.Children.Remove(select_rect);
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e, int x, int y)
        {
            if (!canvas.Children.Contains(select_rect))
                canvas.Children.Add(select_rect);
            Gv.ui_editor_window.selections.Clear();
            Gv.ui_editor_window.update_selections();
            selecting = true;
            click_x = x;
            click_y = y;
        }

        public override void Reset()
        {
            select_rect.Width = 0;
            select_rect.Height = 0;
            selecting = false;
            if (canvas.Children.Contains(select_rect))
                canvas.Children.Remove(select_rect);
            Gv.ui_editor_window.selections.Clear();
            Gv.ui_editor_window.update_selections();
        }
    }
}
