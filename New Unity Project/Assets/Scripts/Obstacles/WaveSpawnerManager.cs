﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveArray
    {
        public HelperArray[] arrayOfWaves;
        public float spawnAfterTimer;
        public bool dontWaitForWaveToEnd;
    }
    
    [System.Serializable]
    public class HelperArray
    {
        public GameObject enemiesToSpawnEachRound;
        public Transform spawnLocations;
    }

    public int currentWave = 0;
    public WaveArray[] Waves;
    private bool isTriggeredAlready;
    public GameObject spawner;
    public GameObject currentRoom;
    public GameObject spawnAnimation;

    public UnityEngine.Events.UnityEvent ClearedWaves;
    public UnityEngine.Events.UnityEvent WavesTriggered;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("player") && !isTriggeredAlready)
        {
            InvokeEnemies();
            isTriggeredAlready = true;
            WavesTriggered.Invoke();
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < Waves[currentWave].arrayOfWaves.Length; i++)
        {
            GameObject currentGameObject = Waves[currentWave].arrayOfWaves[i].enemiesToSpawnEachRound;
            Transform spawnLocation = Waves[currentWave].arrayOfWaves[i].spawnLocations;
            GameObject enemy = Instantiate(currentGameObject, spawnLocation.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(currentRoom.transform);
            enemy.GetComponent<Enemy>().wasSpawnedBySpawnManager = true;
            enemy.GetComponent<Enemy>().spawnManager = gameObject;
          //  Debug.Log(currentWave + " " + Waves[currentWave].arrayOfWaves[i].enemiesToSpawnEachRound + " " + Waves[currentWave].arrayOfWaves[i].spawnLocations);
        }
        currentWave++;
        if (currentWave < Waves.Length && Waves[currentWave].dontWaitForWaveToEnd == true)
        {
            InvokeEnemies();
        }
    }

    private IEnumerator StartSpawning(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < Waves[currentWave].arrayOfWaves.Length; i++)
        {
            Transform spawnLocation = Waves[currentWave].arrayOfWaves[i].spawnLocations;
            GameObject spawnAnim = Instantiate(spawnAnimation, spawnLocation.transform.position, Quaternion.identity) as GameObject;
            //  Debug.Log(currentWave + " " + Waves[currentWave].arrayOfWaves[i].enemiesToSpawnEachRound + " " + Waves[currentWave].arrayOfWaves[i].spawnLocations);
        }
        yield return new WaitForSeconds(0.3f);
        SpawnEnemies();
    }

    public void InvokeEnemies()
    {
        if (currentWave < Waves.Length)
            StartCoroutine(StartSpawning(Waves[currentWave].spawnAfterTimer));
        else
            ClearedWaves.Invoke();
    }
}
