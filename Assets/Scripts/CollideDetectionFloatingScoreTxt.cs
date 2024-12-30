using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class CollideDetectionFloatingScoreTxt : MonoBehaviour
{
    [Header("Floating text of score")]
    [SerializeField] int carrotScore;
    [SerializeField] int cauliflowerScore;
    [SerializeField] int chickenScore;
    [SerializeField] int bomnbScore;
    [SerializeField] GameObject floatingTxtPrefab;
    private TextMeshPro floatingTxtPrefabText;

    [Header("Audio sources")]
    [SerializeField] AudioSource correctWrongAudioSource;
    [SerializeField] AudioClip correctAudioClip;
    [SerializeField] AudioClip wrongAudioClip;

    [Header("Particle system")]
    [SerializeField] ParticleSystem bombParticle;
    private bool isBombParticlePlayable=false;
    [SerializeField] CameraShake CameraShake;




    private void Start()
    {
        correctWrongAudioSource = correctWrongAudioSource.GetComponent<AudioSource>();
        
    }

    private void Update()
    {

        if (isBombParticlePlayable)
        {
            StartCoroutine(PlayBombParticle());
        }
        else
        {
            bombParticle.Stop();
        }

    }

    IEnumerator PlayBombParticle()
    {
        bombParticle.Play();
        yield return new WaitForSeconds(0.1f);
        isBombParticlePlayable = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Carrot(Clone)")
        {
            showPositiveFloatingScoreTxt(carrotScore);
        }
        else if (collision.name == "Cauliflower(Clone)")
        {
            showPositiveFloatingScoreTxt(cauliflowerScore);
        }
        else if (collision.name == "Chicken(Clone)")
        {
            showPositiveFloatingScoreTxt(chickenScore);
        }
        else if (collision.name == "Bomb(Clone)")
        {
            
            showNegativeFloatingScoreTxt(bomnbScore);
        }
    }


    void showPositiveFloatingScoreTxt(int score)
    {
        if(correctWrongAudioSource.isPlaying)
        {
            correctWrongAudioSource.Stop();
        }

        isBombParticlePlayable = false;

        correctWrongAudioSource.clip = correctAudioClip;
        correctWrongAudioSource.Play();

        floatingTxtPrefabText = floatingTxtPrefab.GetComponent<TextMeshPro>();
        floatingTxtPrefabText.text = "+" + score.ToString();
        floatingTxtPrefabText.color = Color.green;

        Instantiate(floatingTxtPrefab, transform.position, Quaternion.identity, transform);
        Handheld.Vibrate();
    }

    void showNegativeFloatingScoreTxt(int score)
    {
        if (correctWrongAudioSource.isPlaying)
        {
            correctWrongAudioSource.Stop();
        }

        isBombParticlePlayable = true;

        StartCoroutine(CameraShake.Shake(0.15f, 0.3f));

        correctWrongAudioSource.clip = wrongAudioClip;
        correctWrongAudioSource.Play();

        floatingTxtPrefabText = floatingTxtPrefab.GetComponent<TextMeshPro>();
        floatingTxtPrefabText.text = score.ToString();
        floatingTxtPrefabText.color = Color.red;

        Instantiate(floatingTxtPrefab, transform.position, Quaternion.identity, transform);
        Handheld.Vibrate();
        StartCoroutine(GamepadVibration());
    }

    IEnumerator GamepadVibration()
    {
        Gamepad.current.SetMotorSpeeds(1f, 1f);

        yield return new WaitForSeconds(0.3f);

        Gamepad.current.SetMotorSpeeds(0,0);
    }
}
