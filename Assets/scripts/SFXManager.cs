using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { private set; get; }

    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
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
    
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

}
