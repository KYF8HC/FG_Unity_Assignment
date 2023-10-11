using TMPro;
using UnityEngine;

namespace KarioMart.UI
{
    public class InGameHUD : MonoBehaviour
    {
        //INCLUDE UNITY VERSION IN README!!!!


        //Resources
        private GameObject timerTextResource;
        private GameObject lapCountTextResource;

        //Instances
        private GameObject timerTextInstance;
        private GameObject lapCountTextInstance;

        private TextMeshProUGUI lapCountText;
        private TextMeshProUGUI timerText;
        private Timer timerInstance;
        private float time;

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
            timerTextResource = Resources.Load<GameObject>("UI/InGame/TimerText");
            if (!timerTextResource)
                Debug.LogError("TimerText not found");
            lapCountTextResource = Resources.Load<GameObject>("UI/InGame/LapCountText");
            if (!lapCountTextResource)
                Debug.LogError("LapCountText not found");
        }

        private void InstantiateResources()
        {
            timerTextInstance = Instantiate(timerTextResource, transform.parent);
            if (!timerTextInstance)
                Debug.LogError("TimerTextInstance not found");
            lapCountTextInstance = Instantiate(lapCountTextResource, transform.parent);
            if (!lapCountTextInstance)
                Debug.LogError("LapCountTextInstance not found");
            timerText = timerTextInstance.GetComponent<TextMeshProUGUI>();
            if (!timerText)
                Debug.LogError("TimerText not found");
            lapCountText = lapCountTextInstance.GetComponent<TextMeshProUGUI>();
            if (!lapCountText)
                Debug.LogError("LapCountText not found");
        }
        
        public void SetTimerInstance(Timer timer)
        {
            timerInstance = timer;
            if (timerInstance == null)
                Debug.LogError("TimerInstance not found");
        }
        public void UpdateLapCount(int lapCount ,int maxLapCount)
        {
            lapCountText.text = $"{lapCount} / {maxLapCount} Laps";
        }
    }
}