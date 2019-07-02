using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendObjectHere : MonoBehaviour
{
    public Transform objectToBeYeeted;
    public Transform yeetObjectHere;

    public void YEET()
    {
        objectToBeYeeted.transform.position = yeetObjectHere.transform.position;
    }

}
