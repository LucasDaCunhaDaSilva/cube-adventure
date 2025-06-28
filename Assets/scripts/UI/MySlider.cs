using UnityEngine;
using UnityEngine.EventSystems;

public class MySlider : UnityEngine.UI.Slider, ISubmitHandler
{
    public void OnSubmit(BaseEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(transform.parent.gameObject);
    }
}
