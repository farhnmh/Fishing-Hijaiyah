using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHandler : MonoBehaviour
{
    public GameManager gameManager;
    public FishingRodHandler fishingRodHandler;
    public GameObject starAnimPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);
            gameManager.point -= 100;
            fishingRodHandler.isGot = false;
        }

        if (collision.CompareTag("Hijaiyah"))
        {
            var star = Instantiate(starAnimPrefab);
            star.transform.position = collision.transform.position;
            star.transform.parent = gameObject.transform;
            StartCoroutine(DestroyStars());

            Destroy(collision.gameObject);
            gameManager.point += 100;
            fishingRodHandler.isGot = false;
        }
    }

    IEnumerator DestroyStars()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (Transform child in gameObject.transform)
            Destroy(child.gameObject);
    }
}
