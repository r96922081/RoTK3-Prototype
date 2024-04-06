using System;

namespace Rtk3Mod.States
{
    public class SelectOfficerIntelligenceState : State
    {
        City city;
        SelectOfficerUi selectOfficerUi = new SelectOfficerUi();
        Action backAction;

        public void Init(City city, Action backAction)
        {
            this.city = city;
            this.backAction = backAction;
            selectOfficerUi.Init(city, OfficerSelectionTypeEnum.info, OfficerSelectionAbilityEnum.position, 1);
        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            selectOfficerUi.Update(key);

            if (key == GameKey.Backward)
            {
                Util.Invoke(() =>
                {
                    selectOfficerUi.Hide();
                });
                backAction();
            }
            else if (key == GameKey.Forward)
            {
                Util.Invoke(() =>
                {
                    int officer_index = selectOfficerUi.current_page * SelectOfficerUi.max_row_count_in_one_page + selectOfficerUi.current_row;
                    Gv.showIntelligenceOfficerState.Init(city.regular_officers[officer_index]);
                    Gv.SetNextState(Gv.showIntelligenceOfficerState);
                });
            }
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                selectOfficerUi.Show();
                Gv.cityInfoSmallUi.Hide();
                Gv.statusRectangleUi.SetText("請選擇武將");
            });
        }

        public void ExitState()
        {

        }
    }
}
