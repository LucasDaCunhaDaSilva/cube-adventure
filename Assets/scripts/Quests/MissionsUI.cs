using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionsUI : MonoBehaviour
{
    public MissionManager missionManager;
    public TMP_Text tmptext;
    
    private void Start()
    {
        string text = "* Missões";
        foreach(Mission mission in missionManager.activeMissions)
        {
            text += $"\n -{mission.missionText} +{mission.rewardPoints}P\n";
        }


        tmptext.text = text;
    }

}
