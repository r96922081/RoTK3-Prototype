namespace Rtk3Mod
{
    public class NpcPlottingState : State
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
            Gv.g.remainingCity.RemoveAt(0);
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetPlotting(city.ruler);
            });

            Gv.idleState.Init(100, () =>
            {
                Gv.SetNextState(Gv.turnControllerState);
            });
            Gv.SetNextState(Gv.idleState);
        }

        public void ExitState()
        {

        }
    }
}
