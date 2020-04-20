﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Clase para almacenar audios
[System.Serializable]
public class Audios
{
    public string name;
    public AudioClip clip;
    public float normalizedValue;
    public enum Type
    {
        EFFECT,
        MUSIC,
        END_TYPE
    }
    public Type type;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.2f;


    private AudioSource source;


    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public AudioSource getSource()
    {
        return source;
    }

    public void Play()
    {
        //Seteamos volumen del sonido
        source.volume = GameManager.Instance.masterVolumenValueSaved;

        switch (type)
        {
            case Type.EFFECT:
                source.volume *= GameManager.Instance.effectsVolumenValueSaved;
            break;
            case Type.MUSIC:
                source.volume *= GameManager.Instance.musicVolumenValueSaved;
            break;
            default:
                source.volume *= GameManager.Instance.masterVolumenValueSaved;
            break;
        }

        //Normalizamos el audio mediante una variable propia del clip. 0 == no necesita normalizar.
        //Esto lo usamos para nivelar audios.
        if (normalizedValue != 0) { source.volume *= normalizedValue; }

        //Si randomPitch=0, pitch queda sin modificar
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));

        //Al ser distintos AudioSource se pueden ejecutar a la vez, no es necesario PlayAtOnce()
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}


public class SoundManager : MonoBehaviour
{

    /*index
      ###########################
      #                         #
      #  FUNCIONES DE AUDIO     #
      #                         #
      ###########################
    */
    #region AUDIOFUNCTIONS
    [SerializeField]
    public Audios[] sounds;
    public Audios[] musics;
    private AudioSource actualMusic;

    //Función para buscar el sonido y reproducirlo.
    public void PlaySound(string _name)
    {
        Debug.Log("Playing sound");
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager> Sound not found: " + _name);
        return;
    }

    //Función para de un array de IDs, reproducir un sonido aleatorio. 
    public void PlayRandomSound(string _name)
    {
        //Unity no deja hacer callback de funciones con más de un parametro o array, por lo tanto creamos un sistema interno.
        int[] _ids = new int[0];

        //Dependiendo del audio random, asignamos aqui las ids a seleccionar de manera aleatoria
        switch (_name)
        {
            case "Seagull":
                _ids = new int[] { 0, 1, 2 };
            break;
        }

        //Obtenemos un número random del array int
        int random = Random.Range(0, _ids.Length);
        Debug.Log(random);

        //Reproducimos el audio seleccionado
        sounds[_ids[random]].Play();
        
        return;
    }

    //Función para parar todos los sonidos en reproducción.
    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Stop();
        }
    }

    //Función para parar un sonido concreto.
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning("AudioManager> Sound not found: " + _name);
        return;
    }


    //Función para buscar una música y reproducirla
    public void PlayMusic(string _name)
    {
        //Paramos la música que esté sonando ahora mismo
        actualMusic.Stop();

        //Reproducimos la nueva música
        for (int i = 0; i < musics.Length; i++)
        {
            if (musics[i].name == _name)
            {
                musics[i].Play();
                actualMusic = musics[i].getSource();
                return;
            }
        }
        Debug.LogWarning("AudioManager> Sound not found: " + _name);
        return;
    }

    //Función para parar la música.
    public void StopMusic()
    {
        actualMusic.Stop();
        return;
    }
    #endregion

    
    /*index
      ########################
      #                      #
      #  FUNCIONES DE UNITY  #
      #                      #
      ########################
    */

    // Instanciar SoundManager
    public static SoundManager Instance { get; private set; }

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

        //Instanciamos las músicas
        for (int i=0; i< musics.Length; i++)
        {
            GameObject _go = new GameObject("Music_" + i + "_" + musics[i].name);
            _go.transform.SetParent(this.transform);
            musics[i].setSource(_go.AddComponent<AudioSource>());
        }

        //Instanciamos los efectos de sonido
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("SoundEffect_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].setSource(_go.AddComponent<AudioSource>());
        }
    }

    private void Update()
    {
        //Loop de música
        //if (actualMusic.isPlaying == false)
        //{
        //    actualMusic.Play();
        //}
    }
}