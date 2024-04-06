using System.Windows.Controls;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class RectangleBaseUi : CompositeUi
    {
        int cornerSize = 10;

        public RectangleBaseUi(int width, int height)
        {
            Util.LoadUiDesign(Util.GetUiJson("rectangle_base_ui_design.json"), this);
            GetRectangleElement("base_rect_back").Width = width;
            Canvas.SetRight(GetRectangleElement("base_rect_back"), width);
            GetRectangleElement("base_rect_back").Height = height;
            Canvas.SetBottom(GetRectangleElement("base_rect_back"), height);

            Canvas.SetLeft(GetRectangleElement("top_right_corner"), width - cornerSize);
            Canvas.SetRight(GetRectangleElement("top_right_corner"), width);

            Canvas.SetTop(GetRectangleElement("bottom_left_corner"), height - cornerSize);
            Canvas.SetBottom(GetRectangleElement("bottom_left_corner"), height);

            Canvas.SetLeft(GetRectangleElement("bottom_right_corner"), width - cornerSize);
            Canvas.SetRight(GetRectangleElement("bottom_right_corner"), width);
            Canvas.SetTop(GetRectangleElement("bottom_right_corner"), height - cornerSize);
            Canvas.SetBottom(GetRectangleElement("bottom_right_corner"), height);

            SetZIndex(Gv.z_index_level_5);
            Hide();
        }

        public void RemoveCorner()
        {
            Remove("top_left_corner");
            Remove("top_right_corner");
            Remove("bottom_left_corner");
            Remove("bottom_right_corner");
        }

        public void SetBackColor(byte r, byte g, byte b)
        {
            GetRectangleElement("base_rect_back").Fill = new SolidColorBrush(Color.FromArgb(0xff, r, g, b));
        }

        public void SetBackAlpha(byte alpha)
        {
            GetRectangleElement("base_rect_back").Fill = new SolidColorBrush(Color.FromArgb(alpha, 0, 16, 33));
        }
    }
}
