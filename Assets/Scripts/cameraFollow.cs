using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;           
    public float smoothTime;

    private Vector2 velocity = Vector2.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector2 targetPosition = (Vector2)player.position + offset;

            transform.position = Vector2.SmoothDamp((Vector2)transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
