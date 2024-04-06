using System;

namespace Rtk3Mod
{
    public class SetValueState : State
    {
        public SetValueUi setValueUi;
        int digit;
        int minValue;
        int maxValue;
        Action forwardAction;
        Action backwardAction;
        string status;

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                setValueUi = new SetValueUi();
                setValueUi.Init(digit, maxValue, minValue);
                setValueUi.SetDefaultPosition();
                setValueUi.Show();
                Gv.statusRectangleUi.SetText(status);
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                setValueUi.RemoveAll();
            });
        }

        public void Init(int digit, int maxValue, int minValue, Action forwardAction, Action backwardAction, string status)
        {
            this.digit = digit;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.forwardAction = forwardAction;
            this.backwardAction = backwardAction;
            this.status = status;
        }

        public void Update(GameKey key)
        {
            setValueUi.Update(key);

            if (key == GameKey.Backward)
            {
                backwardAction();
            }
            else if (key == GameKey.Forward)
            {
                forwardAction();
            }
        }
    }
}
