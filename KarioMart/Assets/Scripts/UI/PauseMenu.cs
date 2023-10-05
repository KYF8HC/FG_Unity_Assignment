using KarioMart.Core;
using UnityEngine;
using UnityEngine.InputSystem;
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
        private GameObject canvasInstance;
        private GameObject backgroundInstance;
        private Button mainMenuButtonInstance;
        private Button restartButtonInstance;
        private Button optionsButtonInstance;
        private Button exitButtonInstance;

        private Canvas canvasComponent;
        private InputHandler inputHandler;

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
            canvasInstance = GameObject.Instantiate(canvasResource);
            if (!canvasInstance)
                Debug.LogError("PauseMenu: Failed to instantiate canvas instance.");
            canvasComponent = canvasInstance.GetComponent<Canvas>();
            canvasComponent.enabled = false;
            backgroundInstance = GameObject.Instantiate(backgroundResource, canvasInstance.transform);
            if (!backgroundInstance)
                Debug.LogError("PauseMenu: Failed to instantiate background instance.");
            mainMenuButtonInstance = GameObject.Instantiate(mainMenuButtonResource, canvasInstance.transform)
                .GetComponent<Button>();
            if (!mainMenuButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate main menu button instance.");
            restartButtonInstance = GameObject.Instantiate(restartButtonResource, canvasInstance.transform)
                .GetComponent<Button>();
            restartButtonInstance.onClick.AddListener(InputHandler_OnPause);
            if (!restartButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate restart button instance.");
            optionsButtonInstance = GameObject.Instantiate(optionsButtonResource, canvasInstance.transform)
                .GetComponent<Button>();
            if (!optionsButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate options button instance.");
            exitButtonInstance = GameObject.Instantiate(exitButtonResource, canvasInstance.transform)
                .GetComponent<Button>();
            if (!exitButtonInstance)
                Debug.LogError("PauseMenu: Failed to instantiate exit button instance.");
            exitButtonInstance.onClick.AddListener(Application.Quit);
        }

        private void InputHandler_OnPause()
        {
            canvasComponent.enabled = !canvasComponent.enabled;
        }

        public void Initialize()
        {
            LoadResources();
            InstantiateResources();
            inputHandler.OnPause += InputHandler_OnPause;
        }

        public Button GetRestartButton()
        {
            return restartButtonInstance;
        }

        public void SetInputHandler(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }
    }
}