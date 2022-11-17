using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    /*
    Keeps track of objects entering and exiting the player triggers.
    Directly informs the given PlayerController about what's in the triggers.
    */

    public PlayerController player;
    public bool isBackTrigger;
    
    private List<Collider> tilesInFront = new List<Collider>();
    private List<Collider> tilesBehind = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called for each object the enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            if (!isBackTrigger)
            {
                tilesInFront.Add(other);
                player.currentTileInFront = other;
                player.isTileInFront = true;
            }
            else
            {
                tilesBehind.Add(other);
                player.currentTileBehind = other;
                player.isTileBehind = true;
            }
        }
    }

    // Called each time an object exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            if (!isBackTrigger)
            {
                tilesInFront.Remove(other);
                if (tilesInFront.Count < 1)
                {
                    player.currentTileInFront = null;
                    player.isTileInFront = false;
                }
            }
            else
            {
                tilesBehind.Remove(other);
                if (tilesBehind.Count < 1)
                {
                    player.currentTileBehind = null;
                    player.isTileBehind = false;
                }
            }
        }
    }
}
