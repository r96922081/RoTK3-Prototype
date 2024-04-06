namespace Rtk3Mod.New
{
    public class DateRectangle : TextRectangleUi
    {
        public DateRectangle() : base(145, 55)
        {
            SetZIndex(Gv.z_index_level_2);
            SetTopLeftPosition(Gv.screen_left, Gv.screen_top);
        }

        public void Update()
        {
            SetWhiteText(string.Format("{0}年{1}月", Gv.g.year, Gv.g.month));
        }
    }
}
