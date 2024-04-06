using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UiEditor
{
    public enum DrawAction
    {
        Select,
        Text,
        Rectangle,
        Polygon
    }

    public partial class UiEditorWindow : Window
    {
        public GameViewStrategy game_view_strategy;
        public List<Drawable> drawables = new List<Drawable>();
        public List<Drawable> selections = new List<Drawable>();
        public List<Drawable> copys = new List<Drawable>();
        public List<Rectangle> selection_rects = new List<Rectangle>();
        public int index = 1;
        public bool updating_selection_rects;
        public SelectionPropertySyncer syncer = new SelectionPropertySyncer();
        bool setting_canvas_size_from_code_behind = false;
        double canvas_width_at_first = 0;
        double canvas_height_at_first = 0;
        TextStrategy text_strategy;
        RectangleStrategy rectangle_strategy;
        PolygonStrategy Polygon_strategy;
        SelectStrategy select_strategy;

        public UiEditorWindow()
        {
            InitializeComponent();
        }

        public void UpdateSizeProperty()
        {
            if (selections.Count == 0)
            {
                this.top_text_box.Text = "0";
                this.left_text_box.Text = "0";
                this.width_text_box.Text = "0";
                this.height_text_box.Text = "0";
                return;
            }

            int top = int.MinValue;
            int bottom = int.MinValue;
            int left = int.MaxValue;
            int right = int.MinValue;

            foreach (var s in selections)
            {
                var b = s.GetBoundingRect();
                if (b.Top > top)
                    top = (int)b.Top;
                if (b.Left < left)
                    left = (int)b.Left;

                double b_right = b.Left + b.Width;
                double b_bottom = b.Top + b.Height;

                if (b_right > right)
                    right = (int)b_right;
                if (b_bottom > bottom)
                    bottom = (int)b_bottom;
            }

            top_text_box.Text = top.ToString();
            left_text_box.Text = left.ToString();
            height_text_box.Text = (bottom - top).ToString();
            width_text_box.Text = (right - left).ToString();
        }

        private void Reset()
        {
            SetDrawAction(DrawAction.Select);

            List<Drawable> l = new List<Drawable>();

            foreach (var d in drawables)
                l.Add(d);

            foreach (var d in l)
                DeleteDrawable(d);

            selections.Clear();
            update_selections();

            name_combo_box.Items.Clear();
            name_text_box.Text = "";

            canvas.Children.Clear();
        }

        private void Name_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (selections.Count != 1)
                    return;

                string new_name = name_text_box.Text;
                if (new_name == "")
                    return;

                foreach (var p in name_combo_box.Items)
                {
                    if (p.ToString() == new_name)
                    {
                        MessageBox.Show("Duplicated name");
                        return;
                    }
                }

                var selected = selections[0];

                selected.SetName(new_name);

                // update name_combo_box
                List<Drawable> items = new List<Drawable>();
                foreach (var i in name_combo_box.Items)
                    items.Add(i as Drawable);

                name_combo_box.Items.Clear();
                foreach (var i in items)
                {
                    name_combo_box.Items.Add(i);
                    if (i == selected)
                        name_combo_box.SelectedItem = i;
                }
            }
        }

        private void Name_combo_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selections.Clear();

            var selected = name_combo_box.SelectedItem as Drawable;
            if (selected == null)
                return;

            if (name_combo_box.SelectedItem != null)
                this.selections.Add(selected);

            name_text_box.Text = selected.GetName();

            update_selections();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid_splitter.Focusable = false;
            Gv.ui_editor_window = this;

            FixPropertiesTabWidth();
            SetCanvasSize();

            add_event_handler();

            this.Focusable = true;
            name_text_box.Focusable = true;
            canvas.Focusable = true;

            text_strategy = new TextStrategy(canvas);
            rectangle_strategy = new RectangleStrategy(canvas);
            Polygon_strategy = new PolygonStrategy(canvas);
            select_strategy = new SelectStrategy(canvas);
            game_view_strategy = select_strategy;
            SetDrawAction(DrawAction.Select);
        }

        private void SetCanvasSize()
        {
            canvas_width_at_first = canvas.ActualWidth;
            canvas_height_at_first = canvas.ActualHeight;
            setting_canvas_size_from_code_behind = true;
            canvas_height_text_box.Text = canvas_height_at_first.ToString();
            canvas_width_text_box.Text = canvas_width_at_first.ToString();
            setting_canvas_size_from_code_behind = false;
            canvas.HorizontalAlignment = HorizontalAlignment.Left;
            canvas.VerticalAlignment = VerticalAlignment.Top;
            canvas.Height = canvas_height_at_first;
            canvas.Width = canvas_width_at_first;
        }

        private void FixPropertiesTabWidth()
        {
            // Set SizeToContent="WidthAndHeight" first in .xaml
            // after it resized, fixed it here
            this.SizeToContent = SizeToContent.Manual;

            // Set ColumnDefinition Width="auto" first in .xaml
            // after it resized, fixed it here
            right_panel_grid.ColumnDefinitions[2].Width = new GridLength(right_panel_grid.ColumnDefinitions[2].ActualWidth);
        }

        private void Select_toggle_button_Click(object sender, RoutedEventArgs e)
        {
            SetDrawAction(DrawAction.Select);
        }

        public void update_selections()
        {
            RedrawSelectionRect();

            syncer.SetSelectionsToProperties();
        }

        public void RedrawSelectionRect()
        {
            foreach (var rect in selection_rects)
            {
                canvas.Children.Remove(rect);
            }

            selection_rects.Clear();

            foreach (var s in selections)
            {
                UiEditorRect rect = s.GetBoundingRect();

                Rectangle wpf_rect = new Rectangle();
                wpf_rect.Stroke = new SolidColorBrush(Colors.YellowGreen);
                var dash_array = new DoubleCollection();
                dash_array.Add(1);
                dash_array.Add(1);
                wpf_rect.StrokeDashArray = dash_array;
                wpf_rect.StrokeThickness = 3;

                selection_rects.Add(wpf_rect);
                canvas.Children.Add(wpf_rect);

                wpf_rect.Width = rect.Width;
                wpf_rect.Height = rect.Height;
                Canvas.SetTop(wpf_rect, rect.Top);
                Canvas.SetLeft(wpf_rect, rect.Left);
            }
        }

        public void set_game_view_strategy(GameViewStrategy s)
        {
            game_view_strategy = s;
        }

        public void SetDrawAction(DrawAction draw_action)
        {
            game_view_strategy.Reset();
            if (draw_action == DrawAction.Polygon)
            {
                select_toggle_button.IsChecked = false;
                text_toggle_button.IsChecked = false;
                rectangle_toggle_button.IsChecked = false;
                polygon_toggle_button.IsChecked = true;
                game_view_strategy = Polygon_strategy;
            }
            else if (draw_action == DrawAction.Text)
            {
                select_toggle_button.IsChecked = false;
                text_toggle_button.IsChecked = true;
                rectangle_toggle_button.IsChecked = false;
                polygon_toggle_button.IsChecked = false;
                game_view_strategy = text_strategy;
            }
            else if (draw_action == DrawAction.Rectangle)
            {
                select_toggle_button.IsChecked = false;
                text_toggle_button.IsChecked = false;
                rectangle_toggle_button.IsChecked = true;
                polygon_toggle_button.IsChecked = false;
                game_view_strategy = rectangle_strategy;
            }
            else if (draw_action == DrawAction.Select)
            {
                select_toggle_button.IsChecked = true;
                text_toggle_button.IsChecked = false;
                rectangle_toggle_button.IsChecked = false;
                polygon_toggle_button.IsChecked = false;
                game_view_strategy = select_strategy;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Focus();
            game_view_strategy.MouseDown(sender, e, (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            game_view_strategy.MouseMove(sender, e, (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            game_view_strategy.MouseUp(sender, e, (int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y);
        }

        public void AddDrawableToNameComboBox(Drawable d)
        {
            name_combo_box.Items.Add(d);
            name_combo_box.SelectedIndex = name_combo_box.Items.Count - 1;
        }

        public void RemoveDrawableFromNameComboBox(Drawable d)
        {
            name_combo_box.Items.Remove(d);
        }

        public void AddNewDrawable(Drawable d, string prefix)
        {
            d.SetName(prefix + index.ToString());
            index++;
            AddDrawableToNameComboBox(d);

            d.AddToCanvas(canvas);
            drawables.Add(d);
            selections.Clear();
            selections.Add(d);
            update_selections();
        }

        public void AddNewDrawable(Drawable d)
        {
            index++;
            AddDrawableToNameComboBox(d);

            d.AddToCanvas(canvas);
            drawables.Add(d);
            selections.Clear();
            selections.Add(d);
            update_selections();
        }

        public void DeleteDrawable(Drawable d)
        {
            d.RemoveFromCanvas(canvas);
            Gv.ui_editor_window.drawables.Remove(d);
            Gv.ui_editor_window.RemoveDrawableFromNameComboBox(d);
        }
    }
}
