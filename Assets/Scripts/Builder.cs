using UnityEngine;

public class Builder : MonoBehaviour
{
    public Transform player;
    public GameObject wallObject;
    public float speed = 3;
    public float dashSpeed = 25;
    public float offset = 0.3f;
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
        if (Time.timeScale == 0)
            return;

        Vector3 movement = new Vector3(Input.GetAxis("Vertical") * Time.deltaTime * speed * -1, 0, Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1);

        if (transform.position.x > maxPos.x - offset && movement.z < 0)
            movement.z = 0;
        if (transform.position.x < minPos.x + offset && movement.z > 0)
            movement.z = 0;

        if (transform.position.y > maxPos.y - offset && movement.x < 0)
            movement.x = 0;
        if (transform.position.y < minPos.y + offset && movement.x > 0)
            movement.x = 0;

        transform.Translate(movement);

        if (Input.GetButtonDown("Dash"))
        {
            Vector3 posDash = transform.position + new Vector3(movement.z * -1, movement.x * -1, 0) * dashSpeed;
            if(PositionIsInBoundary(posDash))
                transform.position = posDash;
        }
        if (Input.GetButtonDown("Wall"))
        {
            GameObject wall = Instantiate(wallObject, transform.position, Quaternion.identity);
            wall.transform.LookAt(player);
        }
    }

    private bool PositionIsInBoundary(Vector3 pos)
    {
        if (pos.x > maxPos.x - offset)
            return false;
        if (pos.x < minPos.x + offset)
            return false;

        if (pos.y > maxPos.y - offset)
            return false;
        if (pos.y < minPos.y + offset)
            return false;

        return true;
    }
}
