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


    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        
        //Obtenemos la energia a restar. Esto no se modifica durante la partida.
        energySubstract = GameManager.Instance.energySubstract;
        maxTime = GameManager.Instance.msToSubstractEnergy;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.Instance.consumeEnergy)
        {
            actTime += Time.fixedDeltaTime*1000;
            Debug.Log("actTime: "+actTime);
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
    }
}
