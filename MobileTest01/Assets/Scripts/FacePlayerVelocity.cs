using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerVelocity : MonoBehaviour
{
    public Rigidbody pBody;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            7));
        Vector3 forward = mouseWorld;
        forward.z = pBody.transform.position.z;
        transform.LookAt(forward);

        transform.position = pBody.transform.position;
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 mouse = Input.mousePosition;
    //    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
    //                                                        mouse.x,
    //                                                        mouse.y,
    //                                                        7));
    //    Vector3 forward = mouseWorld;
    //    forward.z = pBody.transform.position.z;
    //
    //    Gizmos.DrawSphere(forward,0.5f);
    //}
}
