using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform center;

    public bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // FixedUpdate is called 25 times per frame
    void FixedUpdate()
    {
        if (!shaking)
        {
            transform.position = new Vector3(center.position.x, center.position.y, center.position.z - 1.5f);
        }
        else
        {
            Shake(.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shake(float r = 1f)
    {
        transform.position = new Vector3(center.position.x + Random.Range(-r, r), center.position.y + Random.Range(-r, r), center.position.z - 1.5f);
    }
}
