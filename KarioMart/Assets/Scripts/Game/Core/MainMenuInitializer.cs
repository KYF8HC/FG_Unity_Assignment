using KarioMart.UI;
using UnityEngine;
using UnityEngine.UI;

namespace KarioMart.Core
{
    public class MainMenuInitializer : MonoBehaviour
    {
        //Singleton
        public static MainMenuInitializer Instance { get; private set; }

        //Resources
        private GameObject mainCameraResource;
        private GameObject canvasResource;
        private GameObject mainMenuResource;
        private GameObject mainMenuBackgroundResource;
        private GameObject playButtonResource;
        private GameObject quitButtonResource;
        private GameObject optionsButtonResource;
        private GameObject mapSelectResource;
        private GameObject backButtonResource;


        //Instances
        private GameObject mainCameraInstance;
        private Canvas mainMenuCanvasInstance;
        private GameObject mainMenuInstance;
        private GameObject mainMenuBackgroundInstance;
        private Canvas mapSelectCanvasInstance;
        private GameObject mapSelectInstance;
        private Button playButtonInstance;
        private Button quitButtonInstance;
        private Button optionsButtonInstance;
        private Button backButtonInstance;

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

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

            Destroy(mainCameraInstance);
            Destroy(mainMenuCanvasInstance);
            Destroy(mainMenuBackgroundInstance);
            Destroy(playButtonInstance);
            Destroy(quitButtonInstance);
            Destroy(optionsButtonInstance);
        }

        private void LoadResources()
        {
            mainCameraResource = Resources.Load<GameObject>("Core/MainCamera");
            if (!mainCameraResource)
                Debug.LogError("MainCameraResource not found");

            canvasResource = Resources.Load<GameObject>("UI/Canvas");
            if (!canvasResource)
                Debug.LogError("CanvasResource not found");

            mainMenuResource = Resources.Load<GameObject>("UI/MainMenu/MainMenu");
            if (!mainMenuResource)
                Debug.LogError("MainMenuResource not found");

            mainMenuBackgroundResource = Resources.Load<GameObject>("UI/MainMenu/MainMenuBackground");
            if (!mainMenuBackgroundResource)
                Debug.LogError("MainMenuBackgroundResource not found");

            playButtonResource = Resources.Load<GameObject>("UI/MainMenu/PlayButton");
            if (!playButtonResource)
                Debug.LogError("PlayButtonResource not found");

            quitButtonResource = Resources.Load<GameObject>("UI/MainMenu/QuitButton");
            if (!quitButtonResource)
                Debug.LogError("QuitButtonResource not found");

            optionsButtonResource = Resources.Load<GameObject>("UI/MainMenu/OptionsButton");
            if (!optionsButtonResource)
                Debug.LogError("OptionsButtonResource not found");

            mapSelectResource = Resources.Load<GameObject>("UI/MainMenu/MapSelect");
            if (!mapSelectResource)
                Debug.LogError("MainMenu: Failed to load map select resource.");

            backButtonResource = Resources.Load<GameObject>("UI/MainMenu/BackButton");
            if (!backButtonResource)
                Debug.LogError("MainMenu: Failed to load back button resource.");
        }

        private void InstantiateResources()
        {
            mainCameraInstance = Instantiate(mainCameraResource);
            if (!mainCameraInstance)
                Debug.LogError("MainMenuInitializer: MainCameraInstance not found");

            mainMenuCanvasInstance = Instantiate(canvasResource).GetComponent<Canvas>();
            if (!mainMenuCanvasInstance)
                Debug.LogError("MainMenuInitializer: CanvasInstance not found");
            mainMenuCanvasInstance.name = "Main Menu Canvas";

            mainMenuInstance = Instantiate(mainMenuResource, mainMenuCanvasInstance.transform);
            if (!mainMenuInstance)
                Debug.LogError("MainMenuInitializer: MainMenuInstance not found");

            mainMenuBackgroundInstance = Instantiate(mainMenuBackgroundResource);
            if (!mainMenuBackgroundInstance)
                Debug.LogError("MainMenuInitializer: MainMenuBackgroundInstance not found");
            mainMenuBackgroundInstance.name = "Main Menu Background";

            playButtonInstance = Instantiate(playButtonResource, mainMenuInstance.transform).GetComponent<Button>();
            if (!playButtonInstance)
                Debug.LogError("MainMenuInitializer: PlayButtonInstance not found");

            quitButtonInstance = Instantiate(quitButtonResource, mainMenuInstance.transform).GetComponent<Button>();
            if (!quitButtonInstance)
                Debug.LogError("MainMenuInitializer: QuitButtonInstance not found");

            optionsButtonInstance =
                Instantiate(optionsButtonResource, mainMenuInstance.transform).GetComponent<Button>();
            if (!optionsButtonInstance)
                Debug.LogError("MainMenuInitializer: OptionsButtonInstance not found");
            
            mapSelectCanvasInstance = Instantiate(canvasResource).GetComponent<Canvas>();
            if (!mapSelectCanvasInstance)
                Debug.LogError("MainMenuInitializer:  Failed to instantiate canvas instance.");
            mapSelectCanvasInstance.name = "Map Select Canvas";
            mapSelectCanvasInstance.enabled = false;

            backButtonInstance = Instantiate(backButtonResource, mapSelectCanvasInstance.transform)
                .GetComponent<Button>();
            if (!backButtonInstance)
                Debug.LogError("MainMenuInitializer:  Failed to instantiate back button instance.");

            var mainMenu = mainMenuInstance.GetComponent<MainMenu>();
            mainMenu.SetButtonReferences(playButtonInstance, optionsButtonInstance, quitButtonInstance,
                backButtonInstance);

            mapSelectInstance = Instantiate(mapSelectResource, mapSelectCanvasInstance.transform);
            if (!mapSelectInstance)
                Debug.LogError("MainMenuInitializer:  Failed to instantiate map select instance.");

            mainMenu.SetCanvasReferences(mapSelectCanvasInstance, mainMenuCanvasInstance);
        }

        public void SetMainMenuActive(bool active)
        {
            mainMenuCanvasInstance.enabled = active;
            mainMenuBackgroundInstance.SetActive(active);
            mainCameraInstance.SetActive(active);
            if (!active)
            {
                mapSelectCanvasInstance.enabled = active;
            }
        }
    }
}