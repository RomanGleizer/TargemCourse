public interface IPathogenState
{
    void Enter(PathogenController pathogen);
    void Update(PathogenController pathogen);
    void Exit(PathogenController pathogen);
}