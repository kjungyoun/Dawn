using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShuffleList
{
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);
        List<E> randomList = new List<E>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (originalList.Count > 0)
        {
            randomIndex = r.Next(0, originalList.Count); // list에서 랜덤으로 고르기
            randomList.Add(originalList[randomIndex]); // add it to the new, random list
            originalList.RemoveAt(randomIndex); // 겹치는 거 지우기
        }

        return randomList; // 새로운 랜덤 리스트 return
    }
}
