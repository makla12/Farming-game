using UnityEngine;

[System.Serializable]
public class SlotSaveData
{
    public Vector2Int position = new (0, 0);
    public SlotState slotState = SlotState.Empty;
    public int plantedId = -1;
    public double secondsLeft = 0;
}
