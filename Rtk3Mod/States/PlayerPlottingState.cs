namespace Rtk3Mod
{
    public class PlayerPlottingState : State
    {
        City city;

        public void Init(City city)
        {
            this.city = city;
        }

        public void Update(GameKey key)
        {

        }

        public void EnterState()
        {
            Gv.topMenuState.Init(Gv.g.remainingCity[0]);
            Gv.SetNextState(Gv.topMenuState);
        }

        public void ExitState()
        {

        }
    }
}
