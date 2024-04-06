using System;

namespace Rtk3Mod.States
{
    public class ShowCityInfoState : State
    {
        City infoCity;
        CityInfoUi cityInfoUi = new CityInfoUi();
        Action backAction;

        public void Init(City infoCity, Action backAction)
        {
            this.infoCity = infoCity;
            this.backAction = backAction;
            cityInfoUi.Init(infoCity);
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                cityInfoUi.Show();
                Gv.statusRectangleUi.SetText("按任意鍵結束");
            });
        }

        public void ExitState()
        {

        }

        public void Update(GameKey key)
        {
            if (key != GameKey.None)
            {
                Util.Invoke(() =>
                {
                    cityInfoUi.Hide();
                });
                backAction();
            }
        }
    }
}
