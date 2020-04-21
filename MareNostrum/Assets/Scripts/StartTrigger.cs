using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    //GameObject padre que agrupa todos los peces del nivel
    private GameObject fishes;
    
    void Start()
    {
        fishes = GameObject.Find("Fishes");
    }

    //Cuando el Player entra al mar, respawnean peces y se desactiva el consumir energia.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.consumeEnergy = false;
            GameManager.Instance.RestoreEnergy();

            foreach (Transform child in fishes.transform)
            {
                child.GetComponent<FishController>().EnableFish();
            }
        }
    }

    //Cuando el Player sale del mar, comienza a consumir energia.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.consumeEnergy = true;
        }
    }
}
