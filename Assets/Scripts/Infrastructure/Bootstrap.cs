using Infrastructure.StateMachine;
using Infrastructure.States;
using UnityEngine;
using GameStateMachine = Infrastructure.StateMachine.StateMachine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private ProjectContext _projectContext;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            var dependencyContainer = new DependencyDictionaryContainer();
            var stateFactory = new StateFactory(dependencyContainer);
            var stateMachine = new GameStateMachine(stateFactory);
            _projectContext = new ProjectContext(dependencyContainer, stateMachine);

            dependencyContainer.Register(new BindDependenciesState(_projectContext));
            
            _projectContext.GameStateMachine.Enter<BindDependenciesState>();
        }

        private void Update()
        {
            _projectContext.GameStateMachine.Tick();
        }
    }
}