using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{

    private static SoundMaster _instance;
    [SerializeField] private AudioSource bg_source;
    [SerializeField] private AudioSource on_hit_source;

    public static SoundMaster Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void playBackground(AudioClip audioClip, Transform spamTrans, float volumn)
    {
        // spam in game object
        AudioSource bg = Instantiate(bg_source, spamTrans.position, Quaternion.identity);

        // assign audio clip
        bg.clip = audioClip;
        bg.volume = volumn;
        bg.Play();
    }

}
