using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplasScriptController : MonoBehaviour
{
    void Update()
    {

        //TODO: Para splash screen video if (videoPlayer.time > 0 && !videoPlayer.isPlaying || Input.anyKey)
        if (Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
    }
}
