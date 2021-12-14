using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject _playerZone;

    [SerializeField] private GameObject _obstacleZone;

    [Header("Colors")]
    [SerializeField] private Color _player;

    [SerializeField] private Color _obstacle;


    // -- OTHER --
    private Transform[,] _boxes;

    private PlayGrid _playGrid;

    private GridView _gridView;

    private int _gridSize;

    public void Init(GridView gridView, PlayGrid playGrid, int gridSize)
    {
        _gridView = gridView;

        _playGrid = playGrid;

        _gridSize = gridSize + (gridSize - 1);

        _gridView.RefreshGridView += ColorRenderer;

        SpawnGrid();
    }

    private void SpawnGrid()
    {
        _boxes = new Transform[_gridSize, _gridSize];

        for (int y = 0; y < _gridSize; y++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                GameObject spawn = x % 2 == 0 && y % 2 == 0 ? _playerZone : _obstacleZone;

                Vector3 spawnPoint = new Vector3(x, 0, y);

                GameObject box = Instantiate(spawn, spawnPoint, Quaternion.identity);

                _boxes[y, x] = box.transform;
            }
        }
    }

    private void ColorRenderer(int[,] grid)
    {
        for (int y = 0; y < _gridSize; y++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                if (grid[y, x] == -2)
                {
                    _boxes[y, x].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = _obstacle;
                }
                else if (grid[y, x] == 1)
                {
                    _boxes[y, x].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = _player;
                }
                else if (grid[y, x] == 0)
                {
                    _boxes[y, x].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }
                else if (grid[y, x] == -4)
                {
                    _boxes[y, x].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Color.gray;
                }
            }
        }
    }
}
