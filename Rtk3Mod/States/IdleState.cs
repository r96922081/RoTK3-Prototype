using System;

namespace Rtk3Mod
{
    public class IdleState : State
    {
        DateTime startTime;
        int waitMs = 0;
        Action endAction;

        public void Init(int waitMs, Action endAction)
        {
            this.waitMs = waitMs;
            this.endAction = endAction;
        }

        public void Update(GameKey key)
        {
            TimeSpan timeDiff = DateTime.Now - startTime;

            if (timeDiff.TotalMilliseconds >= waitMs)
            {
                endAction();
            }
        }

        public void EnterState()
        {
            startTime = DateTime.Now;
        }

        public void ExitState()
        {
        }
    }
}
