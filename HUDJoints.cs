using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDJoints : MonoBehaviour
{

    ModularPendulumController mpc;
    public GameObject OneJoint;
    public GameObject TwoJoint;
    public GameObject ThreeJoint;
    public GameObject FourJoint;

    private void Start()
    {
        OneJoint.GetComponent<Renderer>().enabled = true;
        OneJoint.GetComponent<Renderer>().enabled = true;
        OneJoint.GetComponent<Renderer>().enabled = true;
        OneJoint.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        int caseSwitch = mpc.sticks.Count;

        switch (caseSwitch)
        {
            case 1:
                OneJoint.GetComponent<Renderer>().enabled = false;
                break;
            case 2:
                TwoJoint.GetComponent<Renderer>().enabled = false; 
                break;
            case 3:
                ThreeJoint.GetComponent<Renderer>().enabled = false;
                break;
            case 4:
                FourJoint.GetComponent<Renderer>().enabled = false;
                break;
        }
    }
}
