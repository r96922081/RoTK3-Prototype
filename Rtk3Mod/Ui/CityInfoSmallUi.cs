namespace Rtk3Mod
{
    public class CityInfoSmallUi : RectangleBaseUi
    {
        static int x = 200;
        static int y = 3;

        public CityInfoSmallUi() : base(280, 334)
        {
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\\UiDesign", "city_info_small_ui_design.json");

            Util.LoadUiDesign(path, this);
            Remove("back_rect");
            SetTopLeftPosition(x, y);
            SetBackAlpha(220);
            Hide();
        }

        public void Update(City c)
        {
            GetTextBlockElement("flag").Background = c.GetBrush();
            GetTextBlockElement("name").Text = c.name;
            GetTextBlockElement("ruler").Text = c.ruler;
            GetTextBlockElement("governer").Text = c.governer;
            GetTextBlockElement("stragetist").Text = c.stragetist;
            GetTextBlockElement("population").Text = c.population.ToString();
            GetTextBlockElement("gold").Text = c.gold.ToString();
            GetTextBlockElement("food").Text = c.food.ToString();
            GetTextBlockElement("troops").Text = c.Troops.ToString();
            GetTextBlockElement("land").Text = c.land.ToString();
            GetTextBlockElement("cultivation").Text = c.cultivation.ToString();
            GetTextBlockElement("flood_control").Text = c.flood_control.ToString();
            GetTextBlockElement("economic").Text = c.economic.ToString();
            GetTextBlockElement("order").Text = c.order.ToString();
            GetTextBlockElement("tax").Text = c.tax.ToString();
            GetTextBlockElement("officers").Text = c.regular_officers.Count.ToString();
        }
    }
}
