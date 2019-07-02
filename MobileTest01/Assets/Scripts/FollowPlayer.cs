using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform mPlayer;
    public float camZoom = -9;
    public float yOffset = 0;
    public float xRotation;
    public float lerpspeed = 0.1f;
    public bool isLevelFinished = false;
    private Rigidbody pBody;
    private float lockedY;

    // Start is called before the first frame update
    void Start()
    {
        pBody = mPlayer.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition;

        if (pBody.velocity.magnitude > 1)
        {
            newPosition = mPlayer.transform.position + (pBody.velocity).normalized * 1;
        }
        else
            newPosition = mPlayer.transform.position;

        newPosition.y += yOffset;
        newPosition.z = camZoom;

        if (isLevelFinished)
        {
            lerpspeed = Mathf.Lerp(lerpspeed, 0.01f, 0.2f);
            newPosition.y = lockedY;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, lerpspeed);

        Quaternion oldRot = transform.rotation;
        transform.LookAt(mPlayer.position);
        Quaternion newRot = transform.rotation;

        newRot.x = xRotation;
        newRot.y = oldRot.y;
        transform.rotation = newRot;

    }

    public void ChangeCamZoom(float newOffset)
    {
        camZoom = newOffset;
    }

    public void ChangeLerpspeed(float newLerpspeed)
    {
        lerpspeed = newLerpspeed;
    }

    public void FinishLevel()
    {
        isLevelFinished = true;
        lockedY = transform.position.y;
    }
}
