using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelector : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedGO;    
    
    void Start() => Initialize();
    void OnEnable() => Initialize();

    void Initialize()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Invoke(nameof(Select), 0);
    }

    void Select()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedGO);
    }
}
