using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    public List<AudioClip> IdleSounds;
    public List<AudioClip> JumpSounds;

    public GameObject Music;
    public AudioSource IntroMusic;
    public GameObject Scratch;

    public float MaxIdleCooldown = 8f;
    public float MinIdleCooldown = 3f;
    private bool _didStart;

    private void Awake()
    {
        CatRotationStuff.OnFall.AddListener(() =>
        {
            Scratch.SetActive(true);
            Scratch.GetComponent<AudioSource>().time = .15f;
            var music = Music.GetComponent<AudioSource>();
            var initial = music.volume;
            Lerp.FromTo(0.25f, t => music.volume = t, initial, 0f);
        });
        Lerp.Delay(Random.Range(MinIdleCooldown, MaxIdleCooldown), PlayIdleSound);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_didStart)
        {
            _didStart = true;
            Music.SetActive(true);
            var music = Music.GetComponent<AudioSource>();
            var initial = music.volume;
            Lerp.FromTo(0.25f, t => music.volume = t, 0f, initial);
            music.time = 5.4f;
            var initialIntro = IntroMusic.volume;
            Lerp.FromTo(0.25f, t => IntroMusic.volume = t, initialIntro, 0f);
        }
    }

    private void PlayIdleSound()
    {
        if (!_didStart)
        {
            PlayFromList(IdleSounds);
            Lerp.Delay(Random.Range(MinIdleCooldown, MaxIdleCooldown), PlayIdleSound);
        }
    }

    public void PlayJumpSound()
    {
        PlayFromList(JumpSounds);
    }

    private void PlayFromList(List<AudioClip> list)
    {
        if (list.Count == 0) return;
        AudioSource.PlayClipAtPoint(list[Random.Range(0, list.Count)], Camera.main.transform.position);
    }
}
