namespace Rtk3Mod
{
    public class SelectOfficerToTradeState : State
    {
        City city;
        SelectOfficerUi selectOfficerUi = new SelectOfficerUi();
        JobType jobType;

        public void Init(City city, JobType jobType)
        {
            this.city = city;
            this.jobType = jobType;
            selectOfficerUi.Init(city, OfficerSelectionTypeEnum.job, OfficerSelectionAbilityEnum.charm, 1);
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
                    Gv.SetNextState(Gv.tradeMenuState);
                });
            }
            else if (key == GameKey.Forward)
            {
                Util.Invoke(() =>
                {
                    if (selectOfficerUi.selectedOfficers.Count == 1)
                    {
                        Gv.setTradeQuantityState.Init(city, jobType, selectOfficerUi.selectedOfficers[0]);
                        Gv.SetNextState(Gv.setTradeQuantityState);
                    }
                });
            }
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                selectOfficerUi.Show();
                selectOfficerUi.selectedOfficers.Clear();
                selectOfficerUi.UpdateCurrentPage();

                Gv.cityInfoSmallUi.Hide();
                Gv.statusRectangleUi.SetText("請選擇武將");
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                selectOfficerUi.Hide();
            });
        }
    }
}
