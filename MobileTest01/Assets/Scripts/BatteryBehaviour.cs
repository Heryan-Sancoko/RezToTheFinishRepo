using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehaviour : MonoBehaviour
{
    public float baseRespawnTimer;
    public GameObject sparkParticle;
    private float currentRespawnTimer;
    private Collider mCollider;
    private Vector3 currentLocalScale;

    // Start is called before the first frame update
    void Start()
    {
        currentLocalScale = transform.localScale;
        mCollider = transform.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentRespawnTimer > 0)
        {
            currentRespawnTimer -= Time.deltaTime;
        }

        if (currentRespawnTimer <= 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, currentLocalScale, 0.2f);
            mCollider.enabled = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            currentRespawnTimer = baseRespawnTimer;
            transform.localScale = Vector3.zero;
            mCollider.enabled = false;
            Instantiate(sparkParticle, transform.position, Quaternion.identity);

        }
    }
}
