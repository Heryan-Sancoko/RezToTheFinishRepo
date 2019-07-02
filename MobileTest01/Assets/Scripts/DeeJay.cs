using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeeJay : MonoBehaviour
{
    static GameObject vaporwaveMusic;
    private AudioSource mMusic;
    public AudioSource mCassette;
    public float timeUntilMusic;
    public MeshRenderer mMesh;


    private void Awake()
    {
        if (vaporwaveMusic != null)
        {
            gameObject.SetActive(false);
        }
        else
            vaporwaveMusic = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        mMusic = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        mMusic.volume = 0;
        mMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mCassette.enabled = true;

        if (timeUntilMusic > 0)
            timeUntilMusic -= Time.deltaTime;

        if (timeUntilMusic <= 0)
        {
            mMusic.enabled = true;
            mMusic.volume = Mathf.Lerp(mMusic.volume, 1, 0.005f);
        }
    }
}
