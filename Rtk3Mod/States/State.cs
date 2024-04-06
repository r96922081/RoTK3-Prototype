namespace Rtk3Mod
{
    public interface State
    {
        void EnterState();
        void Update(GameKey key);
        void ExitState();
    }
}
