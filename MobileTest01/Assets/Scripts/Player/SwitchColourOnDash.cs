using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchColourOnDash : MonoBehaviour
{

    public Material noDashMat;
    private Material stillDashesLeftMat;
    private PlayerMovement pmove;
    private MeshRenderer mSkin;

    // Start is called before the first frame update
    void Start()
    {
        pmove = transform.root.GetComponent<PlayerMovement>();
        mSkin = transform.GetComponent<MeshRenderer>();
        stillDashesLeftMat = mSkin.material;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pmove.dashCurrentCount == 0)
        {
            case true:
                mSkin.material = noDashMat;
                break;
            default:
                mSkin.material = stillDashesLeftMat;
                break;
        }
    }
}
