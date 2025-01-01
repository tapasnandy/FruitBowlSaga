using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class CollideDetectionFloatingScoreTxt : MonoBehaviour
{
    [Header("Floating text of score")]
    [SerializeField] int carrotScore;
    [SerializeField] int guavaScore;
    [SerializeField] int watermelonScore;
    [SerializeField] int strawberryScore;
    [SerializeField] int pineappleScore;
    [SerializeField] int bomnbScore;
    [SerializeField] GameObject floatingTxtPrefab;
    private TextMeshPro floatingTxtPrefabText;
    public int totalScore;

    [Header("Audio sources")]
    [SerializeField] AudioSource correctWrongAudioSource;
    [SerializeField] AudioClip correctAudioClip;
    [SerializeField] AudioClip wrongAudioClip;

    [Header("Particle system")]
    [SerializeField] ParticleSystem bombParticle;
    private bool isBombParticlePlayable=false;
    [SerializeField] CameraShake CameraShake;

    [Header("Game Life system and score")]
    [SerializeField] List<GameObject> lifes;
    public int count;
    [SerializeField] TextMeshProUGUI scoreTxt;





    private void Start()
    {
        correctWrongAudioSource = correctWrongAudioSource.GetComponent<AudioSource>();

        bombParticle.Stop();
        count = 0;
        
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

        scoreTxt.text = totalScore.ToString();

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
        else if (collision.name == "Guava(Clone)")
        {
            showPositiveFloatingScoreTxt(guavaScore);
        }
        else if (collision.name == "Watermelon(Clone)")
        {
            showPositiveFloatingScoreTxt(watermelonScore);
        }
        else if (collision.name == "Strawberry(Clone)")
        {
            showPositiveFloatingScoreTxt(strawberryScore);
        }
        else if (collision.name == "Pineapple(Clone)")
        {
            showPositiveFloatingScoreTxt(pineappleScore);
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

        totalScore += score;
        floatingTxtPrefabText = floatingTxtPrefab.GetComponent<TextMeshPro>();
        floatingTxtPrefabText.text = "+" + score.ToString();
        floatingTxtPrefabText.color = Color.green;

        Instantiate(floatingTxtPrefab, transform.position, Quaternion.identity, transform);

        
        
    }

    void showNegativeFloatingScoreTxt(int score)
    {
        if (correctWrongAudioSource.isPlaying)
        {
            correctWrongAudioSource.Stop();
        }

        isBombParticlePlayable = true;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Handheld.Vibrate(); //for android
            StartCoroutine(CameraShake.Shake(0.15f, 0.4f));
        }

        correctWrongAudioSource.clip = wrongAudioClip;
        correctWrongAudioSource.Play();

        totalScore += score;
        floatingTxtPrefabText = floatingTxtPrefab.GetComponent<TextMeshPro>();
        floatingTxtPrefabText.text = score.ToString();
        floatingTxtPrefabText.color = Color.red;

        Instantiate(floatingTxtPrefab, transform.position, Quaternion.identity, transform);
        StartCoroutine(GamepadVibration()); //for gamepad

        if(count < 4)
        {
            lifes[count].SetActive(false);
            
        }
        count++;
        
    }

    IEnumerator GamepadVibration()
    {
        Gamepad.current.SetMotorSpeeds(1f, 1f);

        yield return new WaitForSeconds(0.3f);

        Gamepad.current.SetMotorSpeeds(0,0);
    }
}
