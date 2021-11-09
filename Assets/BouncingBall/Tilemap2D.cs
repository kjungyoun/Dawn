using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap2D : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private StageUI stageUI;

    [Header("Tile")]
    [SerializeField]
    //private GameObject tilePrefab;
    private GameObject[] tilePrefabs;
    [SerializeField]
    private Movement2D movement2D;

    [Header("Item")]
    [SerializeField]
    private GameObject itemPrefab;

    private int maxCoinCount = 0;
    private int currentCoinCount = 0;


    private List<TileBlink> blinkTiles;

    public void GenerateTilemap(MapData mapData)
    {
        blinkTiles = new List<TileBlink>();

        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int index = y * width + x;

                if (mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                Vector3 position = new Vector3(-(width * 0.2f - 0.2f) + (x * 0.4f), (height * 0.2f - 0.2f) - (y * 0.4f));

                if (mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    SpawnTile((TileType)mapData.mapData[index], position);
                }

                else if (mapData.mapData[index] == (int)ItemType.Coin)
                {
                    SpawnItem(position);
                }
            }
        }

        currentCoinCount = maxCoinCount;

        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        foreach (TileBlink tile in blinkTiles)
        {
            tile.SetBlinkTiles(blinkTiles);
        }
    }

    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefabs[(int)tileType-1], position, Quaternion.identity);

        clone.name = "Tile";
        clone.transform.SetParent(transform);

        Tile tile = clone.GetComponent<Tile>();
        tile.Setup(movement2D);

        if(tileType == TileType.Blink)
        {
            blinkTiles.Add(clone.GetComponent<TileBlink>());
        }
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item";
        clone.transform.SetParent(transform);

        maxCoinCount++;
    }

    public void GetCoin(GameObject coin)
    {
        currentCoinCount--;

        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        coin.GetComponent<Item>().Exit();

        if(currentCoinCount == 0)
        {
            stageController.GameClear();
        }
    }
}
