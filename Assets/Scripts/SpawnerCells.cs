using UnityEngine;

public class SpawnerCells : MonoBehaviour
{
    public bool isGenerateArea;
    public bool isGenerateRandom;
    public int xArea = 0, yArea = 0;
    [SerializeField] GameObject prefabDefaultCell;
    [SerializeField] SimulatorOnlinePlayers simulator;
    [SerializeField] Cells cells;
    void Start()
    {
        if (isGenerateArea)
        {
            if (isGenerateRandom)
            {
                GenerateArea(Random.Range(3, xArea), Random.Range(3, yArea));
            }
            else if (xArea == 0 || yArea == 0)
            {
                GenerateArea(10, 10);
            }
            else
            {
                GenerateArea(xArea, yArea);
            }

            GameObject.Find("Camera Manager").transform.position = new Vector3(xArea / 2, yArea / 2, 0);
        }

        if (FindObjectOfType<CellInfo>() != null)
        {
            simulator.StartSimulator();
        }

        Destroy(gameObject);
    }

    public void GenerateArea(int _xArea, int _yArea)
    {
        cells.totalCells = _xArea * _yArea;
        cells.xSizeArea = _xArea;
        cells.ySizeArea = _yArea;

        xArea = _xArea;
        yArea = _yArea;

        for (int y = 0; y < yArea; y++)
        {
            for (int x = 0; x < xArea; x++)
            {
                GameObject objCell = Instantiate(prefabDefaultCell, new Vector3(x, y, 0), Quaternion.identity);
                objCell.name = $"Cell ({x}, {y})";

                CellInfo newCell = objCell.GetComponent<CellInfo>();

                if (x == 0 || y == 0 || x == xArea - 1 || y == yArea - 1)
                {
                    newCell.ChangeTypeCell("Sand");
                }
            }
        }

        CreateRandomObjects();
        CreateWaterBorder();
    }

    void CreateRandomObjects()
    {
        for (int i = 0; i < cells.totalCells/90; i++)
        {
            CellInfo _findUnbusyCell = null;

            while (_findUnbusyCell == null)
            {
                CellInfo _infoCell = GameObject.Find("Cell (" + Random.Range(0, cells.xSizeArea) + ", " + Random.Range(0, cells.ySizeArea) + ")").GetComponent<CellInfo>();

                if (!_infoCell.isBusyObject && _infoCell.isCanBusyCell)
                {
                    _findUnbusyCell = _infoCell;
                }
            }

            _findUnbusyCell.AddObject("Random", cells);
        }
    }
    void CreateWaterBorder()
    {
        for (int y = 0; y < yArea; y++)
        {
            CreateWaterCell(-1, y);
            CreateWaterCell(xArea, y);
        }

        for (int x = -1; x <= xArea; x++)
        {
            CreateWaterCell(x, -1);
            CreateWaterCell(x, yArea);
        }
    }

    void CreateWaterCell(int x, int y)
    {
        GameObject existingCell = GameObject.Find($"Cell ({x}, {y})");
        if (existingCell != null) return;

        GameObject objCell = Instantiate(prefabDefaultCell, new Vector3(x, y, 0), Quaternion.identity);
        objCell.name = $"Cell ({x}, {y})";

        CellInfo newCell = objCell.GetComponent<CellInfo>();
        newCell.ChangeTypeCell("Water");
    }
}
