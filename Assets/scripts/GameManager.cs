using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text pointsText;

    [SerializeField] AudioSource audioSource;
    [SerializeField] MissionManager missionManager;

    private int totalPoints;

    private void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
        CubeController.ImWin += Win;
    }
    private void OnDestroy()
    {
        CubeController.ImWin -= Win;
    }

    private void Win()
    {
        panel.SetActive(true);
        foreach (Mission mission in missionManager.activeMissions)
        {
            if (mission.Check())
            {
                totalPoints += mission.rewardPoints;
            }
        }

        audioSource.Play();
        pointsText.text = $"+{totalPoints} pontos!";

        PlayerPrefs.SetInt("LAST_SCENE", SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void NextMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}