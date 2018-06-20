using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform player;
    public GameObject ammoPrefab;
    public float speed = .1f;
    public float twitchModifier = 1;
    private float cd = 0;
    public float maxCD = 2;
    public bool useTwitchVotes;

    public Vector2 minPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(useTwitchVotes ? 200 : 0, 0, 0));
        }
    }

    public Vector2 maxPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
    }

    private void Update()
    {
        cd -= Time.deltaTime;
        if(cd <= 0)
        {
            cd = Random.Range(0, maxCD);
            Vector3 position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), -1);

            if((position.x != minPos.x || position.x != maxPos.x) && (position.y != minPos.y && position.y != maxPos.y))
            {
                int id = Random.Range(0, 4);
                switch (id)
                {
                    case 0:
                        position.x = minPos.x;
                        break;
                    case 1:
                        position.x = maxPos.x;
                        break;
                    case 2:
                        position.y = minPos.y;
                        break;
                    case 3:
                        position.y = maxPos.y;
                        break;
                }
            }

            GameObject ammo = Instantiate(ammoPrefab, position, Quaternion.identity);
            ammo.transform.LookAt(player);

            ammo.GetComponent<Rigidbody>().velocity = ammo.transform.forward * speed * twitchModifier * Vector3.Distance(ammo.transform.position, player.position);
        }
    }
}
