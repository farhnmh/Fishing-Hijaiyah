using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnHandler : MonoBehaviour
{
    public float cooldownSpawn;
    public GameObject[] hijaiyah;
    public GameObject[] fish;
    public Transform[] spawners;
    public float[] randomBoundaries;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CooldownSpawn());
    }

    IEnumerator CooldownSpawn()
    {
        SpawnObject();
        yield return new WaitForSeconds(cooldownSpawn);
        StartCoroutine(CooldownSpawn());
    }

    void SpawnObject()
    {
        int randObjectType = Random.Range(0, 2);
        int randSpawner = Random.Range(0, spawners.Length);
        float randY = Random.Range(randomBoundaries[0], randomBoundaries[1]);

        GameObject obj;
        if (randObjectType == 0)
        {
            int randHijaiyah = Random.Range(0, hijaiyah.Length);
            obj = Instantiate(hijaiyah[randHijaiyah]);
        }
        else
        {
            int randFish = Random.Range(0, fish.Length);
            obj = Instantiate(fish[randFish]);
        }

        obj.transform.parent = gameObject.transform;
        obj.transform.position = new Vector2(spawners[randSpawner].position.x, randY);

        if (obj.transform.position.x > 0)
            obj.GetComponent<InteractableObjectHandler>().isOnRight = true;
        else
            obj.GetComponent<InteractableObjectHandler>().isOnRight = false;
    }
}
