using System;
using Infrastructure.StateMachine;
using Infrastructure.States;
using SceneLoad;
using UnityEngine;
using GameStateMachine = Infrastructure.StateMachine.StateMachine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            var dependencyContainer = new DependencyDictionaryContainer();
            var stateFactory = new StateFactory(dependencyContainer);
            var stateMachine = new GameStateMachine(stateFactory);

            BindClasses(dependencyContainer);
            BindStates(dependencyContainer);
            
            stateMachine.Enter<SceneLoadState, SceneTypes, Action>(SceneTypes.Game, OnSceneLoaded);
        }

        private void BindClasses(DependencyDictionaryContainer dependencyContainer)
        {
            var sceneLoader = new SceneLoader();
            
            dependencyContainer.Register<ISceneLoader>(sceneLoader);
        }

        private void BindStates(DependencyDictionaryContainer dependencyContainer)
        {
            var state = new SceneLoadState(dependencyContainer.Resolve<ISceneLoader>());
            
            dependencyContainer.Register(state);
        }

        private void OnSceneLoaded()
        {
            Debug.Log("Scene loaded");
        }
    }
}