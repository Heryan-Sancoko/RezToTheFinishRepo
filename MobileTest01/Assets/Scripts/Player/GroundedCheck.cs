using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{

    public LayerMask mLayerMask;
    public float distanceToObstacle = 0;
    public float sphereRadius;
    public float sphereMaxDistance;
    public bool isGrounded;
    public GameObject currentHitObject;
    private Vector3 sphereLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position + (Vector3.up * 0.2f);

        if (Physics.SphereCast(p1, sphereRadius, -transform.up, out hit, sphereMaxDistance, mLayerMask))
        {
            currentHitObject = hit.transform.gameObject;
            distanceToObstacle = hit.distance;
            isGrounded = true;
        }
        else
        {
            distanceToObstacle = sphereMaxDistance;
            currentHitObject = null;
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 p1 = transform.position + (Vector3.up * 0.2f);

        Gizmos.color = Color.red;
        Debug.DrawLine(p1, p1 + (-transform.up) * distanceToObstacle);
        Gizmos.DrawWireSphere(p1 + (-transform.up) * distanceToObstacle, sphereRadius);
    }

}
