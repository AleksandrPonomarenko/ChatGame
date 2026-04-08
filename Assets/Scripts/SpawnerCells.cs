using UnityEngine;

public class SpawnerCells : MonoBehaviour
{
    [SerializeField] GameObject prefabDefaultCell;
    [SerializeField] SimulatorOnlinePlayers simulator;
    void Start()
    {
        int amount;

        int x = 0;
        int y = 0;

        for (amount = 0; amount < 100; amount++)
        {
            GameObject _objCell = Instantiate (prefabDefaultCell, new Vector3(x, y, 0), Quaternion.identity);

            _objCell.name = "Cell (" + x + ", " + y + ")";

            if ((amount+1) % 10 == 0)
            {
                y++;
                x = 0;
            }
            else
            {
                x++;
            }
        }

        if (simulator != null) { simulator.StartSimulator(); }

        Destroy(gameObject);
    }
}
