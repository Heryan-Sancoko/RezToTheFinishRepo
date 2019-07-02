using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    public UnityEvent doOnTriggerEnter;
    public UnityEvent doOnTriggerExit;
    private Transform pTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doOnTriggerEnter.Invoke();
            Debug.Log("player entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (doOnTriggerExit != null)
        {
            if (other.tag == "Player")
            {
                doOnTriggerExit.Invoke();
                Debug.Log("player exit with tag");
            }

            doOnTriggerExit.Invoke();

            Debug.Log("player exit");
        }
    }

    public void HideBody(Transform mTransform)
    {
        foreach (Transform child in mTransform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }

            foreach (Transform child2 in child)
            {
                if (child2.GetComponent<MeshRenderer>() != null)
                {
                    child2.GetComponent<MeshRenderer>().enabled = false;
                }

                foreach (Transform child3 in child2)
                {
                    if (child3.GetComponent<MeshRenderer>() != null)
                    {
                        child3.GetComponent<MeshRenderer>().enabled = false;
                    }

                    foreach (Transform child4 in child3)
                    {
                        if (child4.GetComponent<MeshRenderer>() != null)
                        {
                            child4.GetComponent<MeshRenderer>().enabled = false;
                        }

                        foreach (Transform child5 in child4)
                        {
                            if (child5.GetComponent<MeshRenderer>() != null)
                            {
                                child5.GetComponent<MeshRenderer>().enabled = false;
                            }
                            foreach (Transform child6 in child5)
                            {
                                if (child6.GetComponent<MeshRenderer>() != null)
                                {
                                    child6.GetComponent<MeshRenderer>().enabled = false;
                                }
                                foreach (Transform child7 in child6)
                                {
                                    if (child7.GetComponent<MeshRenderer>() != null)
                                    {
                                        child7.GetComponent<MeshRenderer>().enabled = false;
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
    }

    public void FadeTrail(TrailRenderer mtrail)
    {
        mtrail.emitting = false;
    }

    public void GetPTransform(Transform player)
    {
        pTransform = player.transform;
    }

    public void MoveToPlayerPos(GameObject mGameObject)
    {
        mGameObject.SetActive(true);
        mGameObject.transform.position = pTransform.position;
        mGameObject.transform.LookAt(mGameObject.transform.position + pTransform.GetComponent<Rigidbody>().velocity);
        Quaternion newRot = mGameObject.transform.rotation;

        newRot.x = Quaternion.identity.x;
        newRot.z = Quaternion.identity.z;

        mGameObject.transform.rotation = newRot;
    }




}


