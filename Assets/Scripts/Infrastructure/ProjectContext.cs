using Infrastructure.StateMachine;
using GameStateMachine = Infrastructure.StateMachine.StateMachine;

namespace Infrastructure
{
    public sealed class ProjectContext
    {
        public readonly IDependencyContainer DependencyContainer;
        public readonly IStateMachine GameStateMachine;

        public ProjectContext(DependencyDictionaryContainer dependencyContainer, GameStateMachine stateMachine)
        {
            DependencyContainer = dependencyContainer;
            GameStateMachine = stateMachine;
        }
    }
}