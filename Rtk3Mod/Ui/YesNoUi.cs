using System.Windows;
using System.Windows.Controls;

namespace Rtk3Mod
{
    public enum YesNoEnum
    {
        Yes,
        No,
    }

    public class YesNoUi : RectangleBaseUi
    {
        TextBlock yes_text_block = new TextBlock();
        TextBlock no_text_block = new TextBlock();
        public YesNoEnum selection = YesNoEnum.Yes;

        public YesNoUi() : base(76, 56)
        {
            RemoveCorner();

            int padding = 9;
            int rect_width = (76 - padding * 2) / 2;
            int rect_height = (56 - padding * 2);

            yes_text_block.Text = "是";
            yes_text_block.FontFamily = Gv.menu_font;
            yes_text_block.FontSize = Gv.menu_font_size;
            yes_text_block.FontWeight = FontWeights.Bold;
            yes_text_block.TextAlignment = TextAlignment.Center;
            no_text_block.Text = "否";
            no_text_block.FontFamily = Gv.menu_font;
            no_text_block.FontSize = Gv.menu_font_size;
            no_text_block.FontWeight = FontWeights.Bold;
            no_text_block.TextAlignment = TextAlignment.Center;

            Add(yes_text_block, padding, padding, padding + rect_width, padding + rect_height, Gv.z_index_level_6);
            Add(no_text_block, rect_width + padding, padding, padding + rect_width + rect_width, padding + rect_height, Gv.z_index_level_6);

            SetTopLeftPosition(Gv.statusRectangleUi.GetRight() - GetWidth(), Gv.statusRectangleUi.GetTop() - GetHeight());
            SetZIndex(Gv.z_index_level_6);
            Hide();
        }

        public virtual void EnterState()
        {
            Util.Invoke(() =>
            {
                SetSelection(YesNoEnum.Yes);
                Show();
            });
        }

        public override void Update(GameKey key)
        {
            Util.Invoke(() =>
            {
                if (key == GameKey.Left || key == GameKey.Right)
                {
                    if (selection == YesNoEnum.Yes)
                        selection = YesNoEnum.No;
                    else
                        selection = YesNoEnum.Yes;

                    SetSelection(selection);
                }
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                Hide();
            });
        }

        public void SetSelection(YesNoEnum s)
        {
            Util.Invoke(() =>
            {
                if (s == YesNoEnum.Yes)
                {
                    yes_text_block.Background = Gv.selected_background_brush;
                    yes_text_block.Foreground = Gv.selected_foreground_brush;
                    no_text_block.Background = Gv.unselected_background_brush;
                    no_text_block.Foreground = Gv.unselected_foreground_brush;
                }
                else
                {
                    yes_text_block.Background = Gv.unselected_background_brush;
                    yes_text_block.Foreground = Gv.unselected_foreground_brush;
                    no_text_block.Background = Gv.selected_background_brush;
                    no_text_block.Foreground = Gv.selected_foreground_brush;
                }
            });
        }
    }
}
