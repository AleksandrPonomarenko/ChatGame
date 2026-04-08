using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public bool isBusyPlayer;
    [SerializeField] PlayerInfo currentPlayer;

    public void AddPlayer(PlayerInfo player)
    {
        if (isBusyPlayer && currentPlayer != null)
        {
            RemovePlayer(currentPlayer);
        }

        isBusyPlayer = true;
        currentPlayer = player;
    }
    public void MovePlayer(PlayerInfo player)
    {
        player.transform.position = this.transform.position;
    }
    public void RemovePlayer(PlayerInfo player)
    {
        isBusyPlayer = false;
        currentPlayer = null;
    }
}
