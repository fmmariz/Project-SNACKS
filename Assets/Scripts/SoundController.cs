using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    public List<string> sfxNames;
    [SerializeField]
    public List<AudioClip> sfxClips;
    [SerializeField]
    private GameObject audioSourcePrefab;

    private List<AudioSource> _availableAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        _availableAudioSource = new List<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(string sfxName)
    {
        AudioSource audioSource;
        if(_availableAudioSource.Count == 0)
        {
            GameObject gameObject = GameObject.Instantiate(audioSourcePrefab, transform);
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.transform.SetParent(gameObject.transform);
        }
        else
        {
            audioSource = _availableAudioSource[0];
            _availableAudioSource.RemoveAt(0);
        }
        audioSource.Stop();
        audioSource.time = 0.15f;
        AudioClip clip = sfxClips[sfxNames.IndexOf(sfxName)];
        audioSource.volume = 0.5f;
        audioSource.clip = clip;
        float clipLength = clip.length;

        audioSource.Play();
        StartCoroutine(EndOfSfxCallback(clipLength, audioSource));
    }

    private IEnumerator EndOfSfxCallback(float clipLength, AudioSource source)
    {
        yield return new WaitForSeconds(clipLength);

        _availableAudioSource.Add(source);
    }

    
}
