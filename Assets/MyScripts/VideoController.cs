using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject rawImage;
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = rawImage.GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        // videoPlayer.videClip = videClip;
    }

    public void OnClickBtn()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            return;
        }
        videoPlayer.Play();
    }
}
