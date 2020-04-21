using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Slider slider;
    private float energySubstract;


    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        
        //Obtenemos la energia a restar. Esto no se modifica durante la partida.
        energySubstract = GameManager.Instance.energySubstract;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.Instance.consumeEnergy)
        {
            if (slider.value > 0)
            {
                slider.value -= energySubstract;
            }
            else
            {
                GameManager.Instance.playerControl = false;
            }
        }
    }
}
