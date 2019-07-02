using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLineManager : MonoBehaviour
{

    private PlayerMovement pmove;
    private LineRenderer mLine;
    private DrawLine mDrawLine;
    public Material newLineMat;
    public Material oldLineMat;
    private bool hasOneFrameRendered = false;

    // Start is called before the first frame update
    void Start()
    {
        pmove = GetComponent<PlayerMovement>();
        mLine = GetComponent<LineRenderer>();
        mDrawLine = GetComponent<DrawLine>();
        oldLineMat = mLine.material;
        mLine.material = newLineMat;
    }

    void LateUpdate()
    {
        //if (pmove.joystickMagnitude > 66)
        //{
        //    mLine.SetPosition(0, transform.position + new Vector3(pmove.myJoystick.Horizontal, pmove.myJoystick.Vertical, 0).normalized * 1);
        //    mLine.SetPosition(1, transform.position + new Vector3(pmove.myJoystick.Horizontal, pmove.myJoystick.Vertical, 0).normalized * 2);
        //}
        //else
        //{
        //    mLine.SetPosition(0, transform.position + new Vector3(pmove.myJoystick.Horizontal, pmove.myJoystick.Vertical, 0).normalized * 0);
        //    mLine.SetPosition(1, transform.position + new Vector3(pmove.myJoystick.Horizontal, pmove.myJoystick.Vertical, 0).normalized * 0);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            mLine.material = oldLineMat;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mLine.material = newLineMat;
        }
        
    }
}
