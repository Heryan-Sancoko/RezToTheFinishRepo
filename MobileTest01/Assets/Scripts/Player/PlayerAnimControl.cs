using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{

    private PlayerMovement pmove;
    private Joystick myJoystick;
    private Animator manim;
    public Transform reverseRotationReference;
    private Rigidbody rbody;
    private float inDashAnim = 0;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        pmove = transform.parent.GetComponent<PlayerMovement>();
        myJoystick = pmove.myJoystick;
        manim = GetComponent<Animator>();
        rbody = pmove.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (myJoystick.Horizontal != 0)
        {
            if (myJoystick.Horizontal < 0)
            {
                transform.rotation = new Quaternion(Quaternion.identity.x, reverseRotationReference.rotation.y, Quaternion.identity.z, transform.rotation.w);
            }
            else if (myJoystick.Horizontal > 0)
            {
                transform.rotation = transform.parent.rotation;
            }
        }
        else
        {
            manim.SetFloat("isRunning", 0);
        }

        if (inDashAnim > 0)
        {
            inDashAnim -= Time.deltaTime;
            manim.SetBool("isDashing", true);
        }

        RunningCheck();
        MomentumCheck();
        speed = Mathf.Abs(rbody.velocity.magnitude) / 8.5f;
    }

    private void LateUpdate()
    {
        if (!pmove.isGrounded || rbody.velocity.magnitude <= 0)
        {
            manim.SetBool("isIdle", false);
        }
    }

    void MomentumCheck()
    {
        if (!pmove.isGrounded)
        {
            manim.SetBool("isSliding", false);
            manim.SetBool("isIdle", false);
            manim.SetFloat("isRunning", 1);

            if (Mathf.Abs(rbody.velocity.x) > Mathf.Abs(rbody.velocity.y))
            {
                manim.SetBool("isDashing", true);
                manim.SetBool("isFalling", false);
            }
            else if (rbody.velocity.y < Mathf.Abs(rbody.velocity.x))
            {
                manim.SetBool("isFalling", true);
                manim.SetBool("isDashing", false);
            }
            else if (rbody.velocity.y > Mathf.Abs(rbody.velocity.x))
            {
                manim.SetBool("isDashing", false);
                manim.SetBool("isFalling", false);
            }
        }
    }

    void RunningCheck()
    {
        if (pmove.isGrounded)
        {
            if (pmove.isDashing)
                inDashAnim = 0.5f;
                
            if (inDashAnim <= 0 || rbody.velocity.magnitude < 0.2f)
                manim.SetBool("isDashing", false);

            if (rbody.velocity.magnitude <= 0)
            {
                manim.SetFloat("isRunning", 1);
                manim.SetBool("isIdle", true);
            }
            else
            {
                manim.SetBool("isIdle", false);
                manim.SetFloat("isRunning", (Mathf.Abs(rbody.velocity.magnitude) / 8.5f));
            }
        }
    }
}
