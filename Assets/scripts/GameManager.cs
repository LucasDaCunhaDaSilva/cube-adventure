using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ private set; get; }

    public event Action<bool> PauseEvent;
    public bool IsPaused { set; get; } = false;

    [SerializeField] GameObject panel;

    [SerializeField] AudioClip winGameSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        CubeController.ImWin += Win;
    }
    private void OnDestroy()
    {
        CubeController.ImWin -= Win;
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        PauseEvent?.Invoke(IsPaused);
    }

    private void Win()
    {
        panel.SetActive(true);
        
        SFXManager.Instance.PlaySound(winGameSound);
        PlayerPrefs.SetInt("LAST_SCENE", SceneManager.GetActiveScene().buildIndex + 1);
        PointsManager.Instance.Save();
    }
    public void NextMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}