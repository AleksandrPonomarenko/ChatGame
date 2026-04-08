using UnityEngine;

public class Mover : MonoBehaviour
{
    public int myId;
    [SerializeField] SentData data;

    private bool canMove = true;
    private float timer;

    void Update()
    {
        if (!canMove)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canMove = true;
            }
        }

        if (canMove)
        {
            string direction = null;

            if (Input.GetKeyUp(KeyCode.A)) direction = "Left";
            else if (Input.GetKeyUp(KeyCode.D)) direction = "Right";
            else if (Input.GetKeyUp(KeyCode.W)) direction = "Up";
            else if (Input.GetKeyUp(KeyCode.S)) direction = "Down";

            if (direction != null)
            {
                data.AddPair(myId, direction);
                canMove = false;
                timer = 1;
            }
        }
    }
    public void AddPlayer(int _id)
    {
        myId = _id;

        CameraManager _cam = FindObjectOfType<CameraManager>();
        _cam.objPlayer = FindObjectOfType<OnlinePlayers>().players[myId].transform;
        _cam.OnClickMyCam();

        this.enabled = true;
    }
}