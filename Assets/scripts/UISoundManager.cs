using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager Instance { private set; get; }

    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip selectedSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip sliderSound;

    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }     
    }

    public void OnPointerEnter(PointerEventData data)
    {
        PlaySount(hoverSound);
    }
    public void OnPointerClick(PointerEventData data)
    {
        PlaySount(clickSound);
    }
    public void OnSelect(BaseEventData data)
    {
        PlaySount(selectedSound);
    }
    public void OnSliderChange()
    {
        PlaySount(sliderSound);
    }

    private void PlaySount(AudioClip sound)
    {
        if(audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }


}
