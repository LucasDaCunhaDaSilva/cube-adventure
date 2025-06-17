using UnityEngine;

public class MissionBase: MonoBehaviour
{
    [SerializeField] public string missionText;
    [SerializeField] public int rewardPoints;

    public virtual bool check() { return false; }
}
