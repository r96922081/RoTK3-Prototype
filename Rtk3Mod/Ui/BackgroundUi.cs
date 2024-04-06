using System.Windows.Media;
using System.Windows.Shapes;

namespace Rtk3Mod
{
    public class BackgroundUi : CompositeUi
    {
        public BackgroundUi()
        {
            Rectangle r = new Rectangle();
            r.Name = "back";
            r.Fill = Gv.winter;
            Add(r, 0, 0, 1000, 1000, Gv.z_index_level_0);
        }

        public void SetColor(SolidColorBrush color)
        {
            GetRectangleElement("back").Fill = color;
        }
    }
}
