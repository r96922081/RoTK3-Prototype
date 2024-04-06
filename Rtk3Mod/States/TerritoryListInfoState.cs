namespace Rtk3Mod
{
    public class TerritoryListInfoState : State
    {
        City city;

        public void Init(City city)
        {
            this.city = city;
            Gv.territoryListInfoUi.Init(city);
        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            Gv.territoryListInfoUi.Update(key);

            Util.Invoke(() =>
            {
                if (key == GameKey.Backward)
                {
                    Gv.SetNextState(Gv.intelligenceMenuState);
                }
            });

        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetPlainText("上下左右切換，B結束");
                Gv.territoryListInfoUi.UpdateView();
                Gv.territoryListInfoUi.Show();
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetPleaseOrder(city);
                Gv.territoryListInfoUi.Hide();
            });
        }
    }
}
