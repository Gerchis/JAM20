using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*index
        #########################
        #                       #
        #  GAME OPTIONS: SOUND  #
        #                       #
        #########################
    */

    public bool playerControl;

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

        //TESTING ZONE
        //TESTING END ZONE
    }
}
