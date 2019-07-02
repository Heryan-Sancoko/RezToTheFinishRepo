using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystickToPlayer : MonoBehaviour
{

    public Transform mPlayer;
    public Camera mCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = mCam.WorldToScreenPoint(mPlayer.position);

    }
}
