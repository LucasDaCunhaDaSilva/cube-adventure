using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [Tooltip("Lista de missões ativas")]
    public List<Mission> activeMissions = new List<Mission>();


    // Adiciona uma nova missão
    public void AddMission(Mission mission)
    {
        if (!activeMissions.Contains(mission))
        {
            activeMissions.Add(mission);
            Debug.Log($"Missão adicionada: {mission.missionText}");
        }
    }
    // Zera o progresso (ex: nova fase)
    public void ResetMissions()
    {
        activeMissions.Clear();
    }

}
