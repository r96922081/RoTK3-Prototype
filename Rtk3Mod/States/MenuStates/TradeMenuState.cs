using System;
using System.Collections.Generic;

namespace Rtk3Mod
{
    public class TradeMenuState : MenuState
    {
        public TradeMenuState() : base(new List<string> { "買米", "賣米", "買弓", "買馬", "強弓" })
        {
        }

        public override void Update(GameKey key)
        {
            base.Update(key);

            Util.Invoke(new Action(() =>
            {
                if (key == GameKey.Forward)
                {
                    JobType job = JobType.free;

                    if (index == 0)
                    {
                        job = JobType.buyFood;
                    }
                    else if (index == 1)
                    {
                        job = JobType.sellFood;
                    }
                    else if (index == 2)
                    {
                        job = JobType.buyBow;
                    }
                    else if (index == 3)
                    {
                        job = JobType.buyHorse;
                    }
                    else if (index == 4)
                    {
                        job = JobType.buyStrongBow;
                    }

                    Gv.selectOfficerToTradeState.Init(city, job);
                    Gv.SetNextState(Gv.selectOfficerToTradeState);
                }
                else if (key == GameKey.Backward)
                {
                    Gv.SetNextState(Gv.topMenuState);
                }
            }));
        }


        public override void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetPleaseOrder(city);
                Gv.cityInfoSmallUi.Show();
                Show();
            });
        }

        public override void ExitState()
        {
            Util.Invoke(() =>
            {
                Hide();
            });
        }
    }
}
