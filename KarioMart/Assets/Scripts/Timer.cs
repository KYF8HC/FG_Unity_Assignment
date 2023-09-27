using UnityEngine;

public class Timer
{
    private float elapsedTime = 0.0f;
    
    public void IncreaseTimer()
    {
        elapsedTime += Time.deltaTime;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
