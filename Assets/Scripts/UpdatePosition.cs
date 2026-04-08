using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    [SerializeField] OnlinePlayers online;
    [SerializeField] LoadData load;
    void Start()
    {
        Invoke("NewUpdate", 1);
    }

    void NewUpdate()
    {
        for (int i = 0; i < load.data.Count; i++)
        {
            if (load.data[i] != null)
            {
                PlayerInfo _infoPlayer = online.players[load.data[i].id].GetComponent<PlayerInfo>();
                _infoPlayer.currentCell.RemovePlayer(_infoPlayer);

                CellInfo _infoCell = GameObject.Find(load.data[i].move).GetComponent<CellInfo>();
                _infoCell.AddPlayer(_infoPlayer);
                _infoCell.MovePlayer(_infoPlayer);
            }
        }

        load.Clear();
        Invoke("NewUpdate", 1);
    }
}
