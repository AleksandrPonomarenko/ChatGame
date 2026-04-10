using UnityEngine;

public class SimulatorOnlinePlayers : MonoBehaviour
{
    public bool isSimulatorOn;
    public bool isActivitesOn;
    public bool isActivitesLocal;

    public bool isSpawnYou;

    public int amountBots;

    [SerializeField] OnlinePlayers players;
    [SerializeField] Cells cells;
    [SerializeField] SentData data;
    public void StartSimulator()
    {
        if (isSimulatorOn)
        {
            if (amountBots < cells.totalCells)
            {
                for (int i = 0; i < amountBots; i++)
                {
                    CellInfo _findUnbusyCell = null;

                    while (_findUnbusyCell == null)
                    {
                        CellInfo _infoCell = GameObject.Find("Cell (" + Random.Range(0, cells.xSizeArea) + ", " + Random.Range(0, cells.ySizeArea) + ")").GetComponent<CellInfo>();

                        if (!_infoCell.isBusyPlayer && _infoCell.isCanBusyCell)
                        {
                            _findUnbusyCell = _infoCell;
                        }
                    }

                    players.SpawnNewPlayer("Bot " + i, _findUnbusyCell);
                }

                if (isSpawnYou)
                {
                    int rPlayer = Random.Range(0, players.players.Count);
                    PlayerInfo player = players.players[rPlayer].GetComponent<PlayerInfo>();
                    player.gameObject.name = "You";
                    player.ChangeName("You");
                    player.isBot = false;
                    FindObjectOfType<Mover>().AddPlayer(rPlayer, player);
                }

                if (isActivitesOn) { RandomActivites(); }
            }
            else
            {
                Debug.Log("Количество ботов превышает количество клеток");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void RandomActivites()
    {
        data.Clear();

        for (int i = 0; i < players.players.Count; i++)
        {
            if (players.players[i].GetComponent<PlayerInfo>().isBot == false) continue;

            int rMoves = Random.Range(0, 4);

            int requestX = (int)players.players[i].transform.position.x;
            int requestY = (int)players.players[i].transform.position.y;

            if (rMoves == 0) // left
            {
                requestX--;
            }
            else if (rMoves == 1) // right
            {
                requestX++;
            }
            else if (rMoves == 2) // up
            {
                requestY++;
            }
            else if (rMoves == 3) // down
            {
                requestY--;
            }

            if (!isActivitesLocal)
            {
                data.AddRequest(i, requestX, requestY);
            }
            else
            {
                GameObject objRequestCell = GameObject.Find("Cell (" + requestX + ", " + requestY + ")");

                if (objRequestCell != null)
                {
                    CellInfo requestCell = objRequestCell.GetComponent<CellInfo>();

                    if (!requestCell.isBusyPlayer && requestCell.isCanBusyCell)
                    {
                        PlayerInfo infoRequestPlayer = players.players[i].GetComponent<PlayerInfo>();

                        if (infoRequestPlayer.currentCell != null)
                        {
                            infoRequestPlayer.currentCell.RemovePlayer(infoRequestPlayer);
                        }

                        requestCell.AddPlayer(infoRequestPlayer);

                        players.players[i].transform.position = new Vector3(requestX, requestY, 0);
                        infoRequestPlayer.currentCell = requestCell;
                    }
                }
            }
        }

        Invoke("RandomActivites", 1);
    }
}
