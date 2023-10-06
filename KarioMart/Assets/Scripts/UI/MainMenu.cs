using UnityEngine;
using UnityEngine.UI;

namespace KarioMart.UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject mapSelectInstance;
        private GameObject backButtonInstance;
        private Canvas mapSelectCanvasInstance;
        private Canvas mainMenuCanvasInstance;
        private Button backButton;
        private Button playButton;
        private Button quitButton;
        private Button optionsButton;

        private void Start()
        {
            backButton.onClick.AddListener(SetCanvasesActive);
            playButton.onClick.AddListener(SetCanvasesActive);
            quitButton.onClick.AddListener(Application.Quit);
        }

        private void OnDestroy()
        {
            Destroy(mapSelectInstance);
            Destroy(backButtonInstance);
        }

        private void SetCanvasesActive()
        {
            mapSelectCanvasInstance.enabled = !mapSelectCanvasInstance.enabled;
            mainMenuCanvasInstance.enabled = !mainMenuCanvasInstance.enabled;
        }
        
        public void SetCanvasReferences(Canvas mapSelectCanvasRef, Canvas mainMenuCanvasRef)
        {
            mapSelectCanvasInstance = mapSelectCanvasRef;
            mainMenuCanvasInstance = mainMenuCanvasRef;
        }

        public void SetButtonReferences(Button playButtonRef, Button optionsButtonRef, Button quitButtonRef, Button backButtonRef)
        {
            playButton = playButtonRef;
            optionsButton = optionsButtonRef;
            quitButton = quitButtonRef;
            backButton = backButtonRef;
        }
    }
}