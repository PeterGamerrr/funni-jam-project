using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointHandler : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;

    [SerializeField] Vector2 followPointPosition;

    void Start()
    {
        player = transform.parent.gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdatePointSide();   
    }

    void UpdatePointSide()
    {
        if (playerMovement.isFacingLeft)
        {
            transform.localPosition = new Vector2(followPointPosition.x, followPointPosition.y);
        }
        else if (!playerMovement.isFacingLeft)
        {
            transform.localPosition = new Vector2(-followPointPosition.x, followPointPosition.y);
        }
    }


}
