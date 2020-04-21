using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTrigger : MonoBehaviour
{
    private Slider slider;
    private bool RegenerateEnergy;

    private float actTime;
    private float maxTime;


    //GameObject padre que agrupa todos los peces del nivel
    private GameObject fishes;

    
    void Start()
    {
        slider = GameObject.Find("EnergySlider").GetComponent<Slider>();

        fishes = GameObject.Find("Fishes");
        GetComponent<SpriteRenderer>().enabled = false;

        maxTime = 25;
    }

    private void FixedUpdate()
    {
        if (RegenerateEnergy)
        {
            actTime += Time.fixedDeltaTime * 1000;

            if (slider.value < 100)
            {
                if (actTime >= maxTime)
                {
                    actTime = 0;
                    slider.value += 1;
                }
            }
            else
            {
                RegenerateEnergy = false;
            }
        }
    }

    //Cuando el Player entra al mar, respawnean peces y se desactiva el consumir energia.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Transform child in fishes.transform)
            {
                child.GetComponent<FishController>().EnableFish();
            }
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RegenerateEnergy = true;
            GameManager.Instance.consumeEnergy = false;
            GameManager.Instance.playerControl = true;
        }
    }

    //Cuando el Player sale del mar, comienza a consumir energia.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.consumeEnergy = true;
            RegenerateEnergy = false;
        }
    }
}
