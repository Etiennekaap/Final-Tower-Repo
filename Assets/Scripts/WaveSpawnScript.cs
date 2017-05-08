using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawnScript : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public Transform SpawnSouth;
    public Transform SpawnWest;
    public Transform SpawnEast;
    public Transform SpawnNorth;

    private int waveIndex = 0;
    public float timeBetweenWaves = 8f;
    private float countdown = 6f;
    int spawnIndex;

    public Text waveCountDownText;

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Update();                                                              //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//
    void Awake()
    {
    }

    #region Code Summary Update
    /// <summary>
    /// each time the wave countdown timer reaches 0. Start a Coroutine of SpawnWave()
    /// reset the countdown timer to the time between waves afterwards
    /// -----------
    /// decrement the countdowntimer based on deltaTime and update the UI to notify the player
    /// </summary>
    #endregion

    void Update ()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            PlayerStats.resources += 10;
            // TODO: EDIT ON UI!
            Debug.Log("YOU GAINED 10 resources. You now have " + PlayerStats.resources + " resources!");
        }

        countdown -= Time.deltaTime;

        waveCountDownText.text = ("Next wave will spawn in: " + Mathf.Round(countdown).ToString());
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     SpawnWave();                                                           //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary SpawnWave (Coroutine)
    /// <summary>
    /// Coroutine for Spawning Waves
    /// -----------
    /// (Developer Note) [Coroutines are used to wait a certain time to iterate through the function using yield return new WaitForSeconds(float Time)]
    /// -----------
    /// spawns enemies based on the waveIndex using SpawnEnemy()
    /// -----------
    /// TODO: Implement a real spawning algorithm. WHAT IS USED NOW IS FOR TESTING PURPOSES ONLY!!!!!
    /// </summary>
    /// <returns></returns>
    #endregion

    IEnumerator SpawnWave()
    {
        spawnIndex = Random.Range(1, 5);

        if (spawnIndex == 1)
        {
            spawnPoint = SpawnSouth;
        }

        if (spawnIndex == 2)
        {
            spawnPoint = SpawnWest;
        }

        if (spawnIndex == 3)
        {
            spawnPoint = SpawnEast;
        }

        if (spawnIndex == 4)
        {
            spawnPoint = SpawnNorth;
        }
    // DEBUG ONLY!!!!!!
        spawnPoint = SpawnNorth;
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     SpawnEnemy();                                                          //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary SpawnEnemy
    /// <summary>
    /// instantiates a new enemyPrefab on the Spawn position
    /// </summary>
    #endregion

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
       //GameObject _obj =
       //_obj.GetComponent<EnemyScript>();
    }
}
