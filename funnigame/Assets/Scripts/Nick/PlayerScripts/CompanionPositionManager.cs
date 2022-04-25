using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionPositionManager : MonoBehaviour
{
    [SerializeField] Transform companionTarget;
    [SerializeField] Transform companionFollowPoint;
    [SerializeField] Vector2 defaultPosition;
    [SerializeField] private string triggerTag;

    PlayerMovement playerMovement;

    private bool isFollowingPlayer = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        UpdateFollowPointSide();
    }

    void UpdateFollowPointSide()
    {
        if (isFollowingPlayer)
        {
            //sets target position to player follow position
            companionTarget.position = companionFollowPoint.position;
            //switches the position to the left or right of the player depending on where the player is facing
            if (playerMovement.isFacingLeft)
            {
                companionFollowPoint.transform.localPosition = new Vector2(defaultPosition.x, defaultPosition.y);
            }
            else if (!playerMovement.isFacingLeft)
            {
                companionFollowPoint.transform.localPosition = new Vector2(-defaultPosition.x, defaultPosition.y);
            }
        }
    }


    public void SetPointLocation(Transform point)
    {
        //sets target position to given point
        isFollowingPlayer = false;
        companionTarget.transform.position = point.position;
        Debug.Log("Updated Position");
    }

    public void MoveToPlayer()
    {
        //sets target position back to following player
        isFollowingPlayer = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
/*            int pointIndex;
            if (int.TryParse(collision.gameObject.name, out pointIndex))
            {
                followPointHandler.MoveToPOI(pointIndex);
            }*/
            Debug.Log("POI Trigger Entered");
            SetPointLocation(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            MoveToPlayer();
        }
    }


}
