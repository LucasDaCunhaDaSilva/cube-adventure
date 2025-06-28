using UnityEngine;

public class PointCollector : MonoBehaviour
{
    private const string POINT_TAG = "Point";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(POINT_TAG))
        {
            Destroy(other.gameObject);
            PointsManager.Instance.Add(1);
        }
    }
}
