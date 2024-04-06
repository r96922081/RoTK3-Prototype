namespace Rtk3Mod
{
    public class CityInfoUi : RectangleBaseUi
    {
        public City city;

        public CityInfoUi() : base(Gv.screen_right - Gv.screen_left, Gv.status_top - Gv.screen_top)
        {
            Util.LoadUiDesign(Util.GetUiJson("city_info_rectangle_dialog_ui_design.json"), this);
            SetTopLeftPosition(Gv.screen_left, 1);
            Hide();
        }

        public void Init(City c)
        {
            this.city = c;
            SetZIndex(Gv.z_index_level_6);

            GetTextBlockElement("city_name").Text = c.name;
            GetTextBlockElement("ruler").Text = c.ruler;
            GetTextBlockElement("governer").Text = c.governer;
            GetTextBlockElement("strategist").Text = c.stragetist;
            GetTextBlockElement("flag").Background = c.GetBrush();
            GetTextBlockElement("population").Text = c.population.ToString();
            GetTextBlockElement("gold").Text = c.gold.ToString();
            GetTextBlockElement("food").Text = c.food.ToString();
            GetTextBlockElement("troops").Text = c.Troops.ToString();
            GetTextBlockElement("officer").Text = c.regular_officers.Count.ToString();
            GetTextBlockElement("economic").Text = c.economic.ToString();
            GetTextBlockElement("land_dev").Text = c.land.ToString();
            GetTextBlockElement("cutivation").Text = c.cultivation.ToString();
            GetTextBlockElement("flood_control").Text = c.flood_control.ToString();
            GetTextBlockElement("tax_rate").Text = c.tax.ToString();
            GetTextBlockElement("order").Text = c.order.ToString();
            GetTextBlockElement("cross_bow").Text = c.crossbow_count.ToString();
            GetTextBlockElement("strong_cross_bow").Text = c.strong_crossbow_count.ToString();
            GetTextBlockElement("horse").Text = c.horse_count.ToString();
            GetTextBlockElement("food_selling_price").Text = c.food_selling_price.ToString();
            GetTextBlockElement("food_buying_price").Text = c.food_buying_price.ToString();
            GetTextBlockElement("cross_bow_price").Text = c.crossbow_price.ToString();
            GetTextBlockElement("strong_cross_bow_price").Text = c.strong_crossbow_price.ToString();
            GetTextBlockElement("horse_price").Text = c.horse_price.ToString();
        }
    }
}
