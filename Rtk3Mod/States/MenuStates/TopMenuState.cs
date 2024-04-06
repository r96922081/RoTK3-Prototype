using System;
using System.Collections.Generic;

namespace Rtk3Mod
{
    public class TopMenuState : MenuState
    {
        public TopMenuState() : base(new List<string> { "情報", "開發", "交易", "特別" })
        {
            SetZIndex(Gv.z_index_level_6);
        }

        public override void Update(GameKey key)
        {
            base.Update(key);

            Util.Invoke(new Action(() =>
            {
                if (key == GameKey.Forward)
                {
                    if (index == 0)
                    {
                        Gv.intelligenceMenuState.Init(city);
                        Gv.SetNextState(Gv.intelligenceMenuState);
                    }
                    else if (index == 1)
                    {
                        Gv.devMenuState.Init(city);
                        Gv.SetNextState(Gv.devMenuState);
                    }
                    else if (index == 2)
                    {
                        Gv.tradeMenuState.Init(city);
                        Gv.SetNextState(Gv.tradeMenuState);
                    }
                    else if (index == 3)
                    {
                        Gv.specialMenuState.Init(city);
                        Gv.SetNextState(Gv.specialMenuState);
                    }
                }

                else if (key == GameKey.Backward)
                {
                    Gv.endCityYesNoState.Init(city);
                    Gv.SetNextState(Gv.endCityYesNoState);
                }
            }));
        }

        public override void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.cityInfoSmallUi.Update(city);
                Gv.cityInfoSmallUi.Show();
                Gv.statusRectangleUi.SetPleaseOrder(city);

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
