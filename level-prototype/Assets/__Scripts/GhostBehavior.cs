using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    private Ray groundRay;
    public GameObject targetObject;
    private Rigidbody rb;
    private float startTime;
    private float y0;

    public bool childGhost = false;
    public float floatHeight = 3f;
    public float floatSpeed = 5f;
    public float speed = 1f;
    public int sightDistance = 20;
    [Range(0, 90)] public float fieldOfView = 80;

    void Start()
    {
        targetObject = PlayerBehavior.S.gameObject;
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
        y0 = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 tempPos = transform.position;
        float age = Time.time - startTime;
        float theta = Mathf.PI * 2 * age / floatSpeed;
        float sin = Mathf.Sin(theta);
        tempPos.y = y0 + floatHeight * sin;
        transform.position = tempPos;

        if (targetInSight(targetObject.transform))
        {
            Vector3 targetPos = targetObject.transform.position;
            Vector3 targetDir = targetPos - transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * 0.05f);

            Quaternion toRotation = Quaternion.LookRotation(targetDir, transform.up);
            Quaternion rot = Quaternion.Lerp(transform.rotation, toRotation, 100f);
            rot.x = rot.z = 0;

            transform.rotation = rot;
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(transform.position, transform.forward));
    }

    bool targetInSight(Transform target)
    {
        Vector3 targetDir = (target.position - transform.position).normalized;
        Ray sightline = new Ray(transform.position + targetDir, targetDir);
        RaycastHit hit;
        if (Vector3.Angle(transform.forward, targetDir) < fieldOfView && 
            Physics.Raycast(sightline, out hit, sightDistance) &&
            hit.collider.gameObject.tag == target.gameObject.tag) 
        {
            return true;
        }

        return false;
    }
}
