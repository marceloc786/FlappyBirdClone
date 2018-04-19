using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum soundsGame {
    die,
    hit,
    menu,
    point,
    wing
}

public class SoundController : MonoBehaviour {

    public AudioClip soundDie;
    public AudioClip soundHit;
    public AudioClip soundMenu;
    public AudioClip soundPoint;
    public AudioClip soundWing;

    public static SoundController instance;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	public void PlaySound (soundsGame currentSound)
    {
        switch (currentSound)
        {
            case soundsGame.die:
                {
                    GetComponent<AudioSource>().PlayOneShot(soundDie);
                }
                break;
            case soundsGame.hit:
                {
                    GetComponent<AudioSource>().PlayOneShot(soundHit);
                    Invoke("PlaySoundDie", 0.3f);
                }
                break;
            case soundsGame.menu:
                {
                    GetComponent<AudioSource>().PlayOneShot(soundMenu);
                }
                break;
            case soundsGame.point:
                {
                    GetComponent<AudioSource>().PlayOneShot(soundPoint);
                }
                break;
            case soundsGame.wing:
                {
                    GetComponent<AudioSource>().PlayOneShot(soundWing);
                }
            break;
        }
    }

    private void PlaySoundDie()
    {
        PlaySound(soundsGame.die);
    }
}
