using System.Windows.Media;

namespace Rtk3Mod
{
    public class OfficerListInfoUi : RectangleBaseUi
    {
        City city;

        int type = 0;

        int maxOfficerCount = 10;
        int officerCount = -1;
        int pageIndex = 0;
        int maxPageCount = -1;

        public OfficerListInfoUi() : base(Gv.screen_right - Gv.screen_left, Gv.status_top - Gv.screen_top)
        {
            Util.LoadUiDesign(Util.GetUiJson("officer_list_info_rectangle_dialog_ui_design.json"), this);
            SetTopLeftPosition(Gv.screen_left, 0);
            SetZIndex(Gv.z_index_level_6);
            Remove("back_rect");
            Hide();
        }

        public void Init(City city)
        {
            this.city = city;

            officerCount = city.regular_officers.Count;
            maxPageCount = officerCount / maxOfficerCount + 1;
        }

        public void UpdateView()
        {
            if (type == 0)
            {
                GetTextBlockElement("r0c2").Text = "忠誠";
                GetTextBlockElement("r0c3").Text = "智力";
                GetTextBlockElement("r0c4").Text = "政治";
                GetTextBlockElement("r0c5").Text = "武力";
            }
            else
            {
                GetTextBlockElement("r0c2").Text = "魅力";
                GetTextBlockElement("r0c3").Text = "指揮";
                GetTextBlockElement("r0c4").Text = "訓練";
                GetTextBlockElement("r0c5").Text = "兵力";
            }

            int row = 1;
            for (int i = pageIndex * 10; i < pageIndex * 10 + 10; i++)
            {
                if (i >= officerCount)
                {
                    GetTextBlockElement("r" + row + "c0").Text = "";
                    GetTextBlockElement("r" + row + "c1").Text = "";
                    GetTextBlockElement("r" + row + "c2").Text = "";
                    GetTextBlockElement("r" + row + "c3").Text = "";
                    GetTextBlockElement("r" + row + "c4").Text = "";
                    GetTextBlockElement("r" + row + "c5").Text = "";
                }
                else
                {
                    Officer o = city.regular_officers[i];

                    SolidColorBrush color = Gv.green;
                    if (o.rank == RankEnum.CivilOfficer || o.rank == RankEnum.MilitaryOfficer)
                        color = Gv.white;

                    GetTextBlockElement("r" + row + "c0").Text = o.name.ToString();
                    GetTextBlockElement("r" + row + "c1").Text = Officer.GetStringFromRankEnum(o.rank);

                    GetTextBlockElement("r" + row + "c0").Foreground = color;
                    GetTextBlockElement("r" + row + "c1").Foreground = color;

                    if (type == 0)
                    {
                        if (o.loyalty == -1)
                            GetTextBlockElement("r" + row + "c2").Text = "--";
                        else
                            GetTextBlockElement("r" + row + "c2").Text = o.loyalty.ToString();
                        GetTextBlockElement("r" + row + "c3").Text = o.intelligence.ToString();
                        GetTextBlockElement("r" + row + "c4").Text = o.political_ability.ToString();
                        GetTextBlockElement("r" + row + "c5").Text = o.war_ability.ToString();
                    }
                    else
                    {
                        GetTextBlockElement("r" + row + "c2").Text = o.charm.ToString();
                        GetTextBlockElement("r" + row + "c3").Text = o.army.ToString();

                        if (o.training == -1)
                            GetTextBlockElement("r" + row + "c4").Text = "--";
                        else
                            GetTextBlockElement("r" + row + "c4").Text = o.training.ToString();

                        if (o.troops == -1)
                            GetTextBlockElement("r" + row + "c5").Text = "--";
                        else
                            GetTextBlockElement("r" + row + "c5").Text = o.troops.ToString();
                    }
                }

                row++;
            }
        }

        public override void Update(GameKey key)
        {
            Util.Invoke(() =>
            {
                if (key == GameKey.Left || key == GameKey.Right)
                {
                    type = (type + 1) % 2;
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
