using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float destoryTime;

    private void Awake()
    {
        Destroy(gameObject, destoryTime);
    }
}
