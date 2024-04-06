using System;
using System.Collections.Generic;

namespace Rtk3Mod
{
    public class IntelligenceMenuState : MenuState
    {
        public IntelligenceMenuState() : base(new List<string> { "間諜", "本國", "他國", "領土" })
        {
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
                        Gv.selectCityToSpyPromptState.Init(city);
                        Gv.SetNextState(Gv.selectCityToSpyPromptState);
                    }
                    else if (index == 1)
                    {
                        Gv.homeCityMenuState.Init(city, () =>
                        {
                            Gv.SetNextState(Gv.intelligenceMenuState);
                        },
                        () =>
                        {
                            Util.Invoke(() =>
                            {
                                Gv.cityInfoSmallUi.Show();
                                Gv.statusRectangleUi.SetPleaseOrder(city);
                                Gv.SetNextState(Gv.homeCityMenuState);
                            });
                        });
                        Gv.SetNextState(Gv.homeCityMenuState);
                    }
                    else if (index == 2)
                    {
                        Gv.selectCityToWatchPromptState.Init(city);
                        Gv.SetNextState(Gv.selectCityToWatchPromptState);
                    }
                    else if (index == 3)
                    {
                        Gv.territoryListInfoState.Init(city);
                        Gv.SetNextState(Gv.territoryListInfoState);
                    }
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
