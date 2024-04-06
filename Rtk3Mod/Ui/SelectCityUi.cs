using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rtk3Mod
{
    public class SelectCityUi : CompositeUi
    {
        int updateCount = 0;
        bool show = true;
        bool animation = false;
        City city;
        public City targetCity;
        List<City> availableCities;

        static int citySize = CityMapUi.city_size;
        List<FrameworkElement> triangles = new List<FrameworkElement>();

        public SelectCityUi()
        {
            Util.Invoke(() =>
            {
                // pointer
                Ellipse pointer = new Ellipse();
                pointer.Name = "pointer";
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.DarkGoldenrod;
                pointer.Fill = brush;
                Add(pointer, 0, 0, citySize, citySize, Gv.z_index_level_7);

                // border
                Rectangle border = new Rectangle();
                border.Name = "border";
                border.Stroke = new SolidColorBrush(Colors.YellowGreen);
                border.StrokeThickness = 5;
                Add(border, 0, 0, citySize, citySize, Gv.z_index_level_6 + 1);

                Hide();
            });
        }

        public void Init(City city, List<City> availableCities)
        {
            Util.Invoke(() =>
            {
                this.city = city;
                this.availableCities = availableCities;
                ResetTriangle(availableCities);
                Canvas.SetLeft(GetElement("pointer"), city.pos_x);
                Canvas.SetTop(GetElement("pointer"), city.pos_y);

                animation = true;
            });
        }

        private void ResetTriangle(List<City> availableCities)
        {
            foreach (FrameworkElement t in triangles)
            {
                Remove(t);
            }
            triangles.Clear();

            // triangle
            int index = 0;
            foreach (City c in availableCities)
            {
                Polygon p = new Polygon();
                p.Name = "polygon" + index;
                index++;

                SolidColorBrush brush2 = new SolidColorBrush();
                brush2.Color = Colors.DarkBlue;
                p.Fill = brush2;

                p.Points = new PointCollection();
                p.Points.Add(new System.Windows.Point(c.pos_x - citySize * 0.8, c.pos_y));
                p.Points.Add(new System.Windows.Point(c.pos_x - citySize * 0.8, c.pos_y + citySize));
                p.Points.Add(new System.Windows.Point(c.pos_x - citySize * 0.1, c.pos_y + citySize / 2));
                p.Visibility = Visibility.Hidden;
                Add(p, Gv.z_index_level_6);
                triangles.Add(p);
            }
        }

        private void ShowTriangleAnimation()
        {
            Util.Invoke(() =>
            {
                if (show)
                {
                    foreach (FrameworkElement f in triangles)
                    {
                        f.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    foreach (FrameworkElement f in triangles)
                    {
                        f.Visibility = Visibility.Hidden;
                    }
                }
            });

            show = !show;
        }

        private void CheckOverlap()
        {
            FrameworkElement pointer = GetElement("pointer");

            double pX = Canvas.GetLeft(pointer) + citySize / 2;
            double pY = Canvas.GetTop(pointer) + citySize / 2;

            targetCity = null;

            foreach (City c in availableCities)
            {
                if (c.pos_x - citySize / 2 <= pX && pX <= c.pos_x + citySize + citySize / 2 &&
                    c.pos_y - citySize / 2 <= pY && pY <= c.pos_y + citySize + citySize / 2)
                {
                    targetCity = c;
                    break;
                }
            }


            Util.Invoke(() =>
            {
                if (targetCity != null)
                {
                    GetElement("border").Visibility = Visibility.Visible;
                    Canvas.SetTop(GetElement("border"), targetCity.pos_y);
                    Canvas.SetLeft(GetElement("border"), targetCity.pos_x);
                }
                else
                {
                    GetElement("border").Visibility = Visibility.Hidden;
                }
            });
        }

        public override void Update(GameKey key)
        {
            updateCount++;
            if (updateCount % 12 == 0 && animation)
            {
                ShowTriangleAnimation();
            }

            if (key == GameKey.None)
                return;

            int diff = 3;
            Util.Invoke(() =>
            {
                FrameworkElement pointer = GetElement("pointer");

                if (key == GameKey.Backward)
                {
                    show = false;
                    ShowTriangleAnimation();
                    animation = false;
                }
                else if (key == GameKey.Up)
                {
                    Canvas.SetTop(pointer, Canvas.GetTop(pointer) - diff);
                    CheckOverlap();
                }
                else if (key == GameKey.Down)
                {
                    Canvas.SetTop(pointer, Canvas.GetTop(pointer) + diff);
                    CheckOverlap();
                }
                else if (key == GameKey.Right)
                {
                    Canvas.SetLeft(pointer, Canvas.GetLeft(pointer) + diff);
                    CheckOverlap();
                }
                else if (key == GameKey.Left)
                {
                    Canvas.SetLeft(pointer, Canvas.GetLeft(pointer) - diff);
                    CheckOverlap();
                }
                else if (key == GameKey.Forward)
                {
                    if (targetCity != null)
                    {
                        show = false;
                        ShowTriangleAnimation();
                        animation = false;
                    }
                }
            });
        }
    }
}
