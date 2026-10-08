using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] AudioSource SFXSource;

    public AudioClip round_start;
    public AudioClip punch;
    public AudioClip game_win;
    public AudioClip applaud1;
    public AudioClip applaud2;
    public AudioClip applaud3;

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
