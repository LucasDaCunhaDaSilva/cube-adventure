using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject firstSelection;


    private void Start()
    {
        GameManager.Instance.PauseEvent += Pause;
    }
    private void OnDestroy()
    {
        GameManager.Instance.PauseEvent -= Pause;
    }


    private void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelection);
        }
        else
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
        }
    }

    public void Resume() => GameManager.Instance.Pause();
    public void OnGoToMainMenu()
    {
        if(GameManager.Instance.IsPaused) GameManager.Instance.Pause();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}