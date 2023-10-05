using KarioMart.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KarioMart.Core
{
    public class InGameInitializer : MonoBehaviour
    {
        //Singleton
        public static InGameInitializer Instance { get; private set; }

        //Resources
        private GameObject canvasResource;
        private GameObject playerOneCarResource;
        private GameObject inGameHUDResource;
        private GameObject lapIncrementerResource;
        private GameObject mapGameObjectResource;
        private GameObject followCameraResource;

        //Instances
        private GameObject canvasInstance;
        private GameObject playerOneCarGameObjectInstance;
        private GameObject inGameHUDGameObjectInstance;
        private GameObject lapIncrementerGameObjectInstance;
        private GameObject mapGameObjectInstance;
        private GameObject followCameraInstance;
        private Timer timer;
        private InputHandler inputHandler;
        private InGameHUD inGameHUD;
        private PauseMenu pauseMenu;

        private string targetMapName;

        public bool gameIsPaused;

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
        }

        private void LoadResources()
        {
            canvasResource = Resources.Load<GameObject>("UI/Canvas");
            if (!canvasResource)
                Debug.LogError("CanvasResource not found");
            playerOneCarResource = Resources.Load<GameObject>("Vehicle/PurpleTruck");
            if (!playerOneCarResource)
                Debug.LogError("PlayerOneCarResource not found");
            inGameHUDResource = Resources.Load<GameObject>("UI/InGame/InGameHUD");
            if (!inGameHUDResource)
                Debug.LogError("InGameHUDResource not found");
            lapIncrementerResource = Resources.Load<GameObject>("MapObjects/LapIncrease");
            if (!lapIncrementerResource)
                Debug.LogError("LapIncrementerResource not found");
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
            pauseMenu.SetInputHandler(inputHandler);
            pauseMenu.Initialize();
            pauseMenu.GetRestartButton().onClick.AddListener(RestartGame);
            canvasInstance = Instantiate(canvasResource);
            if (!canvasInstance)
                Debug.LogError("CanvasInstance not found");
            playerOneCarGameObjectInstance = Instantiate(playerOneCarResource);
            if (!playerOneCarGameObjectInstance)
                Debug.LogError("PlayerOneCarInstance not found");
            inGameHUDGameObjectInstance = Instantiate(inGameHUDResource, canvasInstance.transform);
            if (!inGameHUDGameObjectInstance)
                Debug.LogError("InGameHUDInstance not found");
            inGameHUD = inGameHUDGameObjectInstance.GetComponent<InGameHUD>();
            inGameHUD.SetTimerInstance(timer);
            lapIncrementerGameObjectInstance = Instantiate(lapIncrementerResource);
            if (!lapIncrementerGameObjectInstance)
                Debug.LogError("LapIncrementerInstance not found");
            lapIncrementerGameObjectInstance.GetComponent<IncrementLapCount>().SetInGameHUD(inGameHUD);
            followCameraInstance = Instantiate(followCameraResource);
            if (!followCameraInstance)
                Debug.LogError("FollowCameraInstance not found");
            followCameraInstance.GetComponent<FollowCamera>().SetFollowTarget(playerOneCarGameObjectInstance.transform);
        }

        private void InputHandler_OnPause()
        {
            gameIsPaused = !gameIsPaused;
        }

        public void RestartGame()
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
            mapGameObjectInstance = Instantiate(mapGameObjectResource);
            if (!mapGameObjectInstance)
                Debug.LogError("MapGameObjectInstance not found");
        }

        public InputHandler GetInputHandler()
        {
            return inputHandler;
        }
    }
}