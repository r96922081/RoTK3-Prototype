using System;
using System.Collections.Generic;

namespace Rtk3Mod
{
    public class HomeCityMenuState : MenuState
    {
        Action backAction;
        Action nextMenuBackAction;

        public HomeCityMenuState() : base(new List<string> { "武將", "一覽", "都市" })
        {
        }

        public void Init(City city, Action backAction, Action nextMenuBackAction)
        {
            this.city = city;
            this.backAction = backAction;
            this.nextMenuBackAction = nextMenuBackAction;
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
                        Gv.selectIntelligenceOfficerState.Init(city, nextMenuBackAction);
                        Gv.SetNextState(Gv.selectIntelligenceOfficerState);
                    }
                    else if (index == 1)
                    {
                        Gv.officerListIntelligenceState.Init(city, nextMenuBackAction);
                        Gv.SetNextState(Gv.officerListIntelligenceState);
                    }
                    else if (index == 2)
                    {
                        Gv.showCityInfoState.Init(city, nextMenuBackAction);
                        Gv.SetNextState(Gv.showCityInfoState);
                    }
                }

                else if (key == GameKey.Backward)
                {
                    backAction();
                }
            }));
        }

        public override void EnterState()
        {
            Util.Invoke(() =>
            {
                Show();
                SetMenuIndex(0);
                Gv.selectIntelligenceOfficerState.Init(city, nextMenuBackAction);
                Gv.officerListIntelligenceState.Init(city, nextMenuBackAction);
                Gv.statusRectangleUi.SetPleaseOrder(city);
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
