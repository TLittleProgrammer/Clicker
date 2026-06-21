using Data;
using Infrastructure.StateMachine;
using GameStateMachine = Infrastructure.StateMachine.StateMachine;

namespace Infrastructure
{
    public sealed class ProjectContext
    {
        public readonly IDependencyContainer DependencyContainer;
        public readonly IStateMachine GameStateMachine;
        public readonly GameRules GameRules;

        public ProjectContext(DependencyDictionaryContainer dependencyContainer, GameStateMachine stateMachine)
        {
            DependencyContainer = dependencyContainer;
            GameStateMachine = stateMachine;
            GameRules = new GameRules();
        }
    }
}