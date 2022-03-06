using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameController gameManager;
    public GameObject rawImage;
    VideoPlayer videoPlayer;

    string path;

    // Start is called before the first frame update
    void Start()
    {
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        vp.prepareCompleted -= PrepareCompleted;
        // vp.Play();
    }

    public void OnClickBtn()
    {
        path = Application.streamingAssetsPath + "/" + gameManager.pieceID;
        Debug.Log(path);
        PlayMovie();
    }

    void PlayMovie(){
        videoPlayer = rawImage.GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = path;
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.Prepare();

        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            return;
        }
        videoPlayer.Play(); 
    }
}
