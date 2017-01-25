namespace fi.tamk.hellgame.interfaces
{
    public interface IWorldState
    {
        void Timestep(float deltaTime);
        void OnEnter();
        void OnExit();
    }
}
