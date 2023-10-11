using UnityEngine;
using UnityEngine.UI;

namespace KarioMart.UI
{
    public class PauseMenu
    {
        //Resources
        private GameObject canvasResource;
        private GameObject backgroundResource;
        private GameObject mainMenuButtonResource;
        private GameObject restartButtonResource;
        private GameObject optionsButtonResource;
        private GameObject exitButtonResource;

        //Instances
        private Canvas pauseMenuCanvasInstance;
        private GameObject backgroundInstance;
        private Button mainMenuButtonInstance;
        private Button restartButtonInstance;
        private Button optionsButtonInstance;
        private Button exitButtonInstance;


        private void LoadResources()
        {
            canvasResource = Resources.Load<GameObject>("UI/Canvas");
            if (!canvasResource)
                Debug.LogError("PauseMenu: Failed to load canvas resource.");

            backgroundResource = Resources.Load<GameObject>("UI/PauseMenu/Background");
            if (!backgroundResource)
                Debug.LogError("PauseMenu: Failed to load background resource.");

            mainMenuButtonResource = Resources.Load<GameObject>("UI/PauseMenu/MainMenuButton");
            if (!mainMenuButtonResource)
                Debug.LogError("PauseMenu: Failed to load main menu button resource.");

            restartButtonResource = Resources.Load<GameObject>("UI/PauseMenu/RestartButton");
            if (!restartButtonResource)
                Debug.LogError("PauseMenu: Failed to load restart button resource.");

            optionsButtonResource = Resources.Load<GameObject>("UI/PauseMenu/OptionsButton");
            if (!optionsButtonResource)
                Debug.LogError("PauseMenu: Failed to load options button resource.");

            exitButtonResource = Resources.Load<GameObject>("UI/PauseMenu/ExitButton");
            if (!exitButtonResource)
                Debug.LogError("PauseMenu: Failed to load exit button resource.");
        }

        private void InstantiateResources()
        {
            pauseMenuCanvasInstance = Instantiate(canvasResource).GetComponent<Canvas>();
            if (!pauseMenuCanvasInstance)
                Debug.LogError("PauseMenu: Failed to instantiate canvas instance.");
            pauseMenuCanvasInstance.name = "Pause Menu Canvas";
            pauseMenuCanvasInstance.enabled = false;

            backgroundInstance = Instantiate(backgroundResource, pauseMenuCanvasInstance.transform);
            if (!backgroundInstance)
                Debug.LogError("PauseMenu: Failed to instantiate background instance.");

            mainMenuButtonInstance = Instantiate(mainMenuButtonResource, pauseMenuCanvasInstance.transform)
                .GetComponent<Button>();
            if (!mainMenuButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate main menu button instance.");

            restartButtonInstance = Instantiate(restartButtonResource, pauseMenuCanvasInstance.transform)
                .GetComponent<Button>();
            if (!restartButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate restart button instance.");
            restartButtonInstance.onClick.AddListener(InputHandler_OnPause);

            optionsButtonInstance = Instantiate(optionsButtonResource, pauseMenuCanvasInstance.transform)
                .GetComponent<Button>();
            if (!optionsButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate options button instance.");

            exitButtonInstance = Instantiate(exitButtonResource, pauseMenuCanvasInstance.transform)
                .GetComponent<Button>();
            if (!exitButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate exit button instance.");
            exitButtonInstance.onClick.AddListener(Application.Quit);
        }
        

        public void InputHandler_OnPause()
        {
            pauseMenuCanvasInstance.enabled = !pauseMenuCanvasInstance.enabled;
        }

        public void Initialize()
        {
            LoadResources();
            InstantiateResources();
        }

        public void DestroyPauseMenu()
        {
            if (pauseMenuCanvasInstance)
                Destroy(pauseMenuCanvasInstance.gameObject);
            if (backgroundInstance)
                Destroy(backgroundInstance);
            if (mainMenuButtonInstance)
                Destroy(mainMenuButtonInstance);
            if (restartButtonInstance)
                Destroy(restartButtonInstance);
            if (optionsButtonInstance)
                Destroy(optionsButtonInstance);
            if (exitButtonInstance)
                Destroy(exitButtonInstance);
        }

        public Button GetRestartButton()
        {
            return restartButtonInstance;
        }

        public Button GetMainMenuButton()
        {
            return mainMenuButtonInstance;
        }

        private static GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }

        private static GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return GameObject.Instantiate(prefab, parent);
        }

        private static void Destroy(Object gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}