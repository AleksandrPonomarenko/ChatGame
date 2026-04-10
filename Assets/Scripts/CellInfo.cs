using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public bool isBusyPlayer;
    public bool isBusyObject;
    public bool isCanBusyCell;
    public string typeNameCell;
    [SerializeField] SpriteRenderer spriteGround, spriteObject;
    public Color colorGround;
    [SerializeField] PlayerInfo currentPlayer;

    public void ChangeTypeCell(string typeName)
    {
        typeNameCell = typeName;

        if (typeName == "Water")
        {
            isCanBusyCell = false;
            colorGround = new Color(75f / 255f, 75f / 255f, 200f / 255f);
            spriteGround.color = colorGround;
        }
        else if (typeName == "Sand")
        {
            colorGround = new Color(200f / 255f, 170f / 255f, 120f / 255f);
            spriteGround.color = colorGround;
        }
    }
    public void AddPlayer(PlayerInfo player)
    {
        if (isBusyPlayer && currentPlayer != null)
        {
            RemovePlayer(currentPlayer);
        }

        isBusyPlayer = true;
        currentPlayer = player;

        spriteGround.color = colorGround + new Color(0.2f, 0.2f, 0.2f, 0f);
    }
    public void RemovePlayer(PlayerInfo player)
    {
        isBusyPlayer = false;
        currentPlayer = null;

        spriteGround.color = colorGround;
    }
    public void MovePlayer(PlayerInfo player)
    {
        player.transform.position = this.transform.position;
    }
    public void AddObject(string _name, Cells cells)
    {
        isBusyObject = true;
        isCanBusyCell = false;

        spriteGround.color = colorGround + new Color(0.2f, 0.2f, 0.2f, 0f);

        if (!string.IsNullOrEmpty(_name) || _name == "Random")
        {
            spriteGround.sprite = cells.imaagesObject[0];
            spriteGround.color = Color.white;
        }
    }
}
