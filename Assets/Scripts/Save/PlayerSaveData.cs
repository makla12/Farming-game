
using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerSaveData
{
    public int money = 10;
    public List<SlotSaveData> slotsData = new() { new() };
    public long lastExitTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
