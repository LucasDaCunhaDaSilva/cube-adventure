using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveManager : MonoBehaviour
{
    public static LiveManager Instance { private set; get; }

    public int maxLives = 5;
    public int deathCount = 0;

    public Image[] livesIcons;

    private void Awake()
    {
        Instance = this;
        CubeController.ImDead += PlayerDead;
    }

    private void OnDestroy()
    {
        CubeController.ImDead -= PlayerDead;
    }

    public void PlayerDead()
    {
        deathCount++;

        livesIcons[maxLives - deathCount].enabled = false;

        if (deathCount >= maxLives)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        else
        {
            CubeController.Instance.Resurrect();
        }
    }

}
