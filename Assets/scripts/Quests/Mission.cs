
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] public string missionText;
    [SerializeField] public int rewardPoints;

    public virtual bool Check() => false;
}
