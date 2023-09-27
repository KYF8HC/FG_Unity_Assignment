using System;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private Timer timerInstance;
    private float timer;

    private void Awake()
    {
        timerInstance = Manager.Instance.GetTimer();
    }

    private void Update()
    {
        timer = timerInstance.GetElapsedTime();
        timerText.text = $"{(int)(timer / 60):00}:{(int)(timer % 60):00}.{(int)((timer * 1000) % 1000):000}";
    }
}
