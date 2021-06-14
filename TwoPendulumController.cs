using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPendulumController : MonoBehaviour
{
	public GameObject stick1, stick2;
    HingeJoint2D hinge1, hinge2;
    Rigidbody2D localstick, stick1rb, stick2rb;

	public float torqueBst;
    float direction, direction1, direction2;

    [SerializeField]
    List<HingeJoint2D> stick1Hinges;

    // Start is called before the first frame update
    void Start()
    {
        stick1Hinges = stick1.GetComponents<HingeJoint2D>().ToList();
        hinge1 = stick1Hinges[0];
        hinge2 = stick2.GetComponent<HingeJoint2D>();
        stick1rb = stick1.GetComponent<Rigidbody2D>();
        stick2rb = stick2.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // Rotation is in Z axis
    void Update()
    {
    	// Boost in angular direction
    	direction1 = (stick1rb.angularVelocity / Mathf.Abs(stick1rb.angularVelocity));
        direction2 = (stick2rb.angularVelocity / Mathf.Abs(stick2rb.angularVelocity));

    	// Control
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hinge1.enabled = true;
            hinge2.enabled = false;
            direction = direction2;
            localstick = stick2rb;
    	}
    	if (Input.GetKeyDown(KeyCode.Alpha2))
    	{
            hinge1.enabled = false;
            hinge2.enabled = true;
            direction = direction1;
            localstick = stick1rb;
    	}
    	if (Input.GetKeyDown(KeyCode.Space)) // Yeet
    	{
    		localstick.AddTorque(torqueBst * direction, ForceMode2D.Force);
    	}
    }
}
