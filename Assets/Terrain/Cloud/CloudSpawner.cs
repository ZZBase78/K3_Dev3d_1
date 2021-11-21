using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    List<GameObject> clouds;
    [SerializeField] GameObject cloud_prefab;

    private void Awake()
    {
        clouds = new List<GameObject>();
    }
    public void CloudDectroy(GameObject cloud)
    {
        clouds.Remove(cloud);
        Destroy(cloud);
    }

    private void Update()
    {
        if (clouds.Count < 20)
        {
            Vector3 position = new Vector3(Random.Range(-250f, 500f), 100f, Random.Range(-250f, 500f));
            GameObject go = Instantiate(cloud_prefab, position, Quaternion.identity);
            go.GetComponent<Cloud>().spawner = this;
            clouds.Add(go);
        }
    }

}
