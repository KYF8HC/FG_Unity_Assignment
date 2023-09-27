using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<Manager>();
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("Manager");
                instance = singletonObject.AddComponent<Manager>();
            }
            return instance;
        }
    }

    private Timer timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        timer = new Timer();
    }

    private void Update()
    {
        timer.IncreaseTimer();
    }

    public Timer GetTimer()
    {
        return timer;
    }
}