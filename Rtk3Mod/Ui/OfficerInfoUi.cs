using System;

namespace Rtk3Mod
{
    public class OfficerInfoUi : RectangleBaseUi
    {
        public Action backwardAction;

        public OfficerInfoUi() : base(Gv.screen_right - Gv.screen_left, Gv.status_top - Gv.screen_top)
        {
            Util.LoadUiDesign(Util.GetUiJson("officer_info_rectangle_dialog_ui_design.json"), this);
            SetTopLeftPosition(Gv.screen_left, 0);
            Hide();
        }

        public void Init(Officer officer)
        {
            GetTextBlockElement("name").Text = officer.name;
            GetTextBlockElement("rank").Text = Officer.GetStringFromRankEnum(officer.rank);
            GetTextBlockElement("city").Text = officer.location.name;
            GetTextBlockElement("loyalty").Text = officer.loyalty.ToString();
            if (GetTextBlockElement("loyalty").Text == "-1")
                GetTextBlockElement("loyalty").Text = "---";
            GetTextBlockElement("troops").Text = officer.troops.ToString();
            if (GetTextBlockElement("troops").Text == "-1")
                GetTextBlockElement("troops").Text = "---";
            GetTextBlockElement("intelligence").Text = officer.intelligence.ToString();
            GetTextBlockElement("political_ability").Text = officer.political_ability.ToString();
            GetTextBlockElement("war").Text = officer.war_ability.ToString();
            GetTextBlockElement("charm").Text = officer.charm.ToString();
            GetTextBlockElement("army").Text = officer.army.ToString();
            GetTextBlockElement("training").Text = officer.training.ToString();
            if (GetTextBlockElement("training").Text == "-1")
                GetTextBlockElement("training").Text = "---";
        }
    }
}
