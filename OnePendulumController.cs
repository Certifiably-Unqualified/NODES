using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePendulumController : MonoBehaviour
{
	Rigidbody2D stick;
	HingeJoint2D hinge;

	public float torqueBst;
	float direction;


    // Start is called before the first frame update
    void Start()
    {
        stick = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    // Rotation is in Z axis
    void Update()
    {
    	// Boost in angular direction
    	direction = stick.angularVelocity / Mathf.Abs(stick.angularVelocity);

    	// Control
        if (Input.GetKeyDown("1"))
    	{
    		hinge.anchor = new Vector2(0.5f, 0f);
        	hinge.connectedAnchor = new Vector2(0.5f, 0f);
    	}
    	if (Input.GetKeyDown("2"))
    	{
    		hinge.anchor = new Vector2(-0.5f, 0f);
        	hinge.connectedAnchor = new Vector2(-0.5f, 0f);
    	}
    	if (Input.GetKeyDown("space"))
    	{
    		stick.AddTorque(torqueBst * direction, ForceMode2D.Force);
    	}
    }    
}
