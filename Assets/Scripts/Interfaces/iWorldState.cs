namespace fi.tamk.hellgame.interfaces
{
    public interface IWorldState
    {
        float TimeScale { get; }

        void Timestep(float deltaTime);
        void OnEnter();
        void OnExit();
    }
}
