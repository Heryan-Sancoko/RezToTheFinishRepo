using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{

    private Image mImage;
    public bool doYouWantToFadeToWhite = false;

    // Start is called before the first frame update
    void Start()
    {
        mImage = GetComponent<Image>();
        mImage.color = Vector4.one;
    }

    // Update is called once per frame
    void Update()
    {

        if (doYouWantToFadeToWhite)
        {
            mImage.color = Vector4.Lerp(mImage.color, new Vector4(1, 1, 1, 1), 0.1f);
            if (mImage.color.a > 0.99)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
        else
            mImage.color = Vector4.Lerp(mImage.color, new Vector4(1, 1, 1, 0), 0.1f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            doYouWantToFadeToWhite = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);

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
                    doYouWantToFadeToWhite = true;
                    Debug.Log("touching ceased 2");
                    break;
            }
        }

    }

    public void FadeAway()
    {
        doYouWantToFadeToWhite = true;
    }
}
