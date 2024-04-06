namespace Rtk3Mod
{
    public class SelectOfficerToDevState : State
    {
        City city;
        SelectOfficerUi selectOfficerUi = new SelectOfficerUi();
        JobType jobType;

        public void Init(City city, JobType jobType)
        {
            this.city = city;
            this.jobType = jobType;
            selectOfficerUi.Init(city, OfficerSelectionTypeEnum.job, OfficerSelectionAbilityEnum.political_ability, 3);
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

                    if (selectOfficerUi.selectedOfficers.Count == 0)
                    {
                        Gv.SetNextState(Gv.devMenuState);
                        Gv.cityInfoSmallUi.Show();
                    }
                    else
                    {
                        /*
                        bool only1Month = false;
                        foreach (var o in officerSelectionUi.selectedOfficers)
                        {
                            if (o.rank != RankEnum.MilitaryOfficer && o.rank != RankEnum.CivilOfficer)
                                only1Month = true;
                        }

                        if (only1Month)*/
                        if (true) // todo, support at most 6 months
                        {
                            Gv.setDevGoldState.Init(3, 100, 1,
                                () =>
                                {
                                    Util.Invoke(() =>
                                    {
                                        int money = Gv.setDevGoldState.setValueUi.GetValue();
                                        city.gold -= money;
                                        Gv.cityInfoSmallUi.Update(city);
                                        Gv.statusRectangleUi.StartDev(jobType, selectOfficerUi.selectedOfficers, 1, money);
                                        Gv.c.SetDev(selectOfficerUi.selectedOfficers, jobType, city, 1, money);

                                        Gv.idleState.Init(1000, () =>
                                        {
                                            Util.Invoke(() =>
                                            {
                                                Gv.statusRectangleUi.SetPleaseOrder(city);
                                                Gv.cityInfoSmallUi.Show();
                                                Gv.SetNextState(Gv.devMenuState);
                                            });
                                        });
                                        Gv.SetNextState(Gv.idleState);
                                    });
                                },
                                () =>
                                {
                                    Gv.SetNextState(Gv.selectOfficerToDevState);
                                },
                                "多少錢？(1 - 100)"
                            );

                            Gv.SetNextState(Gv.setDevGoldState);
                        }
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
