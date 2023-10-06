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
            InstantiateMainMenu();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

            Destroy(mainMenuInitializerInstance);
            Destroy(inGameInitializerInstance);
            Destroy(gameOverInitializerInstance);
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

        private void InstantiateMainMenu()
        {
            mainMenuInitializerInstance = Instantiate(mainMenuInitializerResource);
            if (!mainMenuInitializerInstance)
                Debug.LogError("GameStateManager: MainMenuInitializerInstance not found");
        }

        public void InstantiateTargetMap(string mapName)
        {
            mainMenuInitializerInstance.GetComponent<MainMenuInitializer>().SetMainMenuActive(false);
            inGameInitializerInstance = Instantiate(inGameInitializerResource);
            var initializer = InGameInitializer.Instance;
            if (!initializer)
                Debug.LogError("InGameInitializer not found");
            else
                initializer.LoadTargetMapName(mapName);
        }

        public void ReturnToMainMenu()
        {
            mainMenuInitializerInstance.GetComponent<MainMenuInitializer>().SetMainMenuActive(true);
            Destroy(InGameInitializer.Instance.gameObject);
        }
        
        public void SetGameState(GameState state)
        {
            currentGameState = state;
        }
    }
}