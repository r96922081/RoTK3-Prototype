using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Rtk3Mod
{
    public class MenuState : CompositeUi, State
    {
        public int index = 0;
        public List<string> menu;
        public City city;

        int width = 64;
        int height = 32;


        public MenuState(List<string> menu)
        {
            this.menu = menu;
            for (int i = 0; i < menu.Count; i++)
            {
                TextBlock t = new TextBlock();
                t.Text = menu[i];
                t.Name = "menu" + i;
                t.TextAlignment = TextAlignment.Center;
                t.FontFamily = Gv.menu_font;
                t.FontSize = 25;
                Add(t, 0, i * height, width, (i + 1) * height, Gv.z_index_level_3);
            }

            SetTopLeftPosition(Gv.screen_left, Gv.status_top - menu.Count * height);
            Hide();
        }

        public void Init(City city)
        {
            this.city = city;

            Util.Invoke(() =>
            {
                SetMenuIndex(0);
            });
        }

        public void Down()
        {
            index++;
            if (index == menu.Count)
            {
                index = 0;
            }

            SetMenuIndex(index);
        }

        public void Up()
        {
            index--;
            if (index == -1)
            {
                index = menu.Count - 1;
            }

            SetMenuIndex(index);
        }

        public virtual void SetMenuIndex(int index)
        {
            this.index = index;
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == index)
                {
                    GetTextBlockElement("menu" + i).Background = Gv.selected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.selected_foreground_brush;
                }
                else
                {
                    GetTextBlockElement("menu" + i).Background = Gv.unselected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.unselected_foreground_brush;
                }
            }
        }

        public override void Update(GameKey key)
        {
            Util.Invoke(new Action(() =>
            {
                if (key == GameKey.Up)
                {
                    Up();
                }
                else if (key == GameKey.Down)
                {
                    Down();
                }
            }));
        }

        public virtual void EnterState()
        {
        }

        public virtual void ExitState()
        {
        }
    }
}
