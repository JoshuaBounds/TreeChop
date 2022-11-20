using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
    Main player controller.
    */

    // Settings
    public float moveTime = 0.5f;

    // Controlled by PlayerCollider
    public bool isTileInFront = false;
    public bool isTileBehind = false;
    public Collider currentTileInFront;
    public Collider currentTileBehind;

    private bool isMoving = false;
    private float moveProgress;
    private Vector3 moveStartPos;
    private Vector3 moveEndPos;
    private Quaternion moveStartRot;
    private Quaternion moveEndRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called before setting the destination position/rotation for a movement
    void PrepareXForm()
    {
        isMoving = true;
        moveProgress = 0f;
        moveStartPos = moveEndPos = transform.position;
        moveStartRot = moveEndRot = transform.rotation;
    }

    // Translates the Player using the given position
    void XForm(Vector3 pos)
    {        
        PrepareXForm();
        moveEndPos = transform.position + transform.rotation * pos;
    }

    // Rotates the Player using the given rotation
    void XForm(Quaternion rot)
    {
        PrepareXForm();
        moveEndRot = transform.rotation * rot;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");    
        
        // Moves the player based on player input
        if (!isMoving)
        {
            if (horizontalInput > 0f)
                XForm(Quaternion.Euler(0, 90, 0));
            else if (horizontalInput < 0f)
                XForm(Quaternion.Euler(0, -90, 0));
            else if (verticalInput > 0f && currentTileInFront != null)
                XForm(Vector3.forward);
            else if (verticalInput < 0f && currentTileBehind != null)
                XForm(Vector3.back);
        }
        else
        {
            moveProgress += Time.deltaTime;

            if (moveProgress < moveTime) // Player in motion
            {
                transform.position = Vector3.Lerp(moveStartPos, moveEndPos, moveProgress * 1 / moveTime);
                transform.rotation = Quaternion.Slerp(moveStartRot, moveEndRot, moveProgress * 1 / moveTime);
            } 
            else // End of player movement
            {
                transform.position = moveEndPos;
                transform.rotation = moveEndRot;
                isMoving = false;

                if (currentTileInFront != null)
                    currentTileInFront.GetComponent<Animator>().SetBool("isAlive", true);
            }
        }
    }
}
