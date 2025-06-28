using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderField : Selectable, ISubmitHandler
{
    [SerializeField] UnityEngine.UI.Slider slider;

    public void OnSubmit(BaseEventData eventData) =>
        EventSystem.current.SetSelectedGameObject(slider.gameObject);
}
