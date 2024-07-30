using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.GameManager
{
    public enum GameState
    {
        OFF = 0,
        PLAYING = 1,
        PAUSED = 2,
        FINISHED = 3
    }
    
    public sealed class GameManager: MonoBehaviour
    {
        [SerializeField]
        private bool _autoRun = true;

        private bool _initiatedModules = false;

        public void ModulesInitFinished() => _initiatedModules = true;

        [ShowInInspector, ReadOnly]
        public GameState State => this.gameMachine.State;

        private readonly GameMachine gameMachine = new GameMachine();

        private void Start()
        {
            if (_initiatedModules)
            {
                if (this._autoRun)
                {
                    this.InitGame();
                    //Debug.LogWarning("[GameManager]: StartGame not use module StartFinisUI");
                    //this.StartGame();     //StartGame will make through module StartFinisUI
                }
            }
            else
                Debug.LogWarning("Modules not inited. Autorun disabled");
        }

        private void Update()
        {
            this.gameMachine.Update();
        }

        private void FixedUpdate()
        {
            this.gameMachine.FixedUpdate();
        }

        private void LateUpdate()
        {
            this.gameMachine.LateUpdate();
        }

        public void AddListener(IGameListener listener)
        {
            this.gameMachine.AddListener(listener);
        }
        
        public void AddListeners(IEnumerable<IGameListener> listeners)
        {
            this.gameMachine.AddListeners(listeners);
        }
        
        public void RemoveListener(IGameListener listener)
        {
            this.gameMachine.RemoveListener(listener);
        }

        [Button]
        public void InitGame()
        {
            this.gameMachine.InitGame();
        }

        [Button]
        public void StartGame()
        {
            this.gameMachine.StartGame();
        }

        [Button]
        public void PauseGame()
        {
            this.gameMachine.PauseGame();
        }

        [Button]
        public void ResumeGame()
        {
            this.gameMachine.ResumeGame();
        }

        [Button]
        public void FinishGame()
        {
            this.gameMachine.FinishGame();
        }
    }
}