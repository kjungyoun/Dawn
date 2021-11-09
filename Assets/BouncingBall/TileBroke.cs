using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBroke : Tile
{
    [SerializeField]
    private GameObject tileBrokeEffect;

    public override void Collision(CollisionDirection direction)
    {
        Instantiate(tileBrokeEffect, transform.position, Quaternion.identity);

        if(direction == CollisionDirection.Down)
        {
            movement2D.JumpTo();
        }

        Destroy(gameObject);
    }
}
