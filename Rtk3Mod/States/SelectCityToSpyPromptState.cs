namespace Rtk3Mod
{
    public class SelectCityToSpyPromptState : State
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
                Gv.statusRectangleUi.SetText("要向哪個城市派間諜？");
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

            Gv.selectCityToSpyState.Init(city);
            Gv.SetNextState(Gv.selectCityToSpyState);
        }
    }
}
