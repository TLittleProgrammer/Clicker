using System;
using Data;
using Data.Enemies;
using Data.Game;
using Infrastructure.ResourcesProvider;
using Infrastructure.StateMachine;

namespace Infrastructure.States
{
    public class ResourcesLoadState : IEnterableState
    {
        private readonly GameRules _gameRules;
        private readonly IResourcesProvider _resourcesProvider;
        private readonly IStateMachine _stateMachine;

        public ResourcesLoadState(GameRules gameRules, IResourcesProvider resourcesProvider, IStateMachine stateMachine)
        {
            _gameRules = gameRules;
            _resourcesProvider = resourcesProvider;
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            _gameRules.Enemies = await _resourcesProvider.LoadAsset<EnemiesData>(ResourcesConstants.EnemiesDataPath);
            _gameRules.Game = await _resourcesProvider.LoadAsset<GameData>(ResourcesConstants.GameDataPath);
            
            _stateMachine.Enter<SceneLoadState, SceneTypes, Action>(SceneTypes.Game, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            _stateMachine.Enter<PreparationGameState>();
        }

        public void Exit() { }
    }
}