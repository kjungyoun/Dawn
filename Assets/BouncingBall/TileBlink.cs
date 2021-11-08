using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlink : Tile
{
    private List<TileBlink> blinkTiles;

    public void SetBlinkTiles(List<TileBlink> blinks)
    {
        blinkTiles = new List<TileBlink>();

        for(int i=0; i < blinks.Count; ++i)
        {
            if (blinks[i] != this)
            {
                blinkTiles.Add(blinks[i]);
            }
        }
    }

    public override void Collision(CollisionDirection direction)
    {
        if(direction == CollisionDirection.Down)
        {
            int index = Random.Range(0, blinkTiles.Count);
            movement2D.transform.position = blinkTiles[index].transform.position + Vector3.up;

            movement2D.JumpTo();
        }
    }
}
