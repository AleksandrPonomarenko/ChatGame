using UnityEngine;

public class SimulatorOnlinePlayers : MonoBehaviour
{
    public bool isSimulatorOn;

    public bool isSpawnYou;

    public int amountBots;

    [SerializeField] OnlinePlayers players;
    [SerializeField] SentData data;
    public void StartSimulator()
    {
        if (isSimulatorOn)
        {
            //for (int i = 0; i < amountBots; i++)
            for (int i = 0; i < Random.Range(1, 10); i++)
            {
                CellInfo _findUnbusyCell = null;

                while (_findUnbusyCell == null)
                {
                    CellInfo _infoCell = GameObject.Find("Cell (" + Random.Range(0, 10) + ", " + Random.Range(0, 10) + ")").GetComponent<CellInfo>();

                    if (!_infoCell.isBusyPlayer)
                    {
                        _findUnbusyCell = _infoCell;
                    }
                }

                players.SpawnNewPlayer("Bot " + i, _findUnbusyCell);
            }

            if (isSpawnYou)
            {
                FindObjectOfType<Mover>().AddPlayer(Random.Range(0, players.players.Count));
            }

            RandomActivites();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void RandomActivites()
    {
        data.Clear();

        string[] moves = { "left", "right", "up", "down", "stay" };

        for (int i = 0; i < players.players.Count; i++)
        {
            data.AddPair(i, moves[Random.Range(0, moves.Length)]);
        }
        //sentData.AddPair(Random.Range(0, players.players.Count), moves[Random.Range(0, moves.Length)]);

        Invoke("RandomActivites", 1);
    }
}
