using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class MoviePlayer : MonoBehaviour
{
    [SerializeField]
    private string relativePath;
    VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.playOnAwake = false;
        player.isLooping = true;
        player.source = VideoSource.Url;
        // player.url = Application.streamingAssetsPath + "/" + relativePath;
        player.url = "https://conditionyellow.net/test/StreamingAssets/Piece001.mp4";
        player.prepareCompleted += PrepareCompleted;
        // player.Prepare();
    }
    void PrepareCompleted(VideoPlayer vp)
    {
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
    }
    
    public void OnClickBtn()
    {
        if (player.isPlaying)
        {
            player.Pause();
            return;
        }
        player.Play();
    }
}