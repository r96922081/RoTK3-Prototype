using System;
using System.Collections.Generic;

namespace Rtk3Mod
{
    public class DevMenuState : MenuState
    {
        public DevMenuState() : base(new List<string> { "土地", "農業", "經濟", "防災" })
        {
        }

        public override void SetMenuIndex(int index)
        {
            this.index = index;
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == index)
                {
                    GetTextBlockElement("menu" + i).Background = Gv.selected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.selected_foreground_brush;
                }
                else
                {
                    GetTextBlockElement("menu" + i).Background = Gv.unselected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.unselected_foreground_brush;

                    if (i == 0)
                    {
                        if (city.HasJob(JobType.land))
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                    else if (i == 1)
                    {
                        if (city.HasJob(JobType.cultivate))
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                    else if (i == 2)
                    {
                        if (city.HasJob(JobType.economics))
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                    else if (i == 3)
                    {
                        if (city.HasJob(JobType.floodControl))
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                }
            }
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
                        if (!city.HasJob(JobType.land))
                        {
                            Gv.selectOfficerToDevState.Init(city, JobType.land);
                            Gv.SetNextState(Gv.selectOfficerToDevState);
                        }
                    }
                    else if (index == 1)
                    {
                        if (!city.HasJob(JobType.cultivate))
                        {
                            Gv.selectOfficerToDevState.Init(city, JobType.cultivate);
                            Gv.SetNextState(Gv.selectOfficerToDevState);
                        }
                    }
                    else if (index == 2)
                    {
                        if (!city.HasJob(JobType.economics))
                        {
                            Gv.selectOfficerToDevState.Init(city, JobType.economics);
                            Gv.SetNextState(Gv.selectOfficerToDevState);
                        }
                    }
                    else if (index == 3)
                    {
                        if (!city.HasJob(JobType.floodControl))
                        {
                            Gv.selectOfficerToDevState.Init(city, JobType.floodControl);
                            Gv.SetNextState(Gv.selectOfficerToDevState);
                        }
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
