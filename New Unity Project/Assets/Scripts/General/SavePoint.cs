using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    private GameMaster gm;
  
    void Start()
    {
        gm = GameMaster.instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            gm.savePointPosition = transform.position;
        }
    }
}
