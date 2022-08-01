using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectHandler : MonoBehaviour
{
    [Header("Object Attribute")]
    public bool isFish;
    public bool isOnRight;
    public float[] moveSpeeds;
    public GameObject fishingHook;

    float moveSpeed;

    void Start()
    {
        moveSpeed = Random.RandomRange(moveSpeeds[0], moveSpeeds[1]);    
    }

    void Update()
    {
        if (isOnRight)
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        else if (!isOnRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (isFish)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (fishingHook != null)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            transform.position = fishingHook.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook") &&
            !collision.transform.parent.GetComponent<FishingRodHandler>().isGot)
        {
            collision.transform.parent.GetComponent<FishingRodHandler>().isGot = true;
            collision.transform.parent.GetComponent<FishingRodHandler>().isHook = false;
            fishingHook = collision.gameObject;
        }

        if (collision.CompareTag("Boundary"))
            Destroy(gameObject);
    }
}
