using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScorer : MonoBehaviour
{
    float hitbonus;
    // Start is called before the first frame update
    void Start()
    {
        hitbonus = PlayerPrefs.GetFloat("hitbonus");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("HITGENERAL");

        if (col.gameObject.CompareTag("Stick"))
        {

            //Debug.Log("HITPROJECTILE");
			hitbonus += 10;
			PlayerPrefs.SetFloat("hitbonus", hitbonus);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
