using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, LastIndex }

public enum CollisionDirection { Up = 0, Down }

public abstract class Tile : MonoBehaviour
{
    //[SerializeField]
    //private Sprite[] images;
    //private SpriteRenderer spriteRenderer;
    //private TileType tileType;

    //public void Setup(TileType tileType)
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    TileType = tileType;
    //}

    //public TileType TileType
    //{
    //    set
    //    {
    //        tileType = value;
    //        spriteRenderer.sprite = images[(int)tileType - 1];
    //    }
    //    get => tileType;
    //}

    protected Movement2D movement2D;

    public virtual void Setup(Movement2D movement2D)
    {
        this.movement2D = movement2D;
    }

    public abstract void Collision(CollisionDirection direction);
}
