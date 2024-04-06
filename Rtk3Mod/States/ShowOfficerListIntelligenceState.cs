using System;

namespace Rtk3Mod.States
{
    public class ShowOfficerListIntelligenceState : State
    {
        City city;
        OfficerListInfoUi officerListInfoUi = new OfficerListInfoUi();
        Action backAction;

        public void Init(City city, Action backAction)
        {
            this.city = city;
            officerListInfoUi.Init(city);
            this.backAction = backAction;
        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            officerListInfoUi.Update(key);

            if (key == GameKey.Backward)
            {
                Util.Invoke(() =>
                {
                    officerListInfoUi.Hide();
                });
                backAction();
            }
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetText("上下左右切換，B結束");
                officerListInfoUi.UpdateView();
                officerListInfoUi.Show();
            });
        }

        public void ExitState()
        {

        }
    }
}
