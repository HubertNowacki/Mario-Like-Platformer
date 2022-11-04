using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBird : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == MyTags.PLAYER_TAG)
        {
            print("SUKA");
        }
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
