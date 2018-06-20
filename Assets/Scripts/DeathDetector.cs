using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    public GameObject DeathText;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Time.timeScale = 0;
        DeathText.SetActive(true);
    }
}
