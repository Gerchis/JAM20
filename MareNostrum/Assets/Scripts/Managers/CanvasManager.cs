using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    //TODO: PENDIENTE REVISAR TODO LA PARTE DE AUDIO DE ESTE SCRIPT, ESTE CODIGO ES PRODUCTO DE LA RECTA FINAL DONDE PREMIA EL FUNCIONAMIENTO DEL CODIGO ANTES QUE CUALQUIER OTRA COSA

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

    private float anticipation1Norm;
    private float anticipation2Norm;
    private float memefallNorm;

    private float memefallInitVol;

    public void RestartMemeSong()
    {
        anticipation1Played = false;
        anticipation2Played = false;
        memefallPlayed = false;
    }

    public void SetVolumen(AudioSource _as, float normVal, bool song)
    {
        Debug.Log("SETTUBG ");
        _as.volume = GameManager.Instance.masterVolumenValueSaved - normVal;

        if (song)
        {
            _as.volume = GameManager.Instance.musicVolumenValueSaved - normVal;
        }
        else
        {
            _as.volume = GameManager.Instance.effectsVolumenValueSaved - normVal;
        }
    }

    private float actTimeMusic;
    public float maxTimeMusic;
    public bool FadeOutMusic;
    private float fadeoutValue;
    private float initValue;

    private float actTimerSeagull;
    private float maxTimerSeagull;
    private bool seagullHit;

    // Start is called before the first frame update
    void Start()
    {
        

        seagullHit = false;
        actTimerSeagull = 0;
        maxTimerSeagull = 500;

        fadeoutValue = 0.01f;
        initValue = fadeoutValue;

        slider = GameObject.Find("EnergySlider").GetComponent<Slider>();

        //TODO: Arreglar esta waltrapada, no estoy aprovechando el sistema de soundmanager becausebugs
        anticipation1 = GameObject.Find("Music_2_Anticipation1").GetComponent<AudioSource>();
        anticipation1Norm = 0;
        anticipation2 = GameObject.Find("Music_3_Anticipation2").GetComponent<AudioSource>();
        anticipation2Norm = 0.3f;
        memefall = GameObject.Find("Music_4_MemeFall").GetComponent<AudioSource>();
        memefallNorm = 0.4f;

        //Obtenemos la energia a restar. Esto no se modifica durante la partida.
        energySubstract = GameManager.Instance.energySubstract;
        maxTime = GameManager.Instance.msToSubstractEnergy;

        maxTimeMusic = 300;
        actTimeMusic = 0;
        FadeOutMusic = false;
        RestartMemeSong();

        SetVolumen(anticipation1, anticipation1Norm, false);
        SetVolumen(anticipation2, anticipation2Norm, false);
        SetVolumen(memefall, memefallNorm, true);
        memefallInitVol = memefall.volume;
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
            if (slider.value <= 50 && !anticipation1.isPlaying && !anticipation2.isPlaying)
            {
                
                anticipation1.Play();
                anticipation2.Stop();
                memefall.Stop();
                anticipation1Played = true;
            }
            else if (slider.value > 0 && slider.value <= 25 && !anticipation2.isPlaying && !memefall.isPlaying)
            {
                anticipation1.Stop();
                anticipation2.Play();
                memefall.Stop();
                anticipation2Played = true;
            }


            if (!GameManager.Instance.playerControl && !memefall.isPlaying)
            {
                anticipation1.Stop();
                anticipation2.Stop();
                if (!GameManager.Instance.seagullHit)
                {
                    //memefall.Play();
                }
                else
                {
                    seagullHit = true;
                }
            }
            //else if (!GameManager.Instance.playerControl && !memefall.isPlaying)
            //{

            //    anticipation1.Stop();
            //    anticipation2.Stop();

            //    if(!memefallPlayed)
            //    {
            //        if (!GameManager.Instance.seagullHit)
            //        {
            //            memefall.Play();
            //        }
            //        else
            //        {
            //            seagullHit = true;
            //        }
            //        memefallPlayed = true;
            //    }


            //}


            if (GameManager.Instance.stopMeme)
            {
                fadeoutValue = fadeoutValue * 3;
                GameManager.Instance.stopMeme = false;
            }

        }
        
        if(seagullHit)
        {
            actTimerSeagull += Time.fixedDeltaTime * 1000;

            if (actTimerSeagull >= maxTimerSeagull)
            {

                memefall.Play();
                seagullHit = false;
                GameManager.Instance.seagullHit = false;

                actTimerSeagull = 0;
            }

        }

        if (GameManager.Instance.playerControl)
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
            fadeoutValue = initValue;
            memefall.volume = memefallInitVol;
        }
        

    }
}
