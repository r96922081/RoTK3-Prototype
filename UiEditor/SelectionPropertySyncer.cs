using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UiEditor
{
    public class SelectionPropertySyncer
    {
        private bool updating_selection_rects = false;

        private void ResetProperties()
        {
            Gv.ui_editor_window.name_text_box.Text = "";
            Gv.ui_editor_window.text_text_box.Text = "";

            Gv.ui_editor_window.text_color_button.Background = new SolidColorBrush(Colors.Black);
            Gv.ui_editor_window.text_color_a_text_box.Text = "";
            Gv.ui_editor_window.text_color_r_text_box.Text = "";
            Gv.ui_editor_window.text_color_g_text_box.Text = "";
            Gv.ui_editor_window.text_color_b_text_box.Text = "";

            Gv.ui_editor_window.font_combo_box.SelectedIndex = Gv.ui_editor_window.font_combo_box.Items.Count - 1;
            Gv.ui_editor_window.font_size_text_box.Text = "";
            Gv.ui_editor_window.bold_toggle_button.IsChecked = null;
            Gv.ui_editor_window.alignment_combo_box.SelectedIndex = Gv.ui_editor_window.alignment_combo_box.Items.Count - 1;
            Gv.ui_editor_window.fill_style_combo_box.SelectedIndex = Gv.ui_editor_window.fill_style_combo_box.Items.Count - 1;

            Gv.ui_editor_window.fill_color_button.Background = new SolidColorBrush(Colors.White);
            Gv.ui_editor_window.fill_color_a_text_box.Text = "";
            Gv.ui_editor_window.fill_color_r_text_box.Text = "";
            Gv.ui_editor_window.fill_color_g_text_box.Text = "";
            Gv.ui_editor_window.fill_color_b_text_box.Text = "";

            Gv.ui_editor_window.left_text_box.Text = "";
            Gv.ui_editor_window.top_text_box.Text = "";
            Gv.ui_editor_window.width_text_box.Text = "";
            Gv.ui_editor_window.height_text_box.Text = "";
        }

        public void SetSelectionsToProperties()
        {
            updating_selection_rects = true;

            ResetProperties();

            if (Gv.ui_editor_window.selections.Count == 0)
            {
                updating_selection_rects = false;
                return;
            }

            SetNameToProperty();
            SetTextToProperty();
            SetTextColorToProperty();
            SetFontFamilyToProperty();
            SetFontSizeToProperty();
            SetBoldToProperty();
            SetAlignToProperty();
            SetFillStyleToProperty();
            SetFillColorToProperty();
            SetPositionToProperty();
            SetBorderSizeToProperty();
            SetBorderColorToProperty();

            updating_selection_rects = false;
        }

        private void SetPositionToProperty()
        {
            int top = -1;
            int left = -1;
            int height = -1;
            int width = -1;
            bool first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                var r = s2.GetBoundingRect();

                if (first)
                {
                    first = false;
                    top = (int)r.Top;
                    left = (int)r.Left;
                    width = (int)r.Width;
                    height = (int)r.Height;
                }
                else
                {
                    int top2 = (int)r.Top;
                    int left2 = (int)r.Left;
                    int width2 = (int)r.Width;
                    int height2 = (int)r.Height;

                    if (top2 != top)
                        top = -2;

                    if (left2 != left)
                        left = -2;

                    if (width2 != width)
                        width = -2;

                    if (height2 != height)
                        height = -2;
                }
            }

            if (top >= 0)
                Gv.ui_editor_window.top_text_box.Text = top.ToString();

            if (left >= 0)
                Gv.ui_editor_window.left_text_box.Text = left.ToString();

            if (width >= 0)
                Gv.ui_editor_window.width_text_box.Text = width.ToString();

            if (height >= 0)
                Gv.ui_editor_window.height_text_box.Text = height.ToString();
        }

        private void SetFillColorToProperty()
        {
            bool first;
            Brush c = null;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                Brush c2 = null;
                if (s2.GetFillColor(ref c2))
                {
                    if (first)
                    {
                        first = false;
                        c = c2;
                    }
                    else if (c2 == null && c == null)
                    {

                    }
                    else if (c2 == null && c != null)
                    {
                        c = null;
                        break;
                    }
                    else if (!c2.Equals(c))
                    {
                        c = null;
                        break;
                    }
                }
            }

            if (c != null)
            {
                Gv.ui_editor_window.fill_color_button.Background = c;

                Utility.SetColorToTextBox(((SolidColorBrush)Gv.ui_editor_window.fill_color_button.Background).Color, Gv.ui_editor_window.fill_color_a_text_box,
                    Gv.ui_editor_window.fill_color_r_text_box, Gv.ui_editor_window.fill_color_g_text_box,
                    Gv.ui_editor_window.fill_color_b_text_box);
            }
        }

        private void SetBorderColorToProperty()
        {
            bool first;
            Brush c = null;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                Brush c2 = null;
                if (s2.GetBorderColor(ref c2))
                {
                    if (first)
                    {
                        first = false;
                        c = c2;
                    }
                    else if (c2 == null && c == null)
                    {

                    }
                    else if (c2 == null && c != null)
                    {
                        c = null;
                        break;
                    }
                    else if (!c2.Equals(c))
                    {
                        c = null;
                        break;
                    }
                }
            }

            if (c != null)
            {
                Gv.ui_editor_window.border_color_button.Background = c;

                Utility.SetColorToTextBox(((SolidColorBrush)Gv.ui_editor_window.border_color_button.Background).Color, Gv.ui_editor_window.border_color_a_text_box,
                    Gv.ui_editor_window.border_color_r_text_box, Gv.ui_editor_window.border_color_g_text_box,
                    Gv.ui_editor_window.border_color_b_text_box);
            }
        }

        private void SetFillStyleToProperty()
        {
            bool first = true;
            bool update_it = true;
            bool filled = false;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                bool filled2 = false;
                if (s2.GetFilled(ref filled2))
                {
                    if (first)
                    {
                        first = false;
                        filled = filled2;
                    }
                    else
                    {
                        if (filled2 != false)
                        {
                            if (filled == false)
                            {
                                update_it = false;
                                break;
                            }
                        }
                        else
                        {
                            if (filled == true)
                            {
                                update_it = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (update_it)
            {
                if (filled)
                    Gv.ui_editor_window.fill_style_combo_box.SelectedIndex = 0;
                else
                    Gv.ui_editor_window.fill_style_combo_box.SelectedIndex = 1;
            }
        }

        private void SetNameToProperty()
        {
            string name = Gv.ui_editor_window.selections[0].GetName();

            if (Gv.ui_editor_window.selections.Count == 1)
            {
                Gv.ui_editor_window.name_text_box.Text = name;
            }

            for (int i = 0; i < Gv.ui_editor_window.name_combo_box.Items.Count; i++)
            {
                if (Gv.ui_editor_window.name_combo_box.Items[i] is Drawable)
                {
                    Drawable d = (Drawable)Gv.ui_editor_window.name_combo_box.Items[i];
                    if (d.GetName() == name)
                    {
                        Gv.ui_editor_window.name_combo_box.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void SetTextToProperty()
        {
            string text = "";
            bool first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                string text2 = "";
                if (s2.GetText(ref text2))
                {
                    if (first)
                    {
                        first = false;
                        text = text2;
                    }
                    else
                    {
                        if (text2 != text)
                        {
                            text = "";
                            break;
                        }
                    }
                }
            }
            Gv.ui_editor_window.text_text_box.Text = text;
        }

        private void SetTextColorToProperty()
        {
            bool first;
            Brush c = null;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                Brush c2 = null;
                if (s2.GetTextColor(ref c2))
                {
                    if (first)
                    {
                        first = false;
                        c = c2;
                    }
                    else
                    {
                        if (!c2.Equals(c))
                        {
                            c = null;
                            break;
                        }
                    }
                }
            }

            if (c != null)
                Gv.ui_editor_window.text_color_button.Background = c;

            Utility.SetColorToTextBox(((SolidColorBrush)Gv.ui_editor_window.text_color_button.Background).Color, Gv.ui_editor_window.text_color_a_text_box,
                Gv.ui_editor_window.text_color_r_text_box, Gv.ui_editor_window.text_color_g_text_box,
                Gv.ui_editor_window.text_color_b_text_box);
        }

        private void SetFontFamilyToProperty()
        {
            bool first;
            FontFamily f = new FontFamily();
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                FontFamily f2 = new FontFamily();

                if (s2.GetFontFamily(ref f2))
                {
                    if (first)
                    {
                        first = false;
                        f = f2;
                    }
                    else
                    {
                        if (!f2.Equals(f))
                        {
                            f = null;
                            break;
                        }
                    }
                }
            }

            if (f != null)
            {
                string name = f.ToString();

                for (int i = 0; i < Gv.ui_editor_window.font_combo_box.Items.Count; i++)
                {
                    object content = ((ComboBoxItem)Gv.ui_editor_window.font_combo_box.Items[i]).Content;
                    if (content == null)
                        continue;

                    if (content.ToString() == name)
                    {
                        Gv.ui_editor_window.font_combo_box.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void SetBoldToProperty()
        {
            bool bold = false;
            bool first = true;
            bool need_to_update = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                bool bold2 = false;
                if (s2.GetBold(ref bold2))
                {
                    if (first)
                    {
                        bold = bold2;
                    }
                    else
                    {
                        if (bold2 != bold)
                        {
                            need_to_update = false;
                            break;
                        }
                    }
                }
            }
            if (need_to_update)
                Gv.ui_editor_window.bold_toggle_button.IsChecked = bold;
        }

        private void SetBorderSizeToProperty()
        {
            bool first;
            int size = -1;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                double size2 = 0;
                if (s2.GetBorderSize(ref size2))
                {
                    if (first)
                    {
                        first = false;
                        size = (int)size2;
                    }
                    else
                    {
                        if ((int)size2 != size)
                        {
                            size = -1;
                            break;
                        }
                    }
                }
            }
            if (size != -1)
                Gv.ui_editor_window.border_size_text_box.Text = size.ToString();
            else
                Gv.ui_editor_window.border_size_text_box.Text = "";
        }

        private void SetFontSizeToProperty()
        {
            bool first;
            int size = -1;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                double size2 = 0;
                if (s2.GetFontSize(ref size2))
                {
                    if (first)
                    {
                        first = false;
                        size = (int)size2;
                    }
                    else
                    {
                        if ((int)size2 != size)
                        {
                            size = -1;
                            break;
                        }
                    }
                }
            }
            if (size != -1)
                Gv.ui_editor_window.font_size_text_box.Text = size.ToString();
        }

        private void SetAlignToProperty()
        {
            bool first;
            TextAlignment align = TextAlignment.Center;
            bool update_it = true;
            first = true;
            foreach (var s2 in Gv.ui_editor_window.selections)
            {
                TextAlignment align2 = TextAlignment.Center;
                if (s2.GetTextAlignment(ref align2))
                {
                    if (first)
                    {
                        first = false;
                        align = align2;
                    }
                    else
                    {
                        if (align2 != align)
                        {
                            update_it = false;
                            break;
                        }
                    }
                }
            }

            if (update_it)
            {
                for (int i = 0; i < Gv.ui_editor_window.alignment_combo_box.Items.Count; i++)
                {
                    object content = ((ComboBoxItem)Gv.ui_editor_window.alignment_combo_box.Items[i]).Content;
                    if (content == null)
                        continue;

                    if (content.ToString() == align.ToString())
                    {
                        Gv.ui_editor_window.alignment_combo_box.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public void SetHeightPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int height = 0;
                if (Int32.TryParse(Gv.ui_editor_window.height_text_box.Text, out height) == false)
                    break;

                s.SetHeight(height);
            }

            Gv.ui_editor_window.RedrawSelectionRect();
        }

        public void SetWidthPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int width = 0;
                if (Int32.TryParse(Gv.ui_editor_window.width_text_box.Text, out width) == false)
                    break;

                s.SetWidth(width);
            }

            Gv.ui_editor_window.RedrawSelectionRect();
        }

        public void SetLeftPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int left = 0;
                if (Int32.TryParse(Gv.ui_editor_window.left_text_box.Text, out left) == false)
                    break;

                s.SetLeft(left);
            }

            Gv.ui_editor_window.RedrawSelectionRect();
        }

        public void SetTopPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int top = 0;
                if (Int32.TryParse(Gv.ui_editor_window.top_text_box.Text, out top) == false)
                    break;

                s.SetTop(top);
            }

            Gv.ui_editor_window.RedrawSelectionRect();
        }

        public void SetFillPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                if (Gv.ui_editor_window.fill_style_combo_box.SelectedIndex == 0)
                {
                    s.SetFill(Gv.ui_editor_window.fill_color_button.Background);
                }
                else
                {
                    s.SetFill(null);
                }
            }
        }

        public void SetAlignPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                if (Gv.ui_editor_window.alignment_combo_box.SelectedIndex == 0)
                    s.SetTextAlignment(TextAlignment.Center);
                else if (Gv.ui_editor_window.alignment_combo_box.SelectedIndex == 1)
                    s.SetTextAlignment(TextAlignment.Left);
                else if (Gv.ui_editor_window.alignment_combo_box.SelectedIndex == 2)
                    s.SetTextAlignment(TextAlignment.Right);
            }
        }

        public void SetFontSizePropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int size = 0;
                if (!Int32.TryParse(Gv.ui_editor_window.font_size_text_box.Text, out size))
                    return;

                s.SetFontSize(size);
            }
        }

        public void SetBorderSizePropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                int size = 0;
                if (!Int32.TryParse(Gv.ui_editor_window.border_size_text_box.Text, out size))
                    return;

                s.SetBorderSize(size);
            }
        }

        public void SetBorderColorPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                s.SetBorderColor(Gv.ui_editor_window.border_color_button.Background);
            }
        }

        public void SetBoldPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            bool? b = Gv.ui_editor_window.bold_toggle_button.IsChecked;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                if (b.HasValue && b.Value)
                {
                    s.SetBold(true);
                }
                else
                {
                    s.SetBold(false);
                }
            }
        }

        public void SetFontPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                s.SetFontFamily(new FontFamily(((ComboBoxItem)Gv.ui_editor_window.font_combo_box.SelectedItem).Content.ToString()));
            }
        }

        public void SetTextColorPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                s.SetTextColor(Gv.ui_editor_window.text_color_button.Background);
            }
        }

        public void SetTextPropertyToSelections()
        {
            if (updating_selection_rects)
                return;

            foreach (var s in Gv.ui_editor_window.selections)
            {
                s.SetText(Gv.ui_editor_window.text_text_box.Text);
            }
        }
    }
}
