using UnityEngine;
using UnityEngine.UI;

namespace KarioMart.UI
{


    public class MainMenu : MonoBehaviour
    {
        //Resources
        private GameObject mapSelectResource;

        private GameObject backButtonResource;

        //Instances
        private GameObject mapSelectInstance;
        private GameObject backButtonInstance;

        private Canvas canvas;
        private Button backButton;
        private Button playButton;
        private Button quitButton;
        private Button optionsButton;

        private void Awake()
        {
            LoadResources();

        }

        private void Start()
        {
            backButtonInstance = Instantiate(backButtonResource, canvas.transform);
            backButton = backButtonInstance.GetComponent<Button>();
            backButtonInstance.SetActive(false);
            backButton.onClick.AddListener(OnBackButtonClicked);
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(Application.Quit);
        }

        private void OnDestroy()
        {
            Destroy(mapSelectInstance);
            Destroy(backButtonInstance);
        }

        private void LoadResources()
        {
            mapSelectResource = Resources.Load<GameObject>("UI/MainMenu/MapSelect");
            if (!mapSelectResource)
            {
                Debug.LogError("MainMenu: Failed to load map select resource.");
            }

            backButtonResource = Resources.Load<GameObject>("UI/MainMenu/BackButton");
            if (!backButtonResource)
            {
                Debug.LogError("MainMenu: Failed to load back button resource.");
            }
        }

        private void OnPlayButtonClicked()
        {
            if (!mapSelectInstance)
            {
                mapSelectInstance = Instantiate(mapSelectResource, canvas.transform);
            }

            mapSelectInstance.SetActive(true);
            backButtonInstance.SetActive(true);
            playButton.gameObject.SetActive(false);
            optionsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
        }


        //TODO: Disable canvas not objects!!!!
        private void OnBackButtonClicked()
        {
            mapSelectInstance.SetActive(false);
            backButtonInstance.SetActive(false);
            playButton.gameObject.SetActive(true);
            optionsButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }

        public void SetCanvasReference(Canvas canvasRef)
        {
            canvas = canvasRef;
        }

        public void SetButtonReferences(Button playButtonRef, Button optionsButtonRef, Button quitButtonRef)
        {
            playButton = playButtonRef;
            optionsButton = optionsButtonRef;
            quitButton = quitButtonRef;
        }
    }
}