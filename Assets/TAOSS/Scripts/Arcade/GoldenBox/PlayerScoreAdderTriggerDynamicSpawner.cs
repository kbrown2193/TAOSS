using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreAdderTriggerDynamicSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private RigidBody2DSpawnData[] rigidBody2DSpawnDatas;

    private float timer = 0f;
    private float endTime = 60f;

    private int currentSpawnIndex;
    private bool hasReachedEnd;

    [SerializeField] private Transform spawnTransform; // use this to spawn


    // Start is called before the first frame update
    void Awake()
    {
        BeginSpawning();
    }

    // Update is called once per frame
    void BeginSpawning()
    {
        Debug.Log("Begin Spawning");
        timer = 0f;
        currentSpawnIndex = 0;
        StartCoroutine(SpawnPrefabsOverTime());
    }
    public IEnumerator SpawnPrefabsOverTime()
    {
        while(timer < endTime)
        {

            if (rigidBody2DSpawnDatas != null)
            {
                if (timer > endTime)
                {
                    Debug.Log("timer has reached end time... ");
                    yield break;
                }

                if (timer >= rigidBody2DSpawnDatas[currentSpawnIndex].spawnTime)
                {
                    Debug.Log("Spawn...");
                    // spawn
                    //spawnTransform.position = rigidBody2DSpawnDatas[currentSpawnIndex].spawnPosition;
                    GameObject spawnedGO = Instantiate(prefabToSpawn, spawnTransform);
                    spawnedGO.transform.position = rigidBody2DSpawnDatas[currentSpawnIndex].spawnPosition;

                    // apply force
                    spawnedGO.GetComponent<Rigidbody2D>().AddForceAtPosition(rigidBody2DSpawnDatas[currentSpawnIndex].initialForce, rigidBody2DSpawnDatas[currentSpawnIndex].initialForcePosition );
                    
                    // iterate
                    currentSpawnIndex++;
                }
                else
                {
                    // is not time to spawn
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }
}
[System.Serializable]
public class RigidBody2DSpawnData
{
    public float spawnTime;
    public Vector3 spawnPosition;
    public Vector3 spawnScale;
    public Vector2 initialForce;
    public Vector2 initialForcePosition;
}
