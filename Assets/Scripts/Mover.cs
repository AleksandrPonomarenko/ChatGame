using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Mover : MonoBehaviour
{
    public int myId;
    [SerializeField] PlayerInfo infoPlayer;
    [SerializeField] SentData data;

    private bool canMove = true;
    private float timer;
    private float moveCooldown = 1f;

    private string pendingDirection = null;

    void Update()
    {
        HandleInput();

        if (!canMove)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canMove = true;

                if (pendingDirection != null)
                {
                    ExecuteMove(pendingDirection);
                    pendingDirection = null;
                }
            }
        }
    }

    void HandleInput()
    {
        string direction = null;

        if (Input.GetKeyUp(KeyCode.A)) direction = "Left";
        else if (Input.GetKeyUp(KeyCode.D)) direction = "Right";
        else if (Input.GetKeyUp(KeyCode.W)) direction = "Up";
        else if (Input.GetKeyUp(KeyCode.S)) direction = "Down";

        if (direction != null)
        {
            if (canMove)
            {
                ExecuteMove(direction);
            }
            else
            {
                pendingDirection = direction;
            }
        }
    }

    void ExecuteMove(string direction)
    {
        int requestX = (int)infoPlayer.transform.position.x;
        int requestY = (int)infoPlayer.transform.position.y;

        switch (direction)
        {
            case "Left": requestX--; break;
            case "Right": requestX++; break;
            case "Up": requestY++; break;
            case "Down": requestY--; break;
        }

        canMove = false;
        timer = moveCooldown;
        
        // ЗАПРОС 

        // локальное движение
        GameObject objRequestCell = GameObject.Find($"Cell ({requestX}, {requestY})");

        if (objRequestCell != null)
        {
            CellInfo requestCell = objRequestCell.GetComponent<CellInfo>();

            if (!requestCell.isBusyPlayer && requestCell.isCanBusyCell)
            {
                infoPlayer.currentCell.RemovePlayer(infoPlayer);
                requestCell.AddPlayer(infoPlayer);
                infoPlayer.transform.position = new Vector3(requestX, requestY, 0);
                infoPlayer.currentCell = requestCell;;
            }
        }
    }

public void AddPlayer(int _id, PlayerInfo _player)
    {
        infoPlayer = _player;
        myId = _id;

        CameraManager _cam = FindObjectOfType<CameraManager>();
        _cam.objPlayer = FindObjectOfType<OnlinePlayers>().players[myId].transform;
        _cam.OnClickMyCam();

        this.enabled = true;
    }
}