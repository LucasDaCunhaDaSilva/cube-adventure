using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFXListenner : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler
{
    public void OnPointerEnter(PointerEventData data) => UISoundManager.Instance?.OnPointerEnter(data);
    public void OnPointerClick(PointerEventData data) => UISoundManager.Instance?.OnPointerClick(data);
    public void OnSelect(BaseEventData data) => UISoundManager.Instance?.OnSelect(data);
}
