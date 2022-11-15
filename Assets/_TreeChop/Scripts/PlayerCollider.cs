using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerController player;
    public bool isBack;
    
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            if (!isBack)
            {
                tilesInFront.Add(other);
                player.isTileInFront = true;
            }
            else
            {
                tilesBehind.Add(other);
                player.isTileBehind = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            if (!isBack)
            {
                tilesInFront.Remove(other);
                if (tilesInFront.Count < 1)
                    player.isTileInFront = false;
            }
            else
            {
                tilesBehind.Remove(other);
                if (tilesBehind.Count < 1)
                    player.isTileBehind = false;
            }
        }
    }
}