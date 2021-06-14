using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public bool stopSpawning = false;
    public float Tperiod = 1;

    void Start()
    {
        StartCoroutine(spawnProj());
    }

    IEnumerator spawnProj()
    {
        while (true)
        {
            Instantiate(projectile, new Vector3(0, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(Tperiod);
        }
    }

    void Update()
    {
        GameObject mp = GameObject.Find("Modular Pendulum");
        Timer timer = mp.GetComponent<Timer>();
        Tperiod = Mathf.Max((3 - timer.currentscore / 100), 0.2f);
    }
}
