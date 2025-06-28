using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{        
    private const string POINTS_KEY = "Points";

    public static PointsManager Instance { private set; get; }
    
    [SerializeField] AudioClip collectPointSound;
    [SerializeField] TMP_Text pointsText;


    public int Points { private set; get; }

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
        Points = PlayerPrefs.GetInt(POINTS_KEY, 0);
        pointsText.text = Points.ToString();
    }

    public void Add(int amount)
    {
        Points += amount;
        SFXManager.Instance.PlaySound(collectPointSound);
        pointsText.text = Points.ToString();
    }
    public void Remove(int amount)
    {
        Points -= amount;
        SFXManager.Instance.PlaySound(collectPointSound);
        pointsText.text = Points.ToString();
    }
    public static void Clear()
    {
        PlayerPrefs.DeleteKey(POINTS_KEY);
    }
    public void Save()
    {
        PlayerPrefs.SetInt(POINTS_KEY, Points);
        PlayerPrefs.Save();
    }
}
