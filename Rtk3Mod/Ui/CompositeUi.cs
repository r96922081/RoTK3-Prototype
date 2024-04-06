using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Rtk3Mod
{
    public class CompositeUi
    {
        public List<FrameworkElement> elements = new List<FrameworkElement>();

        public void Add(FrameworkElement e, int left, int top, int right, int bottom, int zIndex)
        {
            Util.AddToCanvas(e);
            elements.Add(e);
            Canvas.SetLeft(e, left);
            Canvas.SetTop(e, top);
            Canvas.SetRight(e, right);
            Canvas.SetBottom(e, bottom);
            e.Height = bottom - top;
            e.Width = right - left;
            Canvas.SetZIndex(e, zIndex);
        }
        public void Add(FrameworkElement e, int zIndex)
        {
            Util.AddToCanvas(e);
            elements.Add(e);
            Canvas.SetZIndex(e, zIndex);
        }

        public void Shift(int offsetX, int offsetY)
        {
            foreach (var e in elements)
            {
                Util.Adjust(e, offsetX, offsetY);
            }
        }

        public void SetZIndex(int zIndex)
        {
            foreach (var e in elements)
            {
                Canvas.SetZIndex(e, zIndex);
            }
        }

        public virtual void Show()
        {
            foreach (var e in elements)
            {
                e.Visibility = Visibility.Visible;
            }
        }

        public void Hide()
        {
            foreach (var e in elements)
            {
                e.Visibility = Visibility.Hidden;
            }
        }

        public void Remove(FrameworkElement e2)
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                var e = elements[i];
                if (e == e2)
                {
                    Util.RemoveFromCanvas(e);
                    elements.RemoveAt(i);
                }
            }
        }

        public void Remove(string name)
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                var e = elements[i];
                if (e.Name == name)
                {
                    Util.RemoveFromCanvas(e);
                    elements.RemoveAt(i);
                }
            }
        }

        public void RemoveAll()
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                var e = elements[i];
                Util.RemoveFromCanvas(e);
                elements.RemoveAt(i);
            }
        }

        public FrameworkElement GetElement(string name)
        {
            foreach (var e in elements)
            {
                if (e.Name == name)
                    return e;
            }

            return null;
        }

        public TextBlock GetTextBlockElement(string name)
        {
            foreach (var e in elements)
            {
                if (e.Name == name)
                    return (TextBlock)e;
            }

            return null;
        }

        public Rectangle GetRectangleElement(string name)
        {
            foreach (var e in elements)
            {
                if (e.Name == name)
                    return (Rectangle)e;
            }

            return null;
        }


        public int GetRight()
        {
            int value = int.MinValue;
            foreach (var e in elements)
            {
                if (Canvas.GetRight(e) > value)
                    value = (int)Canvas.GetRight(e);
            }

            return value;
        }

        public int GetLeft()
        {
            int value = int.MaxValue;
            foreach (var e in elements)
            {
                if (Canvas.GetLeft(e) < value)
                    value = (int)Canvas.GetLeft(e);
            }

            return value;
        }

        public int GetTop()
        {
            int value = int.MaxValue;
            foreach (var e in elements)
            {
                if (Canvas.GetTop(e) < value)
                    value = (int)Canvas.GetTop(e);
            }

            return value;
        }

        public int GetBottom()
        {
            int value = int.MinValue;
            foreach (var e in elements)
            {
                if (Canvas.GetBottom(e) > value)
                    value = (int)Canvas.GetBottom(e);
            }

            return value;
        }

        public int GetHeight()
        {
            return GetBottom() - GetTop();
        }

        public int GetWidth()
        {
            return GetRight() - GetLeft();
        }

        public int GetZIndex()
        {
            int z = -1;

            foreach (var e in elements)
            {
                if (Canvas.GetZIndex(e) > z)
                    z = Canvas.GetZIndex(e);
            }

            return z;
        }

        public void SetTopLeftPosition(int x, int y)
        {
            int offset_x = x - GetLeft();
            int offset_y = y - GetTop();
            Shift(offset_x, offset_y);
        }

        public virtual void Update(GameKey key)
        {

        }
    }
}
