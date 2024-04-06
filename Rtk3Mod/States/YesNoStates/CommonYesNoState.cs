using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class CommonYesNoState : State
    {
        City city;
        List<Tuple<string, Color, TextAlignment>> texts;
        Action backwardAction;
        Action forwardAction;

        public void Init(City city, List<Tuple<string, Color, TextAlignment>> texts, Action forwardAction, Action backwardAction)
        {
            this.city = city;
            this.texts = texts;
            this.backwardAction = backwardAction;
            this.forwardAction = forwardAction;
            Gv.yesNoUi.SetSelection(YesNoEnum.Yes);
        }

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.yesNoUi.Show();
                Gv.statusRectangleUi.SetTextList(texts);
            });
        }

        public void Update(GameKey key)
        {
            Gv.yesNoUi.Update(key);

            Util.Invoke(() =>
            {
                if (key == GameKey.Backward)
                {
                    backwardAction();
                }
                else if (key == GameKey.Forward)
                {
                    if (Gv.yesNoUi.selection == YesNoEnum.No)
                    {
                        backwardAction();
                    }
                    else
                    {
                        forwardAction();
                    }
                }
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                Gv.yesNoUi.Hide();
            });
        }
    }
}
