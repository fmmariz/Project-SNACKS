using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumpSfx;
    [SerializeField]
    private AudioClip destroySfx;
    [SerializeField]
    private AudioClip winSfx;
    [SerializeField]
    private AudioClip powerUpSfx;
    [SerializeField]
    private AudioSource _audioSource;

    public enum SoundEffects
    {
        JUMP,
        DESTROY,
        WIN,
        POWERUP
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(SoundEffects sfx)
    {
        switch (sfx)
        {
            case SoundEffects.JUMP:
                _audioSource.clip = jumpSfx; break;
                case SoundEffects.DESTROY:
                _audioSource.clip = destroySfx; break;
                case SoundEffects.WIN:
                _audioSource.clip = winSfx;
                break;
                case SoundEffects.POWERUP:
                _audioSource.clip = powerUpSfx; break;
        }
        _audioSource.Play();

    }
}
