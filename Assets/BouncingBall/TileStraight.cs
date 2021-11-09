using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStraight : Tile
{
    [SerializeField]
    private MoveType moveType;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void Collision(CollisionDirection direction)
    {
        Vector3 position = boxCollider2D.bounds.center + Vector3.right * (int)moveType;

        movement2D.SetupStraightMove(moveType, position);
    }
}
