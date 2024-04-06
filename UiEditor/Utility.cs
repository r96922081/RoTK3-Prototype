using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UiEditor
{
    public class Utility
    {
        public static void SetColorToTextBox(Color c, TextBox text_color_a_text_box
            , TextBox text_color_r_text_box, TextBox text_color_g_text_box, TextBox text_color_b_text_box)
        {
            text_color_a_text_box.Text = c.A.ToString("X2");
            text_color_r_text_box.Text = c.R.ToString("X2");
            text_color_g_text_box.Text = c.G.ToString("X2");
            text_color_b_text_box.Text = c.B.ToString("X2");
        }

        public static Polygon Clone(Polygon p)
        {
            return new UiEditorPolygon(p).GetWpfUiElement();
        }
    }

    public class Gv
    {
        public static UiEditorWindow ui_editor_window;

        public static string confirm_advice_ui_design_path
        {
            get
            {
                return System.IO.Path.Combine(@"..\..\UiDesign", "confirm_advice_ui_design.json");
            }
        }
    }
}
