using System;
using UnityEngine;

namespace KarioMart.Core
{
    public class GameStateManager : MonoBehaviour
    {
        public enum GameState
        {
            MainMenu,
            InGame,
            IsPaused,
            GameOver
        }

        //Singleton
        public static GameStateManager Instance { get; private set; }

        //Resources
        private GameObject mainMenuInitializerResource;
        private GameObject inGameInitializerResource;
        private GameObject gameOverInitializerResource;

        //Instances
        private GameObject mainMenuInitializerInstance;
        private GameObject inGameInitializerInstance;
        private GameObject gameOverInitializerInstance;

        private GameState currentGameState;

        private string targetMapName;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(this);
            }

            LoadResources();
        }

        private void Start()
        {
            currentGameState = GameState.MainMenu;
        }

        private void Update()
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    if (!mainMenuInitializerInstance)
                    {
                        mainMenuInitializerInstance = Instantiate(mainMenuInitializerResource);
                    }

                    break;
                case GameState.InGame:
                    if (mainMenuInitializerInstance)
                    {
                        Destroy(mainMenuInitializerInstance);
                    }

                    if (!inGameInitializerInstance)
                    {
                        inGameInitializerInstance = Instantiate(inGameInitializerResource);
                        var initializer = inGameInitializerInstance.GetComponent<InGameInitializer>();
                        if (!initializer)
                            Debug.LogError("InGameInitializer not found");
                        else
                        {
                            initializer.LoadTargetMapName(targetMapName);
                        }
                    }

                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LoadResources()
        {
            mainMenuInitializerResource = Resources.Load<GameObject>("Core/MainMenuInitializer");
            if (!mainMenuInitializerResource)
                Debug.LogError("MainMenuInitializerResource not found");
            inGameInitializerResource = Resources.Load<GameObject>("Core/InGameInitializer");
            if (!inGameInitializerResource)
                Debug.LogError("InGameInitializerResource not found");
            /*gameOverInitializerResource = Resources.Load<GameObject>("Core/GameOverInitializer");
            if (!gameOverInitializerResource)
                Debug.LogError("GameOverInitializerResource not found");*/
        }

        public void SetTargetMapName(string mapName)
        {
            targetMapName = mapName;
        }

        public void SetGameState(GameState state)
        {
            currentGameState = state;
        }
    }
}