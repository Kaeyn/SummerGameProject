using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePipeSpawner : MonoBehaviour
{
    [SerializeField] float timer;
    private float countdown;
    [SerializeField] GameObject breakablePrefab;
    [SerializeField] GameObject prefab;
    List<GameObject> listPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = timer;
            getSpawnObject();
            Spawn();
        }
    }

    void getSpawnObject()
    {
        listPrefabs = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            listPrefabs.Add(prefab);
        }
        int randomPosition = UnityEngine.Random.Range(0, 3);
        listPrefabs[randomPosition] = breakablePrefab;
    }

    void Spawn()
    {
        float localPosition = transform.position.y;
        for (int i = 0; i < 4; i++)
        {
            float spawnPosition = localPosition - 2.5f * i;
            Vector3 vectorNew = new Vector3(transform.position.x, spawnPosition, transform.position.z);
            Instantiate(listPrefabs[i], vectorNew, Quaternion.identity);
        }
    }
}

