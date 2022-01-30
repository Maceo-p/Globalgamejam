using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    public List<AudioClip> audioLeftMan;
    public List<AudioClip> audioRightMan;
}
