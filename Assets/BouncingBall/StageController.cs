using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static int maxStageCount;

    [SerializeField]
    private Tilemap2D tilemap2D;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private StageUI stageUI;

    private void Awake()
    {
        MapDataLoader mapDataLoader = new MapDataLoader();

        int index = PlayerPrefs.GetInt("StageIndex")+1;

        string currentStage = index < 10 ? $"Stage0{index}" : $"Stage{index}";

        MapData mapData = mapDataLoader.Load(currentStage);

        tilemap2D.GenerateTilemap(mapData);

        playerController.Setup(mapData.playerPosition, mapData.mapSize.y);

        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);

        stageUI.UpdateTextStage(currentStage);
    }

    public void GameClear()
    {
        //Debug.Log("Game Clear");
        int index = PlayerPrefs.GetInt("StageIndex");

        if(index < maxStageCount - 1)
        {
            index++;
            PlayerPrefs.SetInt("StageIndex", index);
            SceneLoader.LoadScene();
        }
        else
        {
            SceneLoader.LoadScene("AfterDishSuccessScene");
        }
    }
}
