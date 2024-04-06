namespace Rtk3Mod
{
    public class TurnControllerState : State
    {
        public void EnterState()
        {

        }

        public void Update(GameKey key)
        {
            if (Gv.g.remainingCity.Count == 0)
            {
                Gv.g.remainingForce.RemoveAt(0);

                if (Gv.g.remainingForce.Count == 0)
                {
                    AdvanceMonth();
                    return;
                }

                Gv.g.remainingCity = Gv.g.remainingForce[0].GetCities();
            }

            if (Gv.g.players.Contains(Gv.g.remainingForce[0].ruler))
            {
                Gv.topMenuState.Init(Gv.g.remainingCity[0]);
                Gv.SetNextState(Gv.topMenuState);
            }
            else
            {
                Gv.npcPlottingState.Init(Gv.g.remainingCity[0]);
                Gv.SetNextState(Gv.npcPlottingState);
            }
        }

        public void ExitState()
        {

        }

        public void AdvanceMonth()
        {
            Gv.g.month += 1;
            if (Gv.g.month == 13)
            {
                Gv.g.month = 1;
                Gv.g.year++;
            }

            for (int i = 0; i < Gv.g.forces.Count; i++)
            {
                Force f = Gv.g.forces[i];
                Gv.c.ForceAdvanceMonth(f);
            }

            Gv.g.remainingForce.AddRange(Gv.g.forces);
            Gv.g.remainingCity = Gv.g.remainingForce[0].GetCities();

            Util.Invoke(() =>
            {
                if (Gv.g.month == 1)
                {
                    Gv.backgroundUi.SetColor(Gv.spring);
                }
                else if (Gv.g.month == 4)
                {
                    Gv.backgroundUi.SetColor(Gv.summer);
                }
                else if (Gv.g.month == 7)
                {
                    Gv.backgroundUi.SetColor(Gv.autum);
                }
                else if (Gv.g.month == 10)
                {
                    Gv.backgroundUi.SetColor(Gv.winter);
                }

                Gv.dateRectangle.Update();
                Gv.statusRectangleUi.SetText("");
            });
        }
    }
}
