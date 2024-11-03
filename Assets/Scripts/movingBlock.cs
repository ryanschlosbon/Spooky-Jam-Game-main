using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    GameObject self;
    Camera theCamera;
    public Rigidbody2D theRB;
    public int horizRate;
    public float vertRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        theRB.position = new Vector2(theRB.position.x, theRB.position.y + vertRate);
    }
}
