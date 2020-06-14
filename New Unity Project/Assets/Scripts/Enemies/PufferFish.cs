using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : MonoBehaviour, ISFXResetable
{
    public float respawnTime;
    public GameObject spawnAnim;
    private Vector2 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }
    public IEnumerator RespawnCoroutine()
    {
        Debug.Log("coroutine");
        yield return new WaitForSeconds(respawnTime);
        GameObject spawnAnimation = Instantiate(spawnAnim, originalPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Enemy>().PlayerRespawned();
    }

    public void ResetSFXCues()
    {

    }

    public void SetStateDead()
    {
        GetComponent<Enemy>().isBeingPulledDown = false;
        GetComponent<Enemy>().StopPullDownVFX();
    }
}
