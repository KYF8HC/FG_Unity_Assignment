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


        //Instances
        private GameObject mainCameraInstance;
        private GameObject canvasInstance;
        private GameObject mainMenuInstance;
        private GameObject mainMenuBackgroundInstance;
        private GameObject playButtonInstance;
        private GameObject quitButtonInstance;
        private GameObject optionsButtonInstance;

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
            Destroy(canvasInstance);
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
        }

        private void InstantiateResources()
        {
            mainCameraInstance = Instantiate(mainCameraResource);
            if (!mainCameraInstance)
                Debug.LogError("MainCameraInstance not found");
            canvasInstance = Instantiate(canvasResource);
            if (!canvasInstance)
                Debug.LogError("CanvasInstance not found");
            mainMenuInstance = Instantiate(mainMenuResource, canvasInstance.transform);
            if (!mainMenuInstance)
                Debug.LogError("MainMenuInstance not found");
            mainMenuBackgroundInstance = Instantiate(mainMenuBackgroundResource);
            if (!mainMenuBackgroundInstance)
                Debug.LogError("MainMenuBackgroundInstance not found");
            playButtonInstance = Instantiate(playButtonResource, mainMenuInstance.transform);
            if (!playButtonInstance)
                Debug.LogError("PlayButtonInstance not found");
            quitButtonInstance = Instantiate(quitButtonResource, mainMenuInstance.transform);
            if (!quitButtonInstance)
                Debug.LogError("QuitButtonInstance not found");
            optionsButtonInstance = Instantiate(optionsButtonResource, mainMenuInstance.transform);
            if (!optionsButtonInstance)
                Debug.LogError("OptionsButtonInstance not found");
            var mainMenu = mainMenuInstance.GetComponent<MainMenu>();
            mainMenu.SetButtonReferences(playButtonInstance.GetComponent<Button>(),
                optionsButtonInstance.GetComponent<Button>(), quitButtonInstance.GetComponent<Button>());
            mainMenu.SetCanvasReference(canvasInstance.GetComponent<Canvas>());
        }

        public GameObject GetCanvas()
        {
            return canvasInstance;
        }
    }
}