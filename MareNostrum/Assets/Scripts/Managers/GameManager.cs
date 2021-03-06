﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum CharacterType
    {
        NONE,
        MERMAID,
        TRITON
    }

    /*index
        ######################
        #                    #
        #  SCENE CONTROLLER  #
        #                    #
        ######################
    */
   public void LoadScene (int _id)
    {
        SceneManager.LoadScene(_id);
    }

    public CharacterType characterSelection;

    public void LoadIngameReferences()
    {
        //Cogemos referencias
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
    }

    public void ApplyResolution()
    {
        switch (Resolution)
        {
            case 1:
            Screen.SetResolution(240, 426, false);
            break;
            case 2:
            Screen.SetResolution(450, 800, false);
            break;
            case 3:
            Screen.SetResolution(900, 1600, false);
            break;
        }
    }

    public void SetResolution(int i)
    {
        Resolution = i;
        ApplyResolution();
    }

    /*index
        ######################
        #                    #
        #  ENERGY MANAGEMENT #
        #                    #
        ######################
    */
    private Slider energySlider;
    public float msToSubstractEnergy;
    public float energySubstract;
    public float energyDash;

    public void ModifyEnergy(float _value)
    {
        energySlider.value += _value;
    }

    public void RestoreEnergy()
    {
        energySlider.value = energySlider.maxValue;
    }

    /*index
        ######################
        #                    #
        #  PUBLIC VARIABLES  #
        #                    #
        ######################
    */

    public bool playerControl;
    public bool consumeEnergy;
    public bool playerInSea;
    public bool seagullHit;
    public bool stopMeme;
    public int Resolution;


    /*index
        #########################
        #                       #
        #  GAME OPTIONS: SOUND  #
        #                       #
        #########################
    */

    //Valores actuales de audio
    public float masterVolumenValueSaved;
    public float musicVolumenValueSaved;
    public float effectsVolumenValueSaved;

    //Al aplicar cambios en menú opciones: Valores guardados de las opciones
    public void saveActualValues(float _masterV, float _musicV, float _effectsV)
    {
        masterVolumenValueSaved = _masterV;
        musicVolumenValueSaved = _musicV;
        effectsVolumenValueSaved = _effectsV;
    }

    /*index
        ########################
        #                      #
        #  FUNCIONES DE UNITY  #
        #                      #
        ########################
    */

    // Instanciar GameManager
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }

    }

    private void Start()
    {
        //Habilitamos el control del personaje. Pasa a false cuando se queda sin energia y cae.
        playerControl = true;
        consumeEnergy = false;
        Resolution = 2;

        //TESTING ZONE
        //TESTING END ZONE
    }
}
