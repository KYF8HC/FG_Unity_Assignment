using KarioMart.UI;
using KarioMart.Vehicle;
using UnityEngine;

namespace KarioMart.Core
{
    public class InGameInitializer : MonoBehaviour
    {
        //Singleton
        public static InGameInitializer Instance { get; private set; }

        //Resources
        private GameObject canvasResource;
        private GameObject gameRootResource;
        private GameObject playerOneCarResource;
        private GameObject inGameHUDResource;
        private GameObject mapGameObjectResource;
        private GameObject followCameraResource;

        //Instances
        private GameObject canvasInstance;
        private GameObject gameRootInstance;
        private GameObject playerOneCarGameObjectInstance;
        private GameObject inGameHUDGameObjectInstance;
        private GameObject mapGameObjectInstance;
        private GameObject followCameraInstance;
        private CheckPointTracker checkPointTrackerGameObjectInstance;
        private Timer timer;
        private InputHandler inputHandler;
        private InGameHUD inGameHUD;
        private PauseMenu pauseMenu;

        private string targetMapName;
        private bool gameIsPaused;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }

            LoadResources();
            InstantiateResources();
        }

        private void Update()
        {
            if (gameIsPaused)
                return;
            timer.IncreaseTimer();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
            inputHandler.OnPause -= InputHandler_OnPause;
            inputHandler.OnPause -= pauseMenu.InputHandler_OnPause;
            Destroy(canvasInstance);
            Destroy(gameRootInstance);
            pauseMenu.Destroy();
        }

        private void LoadResources()
        {
            canvasResource = Resources.Load<GameObject>("UI/Canvas");
            if (!canvasResource)
                Debug.LogError("CanvasResource not found");
            
            gameRootResource = Resources.Load<GameObject>("Core/GameRoot");
            
            playerOneCarResource = Resources.Load<GameObject>("Vehicle/PurpleTruck");
            if (!playerOneCarResource)
                Debug.LogError("PlayerOneCarResource not found");
            
            inGameHUDResource = Resources.Load<GameObject>("UI/InGame/InGameHUD");
            if (!inGameHUDResource)
                Debug.LogError("InGameHUDResource not found");
            
            followCameraResource = Resources.Load<GameObject>("Core/FollowCamera");
            if (!followCameraResource)
                Debug.LogError("FollowCameraResource not found");
        }

        private void InstantiateResources()
        {
            inputHandler = new InputHandler();
            if (inputHandler == null)
                Debug.LogError("InputHandler not found");
            inputHandler.Initialize();
            inputHandler.OnPause += InputHandler_OnPause;
            
            timer = new Timer();
            if (timer == null)
                Debug.LogError("Timer not found");
            
            pauseMenu = new PauseMenu();
            if (pauseMenu == null)
                Debug.LogError("PauseMenu not found");
            inputHandler.OnPause += pauseMenu.InputHandler_OnPause;
            pauseMenu.Initialize();
            pauseMenu.GetRestartButton().onClick.AddListener(RestartGame);
            pauseMenu.GetMainMenuButton().onClick.AddListener(GameStateManager.Instance.ReturnToMainMenu);
            
            canvasInstance = Instantiate(canvasResource);
            if (!canvasInstance)
                Debug.LogError("CanvasInstance not found");
            
            gameRootInstance = Instantiate(gameRootResource);
            if (!gameRootInstance)
                Debug.LogError("GameRootInstance not found");
            
            playerOneCarGameObjectInstance = Instantiate(playerOneCarResource, gameRootInstance.transform);
            if (!playerOneCarGameObjectInstance)
                Debug.LogError("PlayerOneCarInstance not found");
            
            inGameHUDGameObjectInstance = Instantiate(inGameHUDResource, canvasInstance.transform);
            if (!inGameHUDGameObjectInstance)
                Debug.LogError("InGameHUDInstance not found");
            inGameHUD = inGameHUDGameObjectInstance.GetComponent<InGameHUD>();
            inGameHUD.SetTimerInstance(timer);
            
            followCameraInstance = Instantiate(followCameraResource, gameRootInstance.transform);
            if (!followCameraInstance)
                Debug.LogError("FollowCameraInstance not found");
            followCameraInstance.GetComponent<FollowCamera>().SetFollowTarget(playerOneCarGameObjectInstance.transform);
        }

        private void InputHandler_OnPause()
        {
            gameIsPaused = !gameIsPaused;
        }

        private void RestartGame()
        {
            Destroy(playerOneCarGameObjectInstance);
            playerOneCarGameObjectInstance = Instantiate(playerOneCarResource);
            followCameraInstance.GetComponent<FollowCamera>().SetFollowTarget(playerOneCarGameObjectInstance.transform);
            timer.ResetTimer();
            inGameHUD.ResetLapCount();
            gameIsPaused = !gameIsPaused;
        }

        public void LoadTargetMapName(string mapName)
        {
            targetMapName = mapName;
            mapGameObjectResource = Resources.Load<GameObject>($"Maps/{targetMapName}");
            if (!mapGameObjectResource)
                Debug.LogError("MapGameObjectResource not found");
            mapGameObjectInstance = Instantiate(mapGameObjectResource, gameRootInstance.transform);
            if (!mapGameObjectInstance)
                Debug.LogError("MapGameObjectInstance not found");
            checkPointTrackerGameObjectInstance = FindObjectOfType<CheckPointTracker>();
            checkPointTrackerGameObjectInstance.SetInGameHUD(inGameHUD);
            playerOneCarGameObjectInstance.GetComponent<CarController>().SetCheckPointTracker(checkPointTrackerGameObjectInstance);
        }

        public InputHandler GetInputHandler()
        {
            return inputHandler;
        }
    }
}