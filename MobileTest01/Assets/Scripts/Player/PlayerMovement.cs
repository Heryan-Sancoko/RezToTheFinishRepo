using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{

    public Joystick myJoystick;
    public Transform myJoystickHandlePos;
    public float horizontalInput;
    public float verticalInput;
    public float joystickMagnitude;
    public float moveSpeed;
    public float topSpeed;
    public Collider airCollider;
    private Collider groundCollider;

    //-----DASH VARIABLES----//
    public GameObject myDashParticle;
    public Transform myLookKeeper;
    private DrawLine mTrajectory;
    public bool isDashing;
    private bool hasDashStarted = false;
    public float dashSpeed;
    public float baseDashCount;
    public float dashCurrentCount;
    public float baseDashDuration;
    public float dashCurrentDuration;
    public float baseDashCooldown;
    [SerializeField]
    private float dashCurrentCooldown;
    private Vector3 dashDirection;
    public float preservedH;
    public float preservedV;
    public bool inertiaStopped = false;
    private bool hasSparkled = true;
    private bool hasDashParticleBeenReleased = true;
    private float dashTailTime;
    private float dashLineWidth;
    public Transform ShootParticleFromHere;
    public GameObject sparkleParticle;
    public TrailRenderer mTrail;
    public UnityEvent doOnDash;
    //-----------------------//

    public float velocityMagnitude;
    public Animator mAnim;
    private GroundedCheck mGroundCheck;
    private Rigidbody rbody;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        mGroundCheck = GetComponent<GroundedCheck>();
        groundCollider = GetComponent<Collider>();
        mTrajectory = GetComponent<DrawLine>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckTouch();
        horizontalInput = myJoystick.Horizontal;
        verticalInput = myJoystick.Vertical;
    }

    private void LateUpdate()
    {
        ReleaseDashParticle();
    }

    void FixedUpdate()
    {

        if (horizontalInput != 0 && verticalInput != 0 && dashCurrentDuration <=0)
        {
            preservedH = horizontalInput;
            preservedV = verticalInput;
        }

        joystickMagnitude = Vector2.Distance(myJoystick.transform.position, myJoystickHandlePos.position);
        GroundedCheck();
        Move();
        Dash();
        Sparkle();

        velocityMagnitude = rbody.velocity.magnitude;

        float playerXPOS = Camera.main.WorldToScreenPoint(transform.position, Camera.MonoOrStereoscopicEye.Mono).x;
        float playerYPOS = Camera.main.WorldToScreenPoint(transform.position, Camera.MonoOrStereoscopicEye.Mono).y;
        Vector3 potentialEnergy = new Vector3(Input.mousePosition.x - playerXPOS, Input.mousePosition.y - playerYPOS, 0).normalized * (dashSpeed*1.2f) * Time.deltaTime;
        //Vector3 potentialEnergy = new Vector3(preservedH, preservedV, 0).normalized * (dashSpeed * 1.2f) * Time.deltaTime;
        potentialEnergy -= rbody.velocity;

        mTrajectory.addedV.x = potentialEnergy.x;
        mTrajectory.addedV.y = potentialEnergy.y;
    }

    void Dash()
    {
        Debug.DrawRay(transform.position, new Vector3(myJoystick.Horizontal, myJoystick.Vertical, 0).normalized * 5,Color.red);

        if (hasDashStarted)
        {
            hasDashParticleBeenReleased = false;
            dashCurrentDuration = baseDashDuration;
            hasDashStarted = false;
        }

        if (dashCurrentDuration > 0)
        {
            isDashing = true;
            rbody.velocity = new Vector3(preservedH, preservedV, 0).normalized * dashSpeed * Time.deltaTime;
            dashCurrentDuration -= Time.deltaTime;
        }

    }

    void Sparkle()
    {
        if (!hasSparkled && dashCurrentCooldown <= 0 && dashCurrentCount != 0)
        {
            if (sparkleParticle != null)
                Instantiate(sparkleParticle,transform);
            hasSparkled = true;
        }
    }

    void ResetDash()
    {
        dashCurrentCooldown = 0;
        dashCurrentCount = 1;
        if (!isGrounded)
        Instantiate(sparkleParticle, transform);

        hasSparkled = true;
    }

    void Move()
    {

        Vector3 myCurrentSpeed = rbody.velocity;

        if (!inertiaStopped && dashCurrentDuration <= 0)
        {
            isDashing = false;
            //myCurrentSpeed = Vector3.zero;
            myCurrentSpeed = rbody.velocity;
            inertiaStopped = true;
            Debug.Log("Inertia stopped");
        }

        Vector3 HSpeedModifier = Vector3.right * moveSpeed * myJoystick.Horizontal * Time.deltaTime;
        Vector3 VSpeedModifier = Vector3.up * moveSpeed * myJoystick.Vertical * Time.deltaTime;

        if (inertiaStopped)
        {
            isDashing = false;

            if (dashCurrentCooldown > 0)
                dashCurrentCooldown -= Time.deltaTime;

            if (joystickMagnitude != 0)
            {
                if (isGrounded)
                    rbody.velocity = Vector3.Lerp(rbody.velocity, new Vector3(HSpeedModifier.x, myCurrentSpeed.y, 0), 0.05f);
                //rbody.velocity = new Vector3(HSpeedModifier.x, myCurrentSpeed.y, 0);
                //rbody.velocity = new Vector3(HSpeedModifier.x, VSpeedModifier.y, 0);
                else
                    rbody.velocity = Vector3.Lerp(rbody.velocity, new Vector3(HSpeedModifier.x, myCurrentSpeed.y, 0), 0.05f);
            }
        }

        mTrail.startWidth = Mathf.Lerp(mTrail.startWidth, dashLineWidth, 0.1f);
        mTrail.time = Mathf.Lerp(mTrail.time, dashTailTime, 0.1f);
    }

    public void StopDash()
    {
        inertiaStopped = false;
        dashCurrentDuration = 0;
    }

    void CheckTouch()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    Debug.Log("Touching Started");
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    if (Input.touchCount <= 1)
                    {
                        if (joystickMagnitude >= 66 && dashCurrentCooldown <= 0 && dashCurrentCount != 0)
                        {
                            hasDashStarted = true;
                            inertiaStopped = false;
                            preservedH = horizontalInput;
                            preservedV = verticalInput;
                            dashCurrentCooldown = baseDashCooldown;
                            dashCurrentCount -= 1;
                            hasSparkled = false;
                            CameraShaker.Instance.ShakeOnce(5, 2, 0.1f, 0.1f);
                            Debug.Log("touching ceased");
                        }
                    }
                    else
                        touch = Input.GetTouch(1);
                    break;
            }
        }

        if (dashCurrentCooldown <= 0 && dashCurrentCount != 0)
        {
            //Mouse version of touch inputs
            if (Input.GetMouseButtonUp(0) && joystickMagnitude >= 66)
            {
                hasDashStarted = true;
                inertiaStopped = false;
                preservedH = horizontalInput;
                preservedV = verticalInput;
                dashCurrentCooldown = baseDashCooldown;
                dashCurrentCount -= 1;
                hasSparkled = false;
                doOnDash.Invoke();
                CameraShaker.Instance.ShakeOnce(5, 2, 0.1f, 0.1f);
                Debug.Log("touching ceased");
            }
        }
    }

    void GroundedCheck()
    {
        isGrounded = mGroundCheck.isGrounded;
        mAnim.SetBool("isGrounded", isGrounded);
        if (dashCurrentCount != 1 && dashCurrentCooldown <= 0 && isGrounded)
        {
            dashCurrentCount = 1;
        }

        if (isGrounded)
        {
            groundCollider.enabled = true;
            airCollider.enabled = false;
        }
        else
        {
            groundCollider.enabled = false;
            airCollider.enabled = true;
        }
    }

    public void ZeroVelocity()
    {
        rbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlusDash")
        {
            ResetDash();
        }
    }

    public void ReleaseDashParticle()
    {
        if (dashTailTime > 0.2f)
        {
            dashLineWidth = 0.05f;
            dashTailTime -= 0.05f;
        }
        else
        {
            dashLineWidth = 0.334f;
            dashTailTime = 0.2f;
        }
        if (!hasDashParticleBeenReleased)
        {
            Instantiate(myDashParticle, ShootParticleFromHere.position, myLookKeeper.rotation, transform);
            hasDashParticleBeenReleased = true;
            dashTailTime = 1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            StopDash();
            ResetDash();
        }
    }
}
