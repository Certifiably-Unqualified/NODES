using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    ModularPendulumController mpc;

    public int index = 0;
    public bool active = false;
    public SpriteRenderer indicator;
    public List<GameObject> beams;

    // Start is called before the first frame update
    void Start()
    {
        if (mpc == null)
        {
            mpc = GameObject.FindWithTag("Player").GetComponent<ModularPendulumController>();
        }
        foreach (Transform t in transform)
        {
            if (t.name == "Beam")
            {
                beams.Add(t.gameObject);
            }
        }
        if (indicator == null)
        {
            indicator = transform.Find("Indicator").gameObject.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateJoint(int index)
    {
        this.index = index;
        active = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (active)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                mpc.CreateExplosion(transform);
                if (mpc.newJointBreak)
                {
                    List<GameObject> toRemove = mpc.sticks.GetRange(mpc.sticks.Count - 1, 1);
                    mpc.sticks.RemoveAt(mpc.sticks.Count - 1);
                    mpc.hinges.RemoveAt(mpc.hinges.Count - 1);
                    mpc.joints.RemoveAt(mpc.joints.Count - 1);
                    mpc.stickrbs.RemoveAt(mpc.stickrbs.Count - 1);
                    mpc.directions.RemoveAt(mpc.directions.Count - 1);
                    mpc.numbies.RemoveAt(mpc.numbies.Count - 1);
                    foreach (var stick in toRemove)
                    {
                        Destroy(stick);
                    }
                    if (mpc.sticks.Count == 0)
                    {
                        mpc.numbies.Clear();
                        mpc.sticks.Clear();
                        mpc.hinges.Clear();
                        mpc.stickrbs.Clear();
                        mpc.directions.Clear();
                        mpc.joints.Clear();
                    }
                }
                else
                {
                    //Debug.Log("omg wow im hit: " + index.ToString());
                    int amount = mpc.sticks.Count - index;
                    if (index == 0)
                    {
                        amount = mpc.sticks.Count;
                    }
                    List<GameObject> toRemove = mpc.sticks.GetRange(index, amount);
                    //Debug.Log(amount + " to remove from index. Amount selected from sticks: " + toRemove.Count);
                    mpc.sticks.RemoveRange(index, amount);
                    mpc.hinges.RemoveRange(index + 1, amount);
                    mpc.joints.RemoveRange(index + 1, amount);
                    mpc.stickrbs.RemoveRange(index, amount);
                    mpc.directions.RemoveRange(index, amount);
                    mpc.numbies.RemoveRange(index + 1, amount);
                    foreach (var stick in toRemove)
                    {
                        Destroy(stick);
                    }
                    if (mpc.numbies.Count == 1) // just in case i  fucked up
                    {
                        mpc.numbies.Clear();
                        mpc.sticks.Clear();
                        mpc.hinges.Clear();
                        mpc.stickrbs.Clear();
                        mpc.directions.Clear();
                        mpc.joints.Clear();
                    }
                }
            }
        }
    }
}