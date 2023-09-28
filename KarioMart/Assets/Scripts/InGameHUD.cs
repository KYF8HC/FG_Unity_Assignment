using System;
using TMPro;
using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    //Resources
    private GameObject timerTextResource;

    //Instances
    private GameObject timerTextInstance;

    [SerializeField] TextMeshProUGUI lapCountText;
    TextMeshProUGUI timerText;
    private Timer timerInstance;
    private float time;
    private int lapCount = 0;

    private void Awake()
    {
        LoadResources();
        InstantiateResources();
    }

    private void Update()
    {
        time = timerInstance.GetElapsedTime();
        timerText.text = $"{(int)(time / 60):00}:{(int)(time % 60):00}.{(int)((time * 1000) % 1000):000}";
    }

    private void LoadResources()
    {
        timerTextResource = Resources.Load<GameObject>("UI/Timertext");
        if (!timerTextResource)
            Debug.LogError("TimerText not found");
        timerInstance = Manager.Instance.GetTimer();
        if (timerInstance == null)
            Debug.LogError("TimerInstance not found");
    }

    private void InstantiateResources()
    {
        timerTextInstance = Instantiate(timerTextResource, transform.parent);
        if (!timerTextInstance)
            Debug.LogError("TimerTextInstance not found");
        timerText = timerTextInstance.GetComponent<TextMeshProUGUI>();
    }

    public void IncrementLapCount()
    {
        lapCount++;
        lapCountText.text = $"{lapCount}";
    }
}