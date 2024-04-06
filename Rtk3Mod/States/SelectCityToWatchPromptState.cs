namespace Rtk3Mod
{
    public class SelectCityToWatchPromptState : State
    {
        City city;

        public void Init(City city)
        {
            this.city = city;
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetText("要看哪個城市？");
                Gv.cityInfoSmallUi.Hide();
            });
        }

        public void ExitState()
        {

        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            Gv.selectCityToWatchState.Init(city);
            Gv.SetNextState(Gv.selectCityToWatchState);
        }
    }
}
