using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Coin = 10 }

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemEffectPrefab;

    public void Exit()
    {
        Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
