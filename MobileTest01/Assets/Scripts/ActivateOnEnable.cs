using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateOnEnable : MonoBehaviour
{
    public UnityEvent doThisOnEnable;

    // Start is called before the first frame update
    void Start()
    {
        doThisOnEnable.Invoke();
    }
}
