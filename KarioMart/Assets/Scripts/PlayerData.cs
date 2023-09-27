using System;

public class PlayerData
{
    private string playerName;
    private int lapCount = 0;

    public void SetPlayerName(string name)
    {
        if (String.IsNullOrEmpty(playerName))
        {
            playerName = name;
        }
    }
    public void IncrementLapCount()
    {
        lapCount++;
    }
}
