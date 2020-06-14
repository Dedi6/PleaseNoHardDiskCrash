using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesDoor : MonoBehaviour
{

    public Animator animator;
    private bool boolValue = true;
    public void OpenDoor()
    {
        GameMaster gm = GameMaster.instance;
        gm.GetComponent<AudioManager>().PlaySound(AudioManager.SoundList.DoorClose);
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("IsOpen", true);
        boolValue = true;
    }

    public void CloseDoor()
    {
        GameMaster gm = GameMaster.instance;
        gm.GetComponent<AudioManager>().PlaySound(AudioManager.SoundList.DoorClose);
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetBool("IsOpen", false);
        boolValue = false;
    }

    void OnEnable()
    {
        animator.SetBool("IsOpen", boolValue);
    }

    void OnDisable()
    {
        animator.SetBool("IsOpen", boolValue);
    }
}
