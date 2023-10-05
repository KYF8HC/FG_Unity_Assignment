using UnityEngine;

public class Timer
{
    private float elapsedTime;
    
    public void IncreaseTimer()
    {
        elapsedTime += Time.deltaTime;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
