using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplasScriptController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }

    void Update()
    {

        if (videoPlayer.time > 0 && !videoPlayer.isPlaying || Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
    }
}
