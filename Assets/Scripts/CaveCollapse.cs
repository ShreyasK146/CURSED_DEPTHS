using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCollapse : MonoBehaviour
{
    #region SpawnElements and SpawnAreas
    [SerializeField] List<GameObject> rocks = new List<GameObject>();
    [SerializeField] List<Transform> spawnAreas = new List<Transform>();
    #endregion

    #region Variables to control the time of cave collapse
    
    private float timerElapsed = 0f; // Time gradually increases untill it's more than timeForCaveCollapse;
    private float elapsedTime = 0f; // Time gradually increases untill it's more than collapseDuration
    private float spawnInterval = 0.05f; // Time interval between rock spawns
    private float collapseDuration = 10f; // Time untill the random rocks spawn to show cave is collapsing
    [HideInInspector] public bool caveCollapsing = false;
    private bool isSpawning = false;
    private bool isTimerActive = false; //Starts the timerElapsed
    
    #endregion

    [SerializeField] DoorAnimationPlayer player;
    [SerializeField] Transform playerCurrentLocation;
    [SerializeField] AudioSource caveCollapseAudio;
    [SerializeField] PlayerHealthController playerHealthController;
    [SerializeField] ObjectiveTexts objective;
    [HideInInspector] public bool playerDied = false;

    #region Methods to update the rockfalling and cavecollapse timer
    private void Update()
    {

        //Debug.Log(DifficultyElements.timeForCaveCollapse);
        if (caveCollapsing && !caveCollapseAudio.isPlaying && !playerDied) 
        {
            caveCollapseAudio.Play();
            objective.objective.text = "Quickly Return to the Island to Escape the Collapsing Cave";
            objective.optionObjective.text = "";
            StartTimer();
        }
        if (isTimerActive)
        {
            timerElapsed += Time.deltaTime;

            if (timerElapsed >= DifficultyElements.timeForCaveCollapse)
            {
                // Stop the timer and trigger the rocks to fall above the player
                isTimerActive = false;
                TriggerRocksAbovePlayer();
            }
        }

        if (player.cursedDoorOpened && !isSpawning)
        {
            caveCollapsing = true;
            isSpawning = true;
            StartCoroutine(SpawnRandomRocks());
        }

        if (isSpawning)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= collapseDuration)
            {
                isSpawning = false;
            }
        }
 
    }
    private void StartTimer()
    {
        isTimerActive = true;
        timerElapsed = 0f;
    }
    #endregion

    #region Spawn the rocks above player to kill him
    private void TriggerRocksAbovePlayer()
    {
        float abovePlayerHeight = 0.5f; 
        float offsetInFront = 1f;
        Vector3 playerPosition = playerCurrentLocation.transform.position;
        Vector3 offset = playerCurrentLocation.transform.forward * offsetInFront;
        Vector3 abovePlayerPosition = playerPosition + Vector3.up * abovePlayerHeight + offset;

        for (int i = 0; i < 10 + Random.Range(0, 2); i++)
        {
            GameObject randomRock = rocks[Random.Range(0, rocks.Count)];
            GameObject newRock = Instantiate(randomRock, abovePlayerPosition, Quaternion.identity);
            Rigidbody rockRigidbody = newRock.GetComponent<Rigidbody>();
            if (rockRigidbody == null)
            {
                rockRigidbody = newRock.AddComponent<Rigidbody>();
            }
        }
        StartCoroutine(Dead());
        playerHealthController.currentHealth = 0f;
        caveCollapseAudio.Stop();
        playerDied = true;
    }
    #endregion

    #region Small delay to make the health 0
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
    }
    #endregion

    #region Spawn random rocks on random spawnarea
    IEnumerator SpawnRandomRocks()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(spawnInterval);

        while (elapsedTime < collapseDuration)
        {
            Transform randomSpawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector3 spawnPosition = GetRandomSpawnPosition(randomSpawnArea);
            GameObject randomRock = rocks[Random.Range(0, rocks.Count)];
            GameObject newRock = Instantiate(randomRock, spawnPosition, Quaternion.identity);
            Rigidbody rockRigidbody = newRock.GetComponent<Rigidbody>();
            if (rockRigidbody == null)
            {
                rockRigidbody = newRock.AddComponent<Rigidbody>();
            }
            yield return spawnDelay; // Wait for the specified delay before the next spawn
        }
    }
    #endregion

    #region Generate random position to spawn rock
    Vector3 GetRandomSpawnPosition(Transform spawnArea)
    {
        // Calculate random position within the specified spawn area
        float randomX = Random.Range(-spawnArea.localScale.x / 2, spawnArea.localScale.x / 2);
        float randomZ = Random.Range(-spawnArea.localScale.z / 2, spawnArea.localScale.z / 2);
        float randomY = Random.Range(-spawnArea.localScale.y / 2, spawnArea.localScale.y / 2);
        Vector3 randomPosition = new Vector3(randomX + spawnArea.position.x, randomY + spawnArea.position.y, randomZ + spawnArea.position.z);
        return randomPosition;
    }
    #endregion

}