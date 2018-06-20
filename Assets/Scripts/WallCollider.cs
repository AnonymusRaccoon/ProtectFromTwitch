using System.Collections;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public int hp = 2;
    public int destroyTime = 5;

    private void Start()
    {
        destroyTime -= GameObject.FindGameObjectsWithTag("Wall").Length;
        destroyTime = Mathf.Clamp(destroyTime, 1, 5);
        StartCoroutine("Unspawn");
    }

    private IEnumerator Unspawn()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        hp--;
        Restarter.Score++;
        if(hp == 0)
            Destroy(gameObject);
    }
}