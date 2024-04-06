using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rtk3Mod
{
    public class CityMapUi : CompositeUi
    {
        public static int city_size = 18;

        public CityMapUi()
        {
            for (int i = 0; i < Gv.g.cities.Count(); i++)
            {
                // index is 1-based
                City city = Gv.g.cities[i + 1];

                foreach (var adj in city.adjacency_cities)
                {
                    Line l = new Line();
                    City c2 = Gv.g.cities[adj];

                    l.X1 = city.pos_x + city_size / 2;
                    l.Y1 = city.pos_y + city_size / 2;
                    l.X2 = c2.pos_x + city_size / 2;
                    l.Y2 = c2.pos_y + city_size / 2;
                    l.Stroke = Gv.white;
                    l.StrokeThickness = 2;
                    Add(l, Gv.z_index_level_1);
                }

                Rectangle r = new Rectangle();

                if (city.ruler == "")
                {
                    r.Fill = new SolidColorBrush(Colors.White);
                }
                else
                {
                    r.Fill = new SolidColorBrush(Gv.c.GetForce(city.ruler).color);
                }
                r.Stroke = new SolidColorBrush(Colors.Black);
                r.StrokeThickness = 2;
                r.Width = city_size;
                r.Height = city_size;
                Canvas.SetLeft(r, city.pos_x);
                Canvas.SetTop(r, city.pos_y);
                Add(r, Gv.z_index_level_2);
            }
        }
    }
}
