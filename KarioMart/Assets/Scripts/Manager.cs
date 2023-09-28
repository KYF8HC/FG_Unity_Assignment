using UnityEngine;

public class Manager : MonoBehaviour
{
    //Singleton
    public static Manager Instance { get; private set; }
    
    //Resources
    private GameObject canvasResource;
    private GameObject playerOneCarResource;
    private GameObject inGameHUDResource;
    
    //Insatnces
    private GameObject canvasInstance;
    private GameObject playerOneCarInstance;
    private GameObject inGameHUDInstance;
    
    private Timer timer;
    
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
        timer.IncreaseTimer();
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void LoadResources()
    {
        canvasResource = Resources.Load<GameObject>("UI/Canvas");
        if(!canvasResource)
            Debug.LogError("CanvasResource not found");
        playerOneCarResource = Resources.Load<GameObject>("Vehicle/PurpleTruck");
        if(!playerOneCarResource)
            Debug.LogError("PlayerOneCarResource not found");
        inGameHUDResource = Resources.Load<GameObject>("UI/InGameHUD");
        if(!inGameHUDResource)
            Debug.LogError("InGameHUDResource not found");
    }

    private void InstantiateResources()
    {
        timer = new Timer();
        if(timer == null)
            Debug.LogError("Timer not found");
        canvasInstance = Instantiate(canvasResource);
        if(!canvasInstance)
            Debug.LogError("CanvasInstance not found");
        playerOneCarInstance = Instantiate(playerOneCarResource);
        if(!playerOneCarInstance)
            Debug.LogError("PlayerOneCarInstance not found");
        inGameHUDInstance = Instantiate(inGameHUDResource, canvasInstance.transform);
        if(!inGameHUDInstance)
            Debug.LogError("InGameHUDInstance not found");
    }
    public Timer GetTimer()
    {
        return timer;
    }
}