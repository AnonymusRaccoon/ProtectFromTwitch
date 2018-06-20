using UnityEngine;
using TMPro;

public class Restarter : MonoBehaviour
{
    public static int Score = 0;
    public GameObject score;
    private float startTime;

    private void OnEnable()
    {
        score.GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString();
        startTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (Input.anyKey && startTime + .5f < Time.realtimeSinceStartup)
        {
            foreach (GameObject ammo in GameObject.FindGameObjectsWithTag("Ammo"))
                Destroy(ammo);

            foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
                Destroy(wall);

            Score = 0;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
