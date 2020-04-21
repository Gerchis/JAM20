using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    /*index
      #####################
      #                   #
      #  CONTROL CANVAS   #
      #                   #
      #####################
    */

    public GameObject mainmenuCanvas;
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;
    public GameObject playCanvas;

    /*index
      ########################
      #                      #
      #  OPCIONES DE JUEGO   #
      #                      #
      ########################
    */

    private Slider masterVolumen;
    private Slider musicVolumen;
    private Slider effectsVolumen;

    public void saveAudio()
    {
        GameManager.Instance.saveActualValues(masterVolumen.value, musicVolumen.value, effectsVolumen.value);
    }

    /*index
      ######################
      #                    #
      #  FUNCIONES UNITY   #
      #                    #
      ######################
    */

    // Start is called before the first frame update
    void Start()
    {
        masterVolumen = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
        musicVolumen = GameObject.Find("MusciVolumeSlider").GetComponent<Slider>();
        effectsVolumen = GameObject.Find("EffectsVolumeSlider").GetComponent<Slider>();

        mainmenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        playCanvas.SetActive(false);
}
}
