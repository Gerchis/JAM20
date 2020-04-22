using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Slider slider;
    private float energySubstract = 0;

    private float actTime;
    private float maxTime;

    private AudioSource anticipation1;
    private AudioSource anticipation2;
    private AudioSource memefall;

    private bool anticipation1Played;
    private bool anticipation2Played;
    private bool memefallPlayed;

    public void RestartMemeSong()
    {
        anticipation1Played = false;
        anticipation2Played = false;
        memefallPlayed = false;
    }

    private float actTimeMusic;
    public float maxTimeMusic;
    private bool FadeOutMusic;
    public float fadeoutValue;

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("EnergySlider").GetComponent<Slider>();

        //TODO: Arreglar esta waltrapada
        anticipation1 = GameObject.Find("Music_2_Anticipation1").GetComponent<AudioSource>();
        anticipation2 = GameObject.Find("Music_3_Anticipation2").GetComponent<AudioSource>();
        memefall = GameObject.Find("Music_4_MemeFall").GetComponent<AudioSource>();

        //Obtenemos la energia a restar. Esto no se modifica durante la partida.
        energySubstract = GameManager.Instance.energySubstract;
        maxTime = GameManager.Instance.msToSubstractEnergy;

        maxTimeMusic = 500;
        actTimeMusic = 0;
        FadeOutMusic = false;
        RestartMemeSong();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.Instance.consumeEnergy)
        {
            actTime += Time.fixedDeltaTime*1000;
            
            if (slider.value > 0)
            {
                if (actTime >= maxTime)
                {
                    actTime = 0;
                    slider.value -= energySubstract;
                }
            }
            else
            {
                actTime = 0;
                GameManager.Instance.consumeEnergy = false;
                GameManager.Instance.playerControl = false;
            }
        }

        if(!GameManager.Instance.playerInSea)
        {
            if (slider.value <= 50 && !anticipation1Played)
            {
                anticipation1.Play();
                anticipation2.Stop();
                memefall.Stop();
                anticipation1Played = true;
            }
            else if (slider.value <= 25 && !anticipation2Played)
            {
                anticipation1.Stop();
                anticipation2.Play();
                memefall.Stop();
                anticipation2Played = true;
            }
            else if (!GameManager.Instance.playerControl && !memefallPlayed)
            {
                //TODO: EJECUTAR CANCION DESPUES DE QUE SUENE PAJARO
                anticipation1.Stop();
                anticipation2.Stop();
                memefall.Play();
                memefallPlayed = true;
            }

            //TODO: EASY WAY, AL SALIR DEL AGUA QUE PARE LA MUSICA Y HAGA EL RESET DE BOOLS
            if(GameManager.Instance.playerControl && memefall.isPlaying)
            {
                fadeoutValue = fadeoutValue * 5;
            }

        }
        



        if (GameManager.Instance.playerInSea)
        {
            if (FadeOutMusic)
            {

                actTimeMusic += Time.fixedDeltaTime * 1000;

                if (actTimeMusic >= maxTimeMusic)
                {

                    memefall.volume -= fadeoutValue;
                    if (memefall.volume <= 0)
                    {
                        memefall.Stop();
                    }
                    actTimeMusic = 0;
                }
            }
            RestartMemeSong();
        }

        if (memefall.isPlaying)
        {
            FadeOutMusic = true;
        }
        else
        {
            FadeOutMusic = false;
        }
        

    }
}
