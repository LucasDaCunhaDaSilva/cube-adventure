using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string LAST_MAP = "LAST_SCENE";
    [SerializeField] private GameObject defaultSelection;

    private void FixedUpdate()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelection);
        }
    }

    public void OnPlay() {
        SceneManager.LoadScene(PlayerPrefs.GetInt(LAST_MAP, 1), LoadSceneMode.Single);
    }

    public void OnNewGame() {
        PlayerPrefs.DeleteKey(LAST_MAP);
        PointsManager.Clear();
        SceneManager.LoadScene(PlayerPrefs.GetInt(LAST_MAP, 1), LoadSceneMode.Single);
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
