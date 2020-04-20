using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private GameObject fishes;
    // Start is called before the first frame update
    void Start()
    {
        fishes = GameObject.Find("Fishes");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Transform child in fishes.transform)
            {
                //child.GetComponent<FishController>().EnableFish();
            }
        }
    }
}
