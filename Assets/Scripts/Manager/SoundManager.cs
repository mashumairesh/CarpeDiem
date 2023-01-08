using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    // Start is called before the first frame update

    public bool isSetUp = false;

    [SerializeField] private AudioSource Source;
    [SerializeField] private List<AudioClip> sound;
    [SerializeField] private List<float> SoundAmount;

    private void OnEnable()
    {

        if (instance == null)
            instance = this;

        if (!isSetUp)
        {
            DontDestroyOnLoad(this);
            isSetUp = true;
        }
    }

    
    
    public void PlayAudio(SoundType rsh)
    {

        Source.volume = SoundAmount[(int)rsh];
        Source.clip = sound[(int)rsh];
        Source.Play();
    }

}

public enum SoundType
{ 
    LoadDeck = 0,
    Bell,
    UIBtn,
    UIOn,
    UIOff,

}
