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

        if (slider.value <= 50 && slider.value >25)
        {
            SoundManager.Instance.PlayMusic("Anticipation1");
        }
        else if (slider.value <= 25 && slider.value > 0)
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayMusic("Anticipation2");
        }
        else if (slider.value<=0)
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayMusic("MemeFall");
        }
    }
}
