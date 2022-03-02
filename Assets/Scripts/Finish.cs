using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if(other.gameObject.GetComponent<PlayerController>())
        {
            other.gameObject.GetComponent<PlayerController>().anim.SetTrigger("Dance");
        }
        else if(other.gameObject.GetComponent<AIPlayerController>())
        {
            other.gameObject.GetComponent<AIPlayerController>().AIanim.SetTrigger("Dance");
        }
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
}
