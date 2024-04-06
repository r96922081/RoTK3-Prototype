using System.Collections.Generic;
using System.Windows;

namespace Rtk3Mod
{
    public class SelectCityToSpyState : State
    {
        City city;
        SelectCityUi selectCityUi;

        public void Init(City city)
        {
            this.city = city;
        }

        public void EnterState()
        {
            selectCityUi = new SelectCityUi();
            List<City> availableCities = new List<City>();

            foreach (City c in Gv.g.cities.Values)
            {
                if (c.ruler == null || c.ruler == "" || c.ruler == city.ruler)
                    continue;
                availableCities.Add(c);
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
                        Gv.selectOfficerToSpyState.Init(city, selectCityUi.targetCity);
                        Gv.SetNextState(Gv.selectOfficerToSpyState);
                    }
                }
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                selectCityUi.RemoveAll();
                Gv.statusRectangleUi.Show();
                selectCityUi.Hide();
            });
        }
    }
}
