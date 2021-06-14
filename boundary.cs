using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            if (gameObject.name == "Floor")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
            }
            if (gameObject.name == "Ceiling")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -600));
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            if ((gameObject.name == "Left") || (gameObject.name == "Your other Left"))
            {
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * velocity.x, 0);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            if (gameObject.name == "Floor")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
            }
            if (gameObject.name == "Ceiling")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -700));
            }
            if (gameObject.name == "Left")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0));
            }
            if (gameObject.name == "Your other Left")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
            }
            if ((gameObject.name == "Left") || (gameObject.name == "Your other Left"))
            {
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-4 * velocity.x, 0);
            }

            
        }
    }






    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            if ((gameObject.name == "Floor") || (gameObject.name == "Ceilling"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
                *//*Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 300-7 * velocity.y);*//*
            }
            if (gameObject.name == "Ceilling")
            {
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -4 * velocity.y);
            }
            if ((gameObject.name == "Left") || (gameObject.name == "Your other Left"))
            {
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-4*velocity.x, 0);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            if ((gameObject.name == "Floor") || (gameObject.name == "Ceilling"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
                *//*Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 300-7 * velocity.y);*//*
            }
            if (gameObject.name == "Ceilling")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000));
            }
            if ((gameObject.name == "Left") || (gameObject.name == "Your other Left"))
            {
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-4 * velocity.x, 0);
            }
        }
    }*/

}
