namespace Rtk3Mod
{
    public class EndCityYesNoState : State
    {
        City city;

        public void Init(City city)
        {
            this.city = city;
            Gv.yesNoUi.SetSelection(YesNoEnum.Yes);
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.yesNoUi.Show();
                Gv.statusRectangleUi.SetSureToEnd(city);
            });
        }

        public void Update(GameKey key)
        {
            Gv.yesNoUi.Update(key);

            Util.Invoke(() =>
            {
                if (key == GameKey.Backward)
                {
                    Gv.SetNextState(Gv.topMenuState);
                }
                else if (key == GameKey.Forward)
                {
                    if (Gv.yesNoUi.selection == YesNoEnum.No)
                    {
                        Gv.SetNextState(Gv.topMenuState);
                    }
                    else
                    {
                        Gv.g.remainingCity.RemoveAt(0);
                        Gv.cityInfoSmallUi.Hide();
                        Gv.SetNextState(Gv.turnControllerState);
                    }
                }
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                Gv.yesNoUi.Hide();
            });
        }
    }
}
