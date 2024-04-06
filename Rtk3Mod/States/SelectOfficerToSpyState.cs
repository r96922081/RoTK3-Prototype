namespace Rtk3Mod.States
{
    public class SelectOfficerToSpyState : State
    {
        City city;
        City spiedCity;
        SelectOfficerUi selectOfficerUi = new SelectOfficerUi();

        public void Init(City city, City spiedCity)
        {
            this.city = city;
            this.spiedCity = spiedCity;
            selectOfficerUi.Init(city, OfficerSelectionTypeEnum.job, OfficerSelectionAbilityEnum.position, 1);
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
                    Gv.SetNextState(Gv.selectCityToSpyState);
                });
            }
            else if (key == GameKey.Forward)
            {
                Util.Invoke(() =>
                {
                    if (selectOfficerUi.selectedOfficers.Count == 1)
                    {
                        Officer officer = selectOfficerUi.selectedOfficers[0];
                        if (officer.rank == RankEnum.Ruler || officer.rank == RankEnum.Govenor || officer.rank == RankEnum.General || officer.rank == RankEnum.Strategist)
                        {
                            Gv.c.SetSpy(officer, spiedCity, 1);
                            Gv.statusRectangleUi.StartSpy(officer, spiedCity, 1);

                            Gv.idleState.Init(1000, () =>
                            {
                                Util.Invoke(() =>
                                {
                                    Gv.statusRectangleUi.SetPleaseOrder(city);
                                    Gv.cityInfoSmallUi.Show();
                                    Gv.SetNextState(Gv.intelligenceMenuState);
                                });
                            });
                            Gv.SetNextState(Gv.idleState);
                        }
                        else
                        {
                            Gv.setSpyMonthState.Init(1, 6, 1,
                                () =>
                                {
                                    Util.Invoke(() =>
                                    {
                                        int month = Gv.setSpyMonthState.setValueUi.GetValue();

                                        Gv.statusRectangleUi.StartSpy(officer, spiedCity, month);
                                        Gv.c.SetSpy(officer, spiedCity, month);

                                        Gv.idleState.Init(1000, () =>
                                        {
                                            Util.Invoke(() =>
                                            {
                                                Gv.statusRectangleUi.SetPleaseOrder(city);
                                                Gv.cityInfoSmallUi.Show();
                                                Gv.SetNextState(Gv.intelligenceMenuState);
                                            });
                                        });
                                        Gv.SetNextState(Gv.idleState);
                                    });
                                },
                                () =>
                                {
                                    Util.Invoke(() =>
                                    {
                                        Gv.SetNextState(Gv.selectOfficerToSpyState);
                                    });
                                }, "要幾個月？(1 - 6)");
                            Gv.SetNextState(Gv.setSpyMonthState);
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
