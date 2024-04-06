using System.Collections.Generic;
using System.Windows;

namespace Rtk3Mod
{
    public class SelectCityToWatchState : State
    {
        City city;
        SelectCityUi selectCityUi = new SelectCityUi();

        public void Init(City city)
        {
            this.city = city;
        }

        public void EnterState()
        {
            List<City> availableCities = new List<City>();

            foreach (City c in Gv.g.cities.Values)
            {
                if (c.ruler == null || c.ruler == "" || c.ruler == city.ruler)
                    availableCities.Add(c);

                foreach (City c2 in Gv.c.GetForce(city.ruler).spiedCities)
                    availableCities.Add(c2);
            }

            Util.Invoke(() =>
            {
                selectCityUi.Init(city, availableCities);
                selectCityUi.Show();
                selectCityUi.GetElement("border").Visibility = Visibility.Hidden;
                Gv.statusRectangleUi.Hide();
            });
        }

        public void Update(GameKey key)
        {
            selectCityUi.Update(key);

            if (key == GameKey.None)
                return;

            Util.Invoke(() =>
            {
                if (key == GameKey.Backward)
                {
                    Gv.statusRectangleUi.Show();
                    Gv.statusRectangleUi.SetPleaseOrder(city);
                    Gv.cityInfoSmallUi.Show();
                    Gv.SetNextState(Gv.intelligenceMenuState);
                }
                else if (key == GameKey.Forward)
                {
                    if (selectCityUi.targetCity != null)
                    {
                        if (selectCityUi.targetCity.ruler == "")
                        {
                            Gv.showCityInfoState.Init(selectCityUi.targetCity, () =>
                            {
                                Util.Invoke(() =>
                                {
                                    Gv.statusRectangleUi.Hide();
                                });
                                Gv.SetNextState(Gv.selectCityToWatchState);
                            });
                            Gv.SetNextState(Gv.showCityInfoState);
                        }
                        else
                        {
                            Gv.otherCityMenuState.Init(selectCityUi.targetCity,
                                () =>
                                {
                                    Gv.statusRectangleUi.Hide();
                                    Gv.SetNextState(Gv.selectCityToWatchState);
                                },
                                () =>
                                {
                                    Util.Invoke(() =>
                                    {
                                        Gv.statusRectangleUi.SetPleaseOrder(city);
                                        Gv.SetNextState(Gv.otherCityMenuState);
                                    });
                                });
                            Gv.SetNextState(Gv.otherCityMenuState);
                        }
                    }
                }
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.Show();
                selectCityUi.Hide();
            });
        }
    }
}
