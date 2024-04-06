using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Rtk3Mod
{
    public class Util
    {
        public static void Show(List<UIElement> ui_elements)
        {
            Invoke(() =>
            {
                foreach (var u in ui_elements)
                    u.Visibility = Visibility.Visible;
            });
        }

        public static void Hide(List<UIElement> ui_elements)
        {
            Invoke(() =>
            {
                foreach (var u in ui_elements)
                    u.Visibility = Visibility.Hidden;
            });
        }

        public static void Adjust(List<UIElement> ui_elements, double offset_x, double offset_y)
        {
            Invoke(() =>
            {
                foreach (var u in ui_elements)
                {
                    Canvas.SetTop(u, Canvas.GetTop(u) + offset_y);
                    Canvas.SetLeft(u, Canvas.GetLeft(u) + offset_x);
                }
            });
        }

        public static void Adjust(FrameworkElement frameworkElement, double offset_x, double offset_y)
        {
            Invoke(() =>
            {
                Canvas.SetTop(frameworkElement, Canvas.GetTop(frameworkElement) + offset_y);
                Canvas.SetLeft(frameworkElement, Canvas.GetLeft(frameworkElement) + offset_x);
                Canvas.SetBottom(frameworkElement, Canvas.GetBottom(frameworkElement) + offset_y);
                Canvas.SetRight(frameworkElement, Canvas.GetRight(frameworkElement) + offset_x);
            });
        }

        public static void AddToCanvas(UIElement u)
        {
            Invoke(() =>
            {
                GameWindow.instance.canvas.Children.Add(u);
            });
        }

        public static void AddToCanvas(UIElement u, double top, double left, int z_index)
        {
            AddToCanvas(u, (int)top, (int)left, z_index);
        }

        public static void AddToCanvas(UIElement u, int top, int left, int z_index)
        {
            Canvas.SetTop(u, top);
            Canvas.SetLeft(u, left);
            Canvas.SetZIndex(u, z_index);
            AddToCanvas(u);
        }

        public static void RemoveFromCanvas(List<UIElement> u)
        {
            Invoke(() =>
            {
                foreach (var u2 in u)
                    GameWindow.instance.canvas.Children.Remove(u2);
            });
        }

        public static void RemoveFromCanvas(UIElement u)
        {
            Invoke(() =>
            {
                GameWindow.instance.canvas.Children.Remove(u);
            });
        }

        public static void BeginInvoke2(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static void LoadUiDesign(string path, CompositeUi gameUiElements)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string file_content = reader.ReadToEnd();

                UiEditor.SaveData save_data = JsonConvert.DeserializeObject<UiEditor.SaveData>(file_content);

                foreach (var d in save_data.UiEditorRects)
                {
                    Rectangle r = d.GetWpfUiElement();
                    gameUiElements.Add(r, Gv.z_index_level_5);

                }

                foreach (var d in save_data.UiEditorPolygons)
                {
                    Polygon p = d.GetWpfUiElement();
                    gameUiElements.Add(p, Gv.z_index_level_5);
                }

                foreach (var d in save_data.UiEditorTexts)
                {
                    TextBlock t = d.GetWpfUiElement();
                    gameUiElements.Add(t, Gv.z_index_level_5);
                }
            }
        }

        public static string GetUiJson(string json)
        {
            return System.IO.Path.Combine(@"..\..\UiDesign", json);
        }
    }
}
