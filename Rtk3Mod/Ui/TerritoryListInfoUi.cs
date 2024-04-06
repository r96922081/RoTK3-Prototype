using System.Collections.Generic;

namespace Rtk3Mod
{
    public class TerritoryListInfoUi : RectangleBaseUi
    {
        List<City> cities;

        int type = 0;

        int maxRowCount = 10;
        int cityCount = -1;
        int pageIndex = 0;
        int maxPageCount = -1;

        public TerritoryListInfoUi() : base(Gv.screen_right - Gv.screen_left, Gv.status_top - Gv.screen_top)
        {
            Util.LoadUiDesign(Util.GetUiJson("territory_list_info_rectangle_dialog_ui_design.json"), this);
            SetZIndex(Gv.z_index_level_6);
            SetTopLeftPosition(Gv.screen_left, 0);
            Remove("back_rect");
            Hide();
        }

        public void Init(City city)
        {
            Force f = Gv.c.GetForce(city.ruler);
            cities = f.GetCities();

            cityCount = cities.Count;
            maxPageCount = cityCount / maxRowCount + 1;
        }

        public void UpdateView()
        {
            if (type == 0)
            {
                GetTextBlockElement("r0c0").Text = "城市";
                GetTextBlockElement("r0c1").Text = "太守";
                GetTextBlockElement("r0c2").Text = "武將";
                GetTextBlockElement("r0c3").Text = "人民";
                GetTextBlockElement("r0c4").Text = "黃金";
            }
            else if (type == 1)
            {
                GetTextBlockElement("r0c0").Text = "兵力";
                GetTextBlockElement("r0c1").Text = "米糧";
                GetTextBlockElement("r0c2").Text = "經濟";
                GetTextBlockElement("r0c3").Text = "土地";
                GetTextBlockElement("r0c4").Text = "農耕";
            }
            else
            {
                GetTextBlockElement("r0c0").Text = "防災";
                GetTextBlockElement("r0c1").Text = "稅率";
                GetTextBlockElement("r0c2").Text = "治安";
                GetTextBlockElement("r0c3").Text = "";
                GetTextBlockElement("r0c4").Text = "";
            }


            int row = 1;
            for (int i = pageIndex * 10; i < pageIndex * 10 + 10; i++)
            {
                if (i >= cityCount)
                {
                    GetTextBlockElement("r" + row + "c0").Text = "";
                    GetTextBlockElement("r" + row + "c1").Text = "";
                    GetTextBlockElement("r" + row + "c2").Text = "";
                    GetTextBlockElement("r" + row + "c3").Text = "";
                    GetTextBlockElement("r" + row + "c4").Text = "";
                }
                else
                {
                    City c = cities[i];

                    if (type == 0)
                    {
                        GetTextBlockElement("r" + row + "c0").Text = c.name.ToString();
                        GetTextBlockElement("r" + row + "c1").Text = c.governer.ToString();
                        GetTextBlockElement("r" + row + "c2").Text = c.regular_officers.Count.ToString();
                        GetTextBlockElement("r" + row + "c3").Text = c.population.ToString();
                        GetTextBlockElement("r" + row + "c4").Text = c.gold.ToString();
                    }
                    else if (type == 1)
                    {
                        GetTextBlockElement("r" + row + "c0").Text = c.troops.ToString();
                        GetTextBlockElement("r" + row + "c1").Text = c.food.ToString();
                        GetTextBlockElement("r" + row + "c2").Text = c.economic.ToString();
                        GetTextBlockElement("r" + row + "c3").Text = c.land.ToString();
                        GetTextBlockElement("r" + row + "c4").Text = c.cultivation.ToString();
                    }
                    else if (type == 2)
                    {
                        GetTextBlockElement("r" + row + "c0").Text = c.flood_control.ToString();
                        GetTextBlockElement("r" + row + "c1").Text = c.tax.ToString();
                        GetTextBlockElement("r" + row + "c2").Text = c.order.ToString();
                        GetTextBlockElement("r" + row + "c3").Text = "";
                        GetTextBlockElement("r" + row + "c4").Text = "";
                    }
                }

                row++;
            }
        }

        public override void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            Util.Invoke(() =>
            {
                if (key == GameKey.Backward)
                {

                }
                else if (key == GameKey.Left || key == GameKey.Right)
                {
                    type = (type + 1) % 3;
                    UpdateView();
                }
                else if (key == GameKey.Up)
                {
                    pageIndex--;
                    if (pageIndex == -1)
                        pageIndex = maxPageCount - 1;
                    UpdateView();
                }
                else if (key == GameKey.Down)
                {
                    pageIndex = (pageIndex + 1) % maxPageCount;
                    UpdateView();
                }
            });

        }
    }
}
