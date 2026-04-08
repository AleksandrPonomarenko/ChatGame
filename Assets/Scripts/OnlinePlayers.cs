using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayers : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();

    [SerializeField] GameObject prefabPlayer;

    public void SpawnNewPlayer(string _playerName, CellInfo _infoCell)
    {
        GameObject _newPlayer = Instantiate(prefabPlayer, _infoCell.transform.position, Quaternion.identity);
        PlayerInfo _infoPlayer = _newPlayer.GetComponent<PlayerInfo>();

        _newPlayer.name = _playerName;

        int newId = players.Count;

        _infoPlayer.LoadInfo(_infoCell, newId, _playerName);
        _infoCell.AddPlayer(_infoPlayer);

        players.Add(_newPlayer);
    }

    public void DeletePlayer(string playerName)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null && players[i].name == playerName)
            {
                Destroy(players[i]);
                players.RemoveAt(i);
                break;
            }
        }
    }
}