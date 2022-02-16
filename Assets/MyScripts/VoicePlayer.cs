using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    // 再生したい音源リソースをセットする
    public AudioClip voiceSample;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // scriptを貼り付けたオブジェクトにAudioSourceのコンポーネントを追加する
        audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f;
    }

    public void OnClickBtn()
    {
        audioSource.clip = voiceSample;
        // 音源が再生されていたら止める、生成されていなければ再生する（先頭から）
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            return;
        }
        audioSource.time = 0f;
        audioSource.Play();
    }
}