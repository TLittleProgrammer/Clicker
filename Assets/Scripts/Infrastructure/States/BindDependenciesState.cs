using System.Threading;
using Infrastructure.ResourcesProvider;
using Infrastructure.StateMachine;
using SceneLoad;

namespace Infrastructure.States
{
    public class BindDependenciesState : IEnterableState
    {
        private readonly ProjectContext _projectContext;

        private IDependencyContainer DiContainer => _projectContext.DependencyContainer;

        public BindDependenciesState(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public void Enter()
        {
            BindClasses();
            BindStates();
            
            _projectContext.GameStateMachine.Enter<ResourcesLoadState>();
        }

        public void Exit() { }

        private void BindClasses()
        {
            var sceneLoader = new SceneLoader();
            var addressablessProvider = new AddressablessProvider();
            
            DiContainer.Register<ISceneLoader>(sceneLoader);
            DiContainer.Register<IResourcesProvider>(addressablessProvider);
        }

        private void BindStates()
        {
            var sceneLoad = new SceneLoadState(DiContainer.Resolve<ISceneLoader>());
            var resourcesLoad = new ResourcesLoadState(_projectContext.GameRules, DiContainer.Resolve<IResourcesProvider>(), _projectContext.GameStateMachine);
            var preparation = new PreparationGameState(_projectContext.GameRules);
            
            DiContainer.Register(sceneLoad);
            DiContainer.Register(resourcesLoad);
            DiContainer.Register(preparation);
        }
    }
}