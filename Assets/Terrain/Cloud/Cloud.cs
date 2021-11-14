using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    [SerializeField] ParticleSystem partical;

    public CloudSpawner spawner;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 5f);

        float k = Random.Range(1f, 3f);

        var main = partical.main;
        main.startSize = 20 * k;
        main.duration = Random.Range(60f, 180f);

        var shape = partical.shape;
        shape.scale = new Vector3(k, k, k);

        partical.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));

        if (!partical.IsAlive(true))
        {
            spawner.CloudDectroy(gameObject);
        }
    }
}
