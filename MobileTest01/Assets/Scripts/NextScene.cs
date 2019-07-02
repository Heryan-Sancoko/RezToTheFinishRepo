using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    static bool hasTitleShown = false;

    // Start is called before the first frame update
    void Start()
    {
        if (hasTitleShown)
            transform.root.gameObject.SetActive(false);
        else
            Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeMeToTheNextScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void StartTheGame()
    {
        hasTitleShown = true;
        Time.timeScale = 1;
        transform.root.gameObject.SetActive(false);
    }

}
