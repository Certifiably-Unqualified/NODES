using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb2;
    public Vector2 ProjectileVelocity;
    public float maxVel = 10;
    public Vector3 stageDimensions;

    GameObject mp;

    // Start is called before the first frame update
    void Start()
    {
        mp = GameObject.FindWithTag("Player");
        Timer timer = mp.GetComponent<Timer>();
        maxVel = timer.currentscore / 30 + 10;

        rb2 = gameObject.GetComponent<Rigidbody2D>();

        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector2(-stageDimensions.x, stageDimensions.y);

        int spawnChoice = (int)Random.Range(0, 4);

        if (spawnChoice == 0) //left
        {
            transform.position = new Vector2(-stageDimensions.x - 5, Random.Range(-stageDimensions.y, stageDimensions.y));
        }
        if (spawnChoice == 1) //right
        {
            transform.position = new Vector2(stageDimensions.x + 5, Random.Range(-stageDimensions.y, stageDimensions.y));
        }
        if (spawnChoice == 2) //top
        {
            transform.position = new Vector2(Random.Range(-stageDimensions.x, stageDimensions.x), stageDimensions.y + 5);
        }
        if (spawnChoice == 3) //bottom
        {
            transform.position = new Vector2(Random.Range(-stageDimensions.x, stageDimensions.x), -stageDimensions.y - 5);
        }
        float centralError = 3; //how far the trajectory deviates from exact centre
        Vector2 direction = (new Vector2(Random.Range(-centralError, centralError), Random.Range(-centralError, centralError)) - new Vector2(transform.position.x, transform.position.y)).normalized;

        ProjectileVelocity = Random.Range(maxVel/2, maxVel) * direction;
        transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * 180/Mathf.PI - 90));
    }


    // Update is called once per frame
    void Update()
    {
        rb2.velocity = ProjectileVelocity;
        if ((Mathf.Abs(transform.position.x) > 2*stageDimensions.x) || (Mathf.Abs(transform.position.y) > 2*stageDimensions.y))
        {
            Destroy(gameObject);
        }
    }

    void Destruct()
    {
        mp.GetComponent<ModularPendulumController>().CreateExplosion(transform);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Stick"))
        {
            Destruct();
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destruct();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            Destruct();
        }
    }
}
// 