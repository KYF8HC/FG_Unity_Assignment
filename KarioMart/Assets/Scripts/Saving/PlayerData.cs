using System;

public class PlayerData : IJsonSavable
{
    private string playerName;
    private float time;

    public PlayerData(string name, float time)
    {
        playerName = name;
        this.time = time;
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Load()
    {
        throw new NotImplementedException();
    }

    public void Register()
    {
        throw new NotImplementedException();
    }
}
