using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularPendulumController : MonoBehaviour
{
    public List<GameObject> sticks;
    public List<HingeJoint2D> hinges;
    public List<JointController> joints;

    public List<Rigidbody2D> stickrbs; // rb2d number 1 should be localstick
    Rigidbody2D localstick;

    public List<float> directions;
    float direction;

    public List<int> numbies, gaies = new List<int>() { 1, 2 };

    public float torqueBst = 270f;
    public bool endsOnly = false;
    bool safetyFirst;
    [SerializeField]
    private int locked = 2;
    public bool newJointBreak = true;
    bool BoostDelay = false;

    public GameObject explosion, lockPulse;
    public AnimationClip explosionClip, lockPulseClip;

    public AudioManager am;

    public GameObject stickPrefab, jointPrefab;

    [SerializeField]
    List<JointController> activeJoints;

    CameraController cam;
    Transform focus;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("flowmode") == "false")
        {
            safetyFirst = true;
        }
        else if (PlayerPrefs.GetString("flowmode") == "true")
        {
            safetyFirst = false;
        }

        if (am == null)
        {
            am = Camera.main.gameObject.GetComponent<AudioManager>();
        }

        GenerateData();

        cam = Camera.main.gameObject.GetComponent<CameraController>();
        focus = cam.center;
    }

    // Update is called once per frame
    void Update()
    {
        // Boost in angular direction
        for (int i = 0; i <= directions.Count - 1; i++)
        {
            directions[i] = GetSign(stickrbs[i].angularVelocity);
        }

        locked = 0;
        foreach (var hinge in hinges)
        {
            if (hinge.enabled)
            {
                locked++;
            }
        }
        
        // Control
        if (endsOnly)
        {
            foreach (var keyNum in gaies)
            {
                if (Input.GetKeyDown(keyNum.ToString()))
                {
                    int temp2 = 0, temp3 = 0;
                    if (keyNum == gaies[1])
                    {
                        temp2 = hinges.Count - 1;
                        temp3 = hinges.Count - 2;
                    }

                    if (safetyFirst)
                    {
                        if (hinges[temp2].enabled && locked == 1)
                        {
                            // cannot because its nono
                        }
                        else
                        {
                            hinges[temp2].enabled = !hinges[temp2].enabled;
                            CreateLockPulse(joints[temp2].transform);
                            locked += GetBoolF(hinges[temp2].enabled);
                            direction = directions[temp3];
                            localstick = stickrbs[temp3];
                        }
                        continue;
                    }

                    foreach (var hinge in hinges)
                    {
                        if (hinges[temp2] == hinge)
                        {
                            hinge.enabled = true;
                            CreateLockPulse(joints[temp2].transform);
                            continue;
                        }
                        hinge.enabled = false;
                    }

                    direction = directions[temp3];
                    localstick = stickrbs[temp3];
                }
            }
        }
        else
        {
            foreach (var keyNum in numbies)
            {
                if (Input.GetKeyDown(keyNum.ToString()))
                {
                    int temp2 = keyNum - 1, temp3 = keyNum - 1;
                    if (keyNum == numbies.Count)
                    {
                        temp3 = keyNum - 2;
                    }

                    if (safetyFirst)
                    {
                        if (hinges[temp2].enabled && locked == 1)
                        {
                            // cannot because its nono
                        }
                        else
                        {
                            hinges[temp2].enabled = !hinges[temp2].enabled;
                            CreateLockPulse(joints[temp2].transform);
                            locked += GetBoolF(hinges[temp2].enabled);
                            direction = directions[temp3];
                            localstick = stickrbs[temp3];
                        }
                        continue;
                    }
                    //CreateLockPulse(activeJoints);

                    foreach (var hinge in hinges)
                    {
                        if (hinges.IndexOf(hinge) == temp2)
                        {
                            hinge.enabled = true;
                            CreateLockPulse(joints[temp2].transform);
                            continue;
                        }
                        hinge.enabled = false;
                    }
                    

                    direction = directions[temp3];
                    localstick = stickrbs[temp3];
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && BoostDelay == false)
        {
            StartCoroutine(Booost());
            foreach (var rb in stickrbs)
            {
                rb.AddTorque(torqueBst * directions[0], ForceMode2D.Force);
            }
            am.PlayOnce(am.boost);
        }

        foreach (var hinge in hinges)
        {
            JointController joint = joints[hinges.IndexOf(hinge)];
            if (hinge.enabled && !activeJoints.Contains(joint))
            {
                activeJoints.Add(joint);
                joint.indicator.enabled = true;
            }
            if (!hinge.enabled && activeJoints.Contains(joint))
            {
                activeJoints.Remove(joint);
                joint.indicator.enabled = false;
            }
        }
    }

    void ProceduralGeneration()
    {
        int sticks = Random.Range(1, 3);
        
    }
    void ProceduralAddition()
    {

    }

    public void CreateExplosion(Transform transform)
    {
        StartCoroutine(Emit(transform, explosion, explosionClip, 3));
        if (Random.Range(1, 2) == 1)
        {
            am.PlayOnce(am.explosion1);
        }
        else
        {
            am.PlayOnce(am.explosion2);
        }
    }
    void CreateLockPulse(Transform transform)
    {
        StartCoroutine(Emit(transform, lockPulse, lockPulseClip, 5, false, true, "LockPulseInsertion"));
        StartCoroutine(Lock());
        am.PlayOnce(am.jointLock);
    }

    IEnumerator Emit(Transform transform, GameObject prefab, AnimationClip clip, int mult, bool shake = true, bool parent = false, string parentName = "")
    {
        GameObject emmission = Instantiate(prefab, transform.position, transform.rotation);
        if (parent)
        {
            emmission.transform.parent = transform.Find(parentName); ;
        }
        if (shake)
        {
            StartCoroutine(Booost(clip.length / (2 * mult)));
        }
        yield return new WaitForSeconds(clip.length / mult);
        Destroy(emmission);
    }

    float GetSign(float input)
    {
        return (input / Mathf.Abs(input));
    }
    int GetBoolF(bool input)
    {
        if (input)
        {
            return 1;
        }
        return -1;
    }

    IEnumerator Booost(float time = 0.15f)
    {
        cam.shaking = true;
        yield return new WaitForSeconds(time);
        cam.shaking = false;
        BoostDelay = true;
        yield return new WaitForSeconds(0.3f); // subject to balancing
        BoostDelay = false;
    }

    IEnumerator Lock(float time = 0.06f)
    {
        cam.shaking = true;
        yield return new WaitForSeconds(time);
        cam.shaking = false;
    }

    void GenerateData()
    {
        hinges.Clear();
        joints.Clear();
        stickrbs.Clear();
        directions.Clear();
        numbies.Clear();

        foreach (var stick in sticks) // we only want to add the empty joint as that joint is the one simulating spatial locking. the other joint is the one that actually connects the sticks. join to the right.
        {
            hinges.Add(stick.GetComponents<HingeJoint2D>()[0]);
            if (sticks.IndexOf(stick) == 0)
            {
                hinges.Add(stick.GetComponents<HingeJoint2D>()[1]);
            }
            stickrbs.Add(stick.GetComponent<Rigidbody2D>());
            List<JointController> tempJ = stick.transform.GetComponentsInChildren<JointController>().ToList();
            foreach (var joint in tempJ)
            {
                joints.Add(joint);
                joint.ActivateJoint(joints.IndexOf(joint));
            }
        }

        for (int i = 0; i < hinges.Count - 1; i++)
        {
            directions.Add(0f);
        }

        int temp = 0;
        foreach (var hinge in hinges)
        {
            temp++;
            numbies.Add(temp);
        }
    }
}