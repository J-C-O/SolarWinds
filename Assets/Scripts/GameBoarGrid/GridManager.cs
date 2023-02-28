using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GameObject _tileContainer;
    [SerializeField] private Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        SetTileRing();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        //_tileContainer.SetActive(true);
        for(int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = string.Format("Tile_{0}_{1}", i, j);

                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);

                spawnedTile.Init(isOffset);

                _tiles[new Vector2(i, j)] = spawnedTile;
                //spawnedTile.transform.SetParent(_tileContainer.transform);
            }
        }
        _camera.transform.position = new Vector3((float)_width / 2 -0.5f, (float)_height / 2 -0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
    public void SetTileRing()
    {
        SetTileColumnColor(0);
        SetTileColumnColor(ConfigurationManager.CMInstance.GameBoard_Width - 1);

        SetTileRowColor(0);
        SetTileRowColor(ConfigurationManager.CMInstance.GameBoard_Height - 1);
    }
    public void SetTileRowColor(float rowIndex)
    {
        Vector2 pos = new Vector2(0, rowIndex);
        for (int x = 0; x < ConfigurationManager.CMInstance.GameBoard_Height; x++)
        {
            pos.x = x;
            if (GetTileAtPosition(pos) != null)
            {
                var isOffset = (x % 2 == 0 && rowIndex % 2 != 0) || (x % 2 != 0 && rowIndex % 2 == 0);
                if (isOffset)
                {
                    GetTileAtPosition(pos).SetColor(ConfigurationManager.CMInstance.DeactiveTileColorOffset);
                }
                else
                {
                    GetTileAtPosition(pos).SetColor(ConfigurationManager.CMInstance.DeactiveTileColorBase);
                }

            }
        }
    }
    public void SetTileColumnColor(float columnIndex)
    {
        Vector2 pos = new Vector2(columnIndex, 0);
        for (int y = 0; y < ConfigurationManager.CMInstance.GameBoard_Height; y++)
        {
            pos.y = y;
            if(GetTileAtPosition(pos) != null)
            {
                var isOffset = (columnIndex % 2 == 0 && y % 2 != 0) || (columnIndex % 2 != 0 && y % 2 == 0);
                if (isOffset)
                {
                    GetTileAtPosition(pos).SetColor(ConfigurationManager.CMInstance.DeactiveTileColorOffset);
                }
                else
                {
                    GetTileAtPosition(pos).SetColor(ConfigurationManager.CMInstance.DeactiveTileColorBase);     
                }
                
            }
        }
    }
}
