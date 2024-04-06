using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class SelectOfficerToDoCharityState : State
    {
        City city;
        SelectOfficerUi selectOfficerUi = new SelectOfficerUi();

        public void Init(City city)
        {
            this.city = city;
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
                    Gv.SetNextState(Gv.specialMenuState);
                });
            }
            else if (key == GameKey.Forward)
            {
                if (selectOfficerUi.selectedOfficers.Count == 1)
                {
                    Gv.setCharityValueState.Init(city.food.ToString().Length, city.food, 1,
                    () =>
                    {
                        Util.Invoke(() =>
                        {
                            int food = Gv.setCharityValueState.setValueUi.GetValue();
                            Gv.c.SetDoCharity(selectOfficerUi.selectedOfficers[0], city, food);

                            List<Tuple<string, Color, TextAlignment>> texts2 = new List<Tuple<string, Color, TextAlignment>>();
                            texts2.Add(Tuple.Create("已完成對", Colors.White, TextAlignment.Left));
                            texts2.Add(Tuple.Create(city.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts2.Add(Tuple.Create("的施捨", Colors.White, TextAlignment.Left));
                            Gv.statusRectangleUi.SetTextList(texts2);
                            Gv.SetNextState(Gv.specialMenuState);

                            Gv.idleState.Init(1000, () =>
                            {
                                Util.Invoke(() =>
                                {
                                    Gv.SetNextState(Gv.specialMenuState);
                                });
                            });
                            Gv.SetNextState(Gv.idleState);
                        });
                    },
                    () =>
                    {
                        Gv.SetNextState(Gv.selectOfficerToDoCharityState);
                    }, "請設定拖捨的米(0 - " + city.food + ")");
                    Gv.SetNextState(Gv.setCharityValueState);
                }
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
