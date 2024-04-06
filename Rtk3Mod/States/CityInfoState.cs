namespace Rtk3Mod
{
    public class CityInfoState : State
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
                Gv.statusRectangleUi.SetText("按任意鍵結束");
            });
        }

        public void ExitState()
        {

        }

        public void Update(GameKey key)
        {
            if (key == GameKey.None)
                return;

            Gv.SetNextState(Gv.intelligenceMenuState);
        }
    }
}
