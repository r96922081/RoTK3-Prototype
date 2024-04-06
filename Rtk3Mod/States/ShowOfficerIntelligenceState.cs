namespace Rtk3Mod
{
    public class ShowOfficerIntelligenceState : State
    {
        OfficerInfoUi officerInfoUi = new OfficerInfoUi();

        public void Init(Officer o)
        {
            officerInfoUi.Init(o);
        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            Util.Invoke(() =>
            {
                officerInfoUi.Hide();
                Gv.SetNextState(Gv.selectIntelligenceOfficerState);
            });
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetText("按任意鍵結束");
                officerInfoUi.Show();
            });
        }

        public void ExitState()
        {

        }
    }
}
