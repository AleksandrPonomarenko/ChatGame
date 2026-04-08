using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public CellInfo currentCell;
    [SerializeField] int id;
    [SerializeField] string username;

    [SerializeField] TMP_Text text_Username;

    public void LoadInfo(CellInfo _cell, int _id, string _name)
    {
        currentCell = _cell;
        id = _id;
        username = _name;
        text_Username.text = _name;
    }
}
