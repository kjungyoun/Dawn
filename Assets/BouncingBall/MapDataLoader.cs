using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

[Serializable]
public class MapDataLoader
{
    public MapData Load(string fileName)
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (fileName.Contains(".json") == false)
            {
                fileName += ".json";
            }

            fileName = Path.Combine(Application.streamingAssetsPath, fileName);

            WWW www = new WWW(fileName);

            string dataAsJson = www.text;

            MapData mapData = new MapData();
            mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

            return mapData;

        }
        else
        {
            if (fileName.Contains(".json") == false)
            {
                fileName += ".json";
            }

            fileName = Path.Combine(Application.streamingAssetsPath, fileName);

            string dataAsJson = File.ReadAllText(fileName);

            MapData mapData = new MapData();
            mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

            return mapData;
        }
    }
}
