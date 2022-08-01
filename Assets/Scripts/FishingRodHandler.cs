using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FishingRodHandler : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Fishing Thread Attribute")]
    public GameObject fishingHook;
    public LineRenderer fishingThread;
    public Vector2 fishingThreadOffset;

    [Header("Hooking Object Attribute")]
    public bool isHook;
    public bool isGot;
    public Vector2 fishingHookDefaultPos;
    public float hookBoundary;
    public float hookSpeed;
    public float hookTime;
    Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        FishingThreadHandler();
        if (Input.GetMouseButtonDown(0) && 
            Input.mousePosition.y < hookBoundary &&
            fishingThreadOffset == fishingHookDefaultPos &&
            !gameManager.isOver)
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            isHook = true;
        }
        else if (isHook)
        {
            fishingThreadOffset = Vector2.MoveTowards(fishingThreadOffset, Camera.main.ScreenToWorldPoint(mousePos), hookSpeed * Time.deltaTime);
            if (fishingThreadOffset == (Vector2)Camera.main.ScreenToWorldPoint(mousePos))
                isHook = false;
        }
        else if (!isHook)
            fishingThreadOffset = Vector2.MoveTowards(fishingThreadOffset, fishingHookDefaultPos, hookSpeed * Time.deltaTime);
    }

    void FishingThreadHandler()
    {
        fishingThread.SetPosition(0, fishingThread.gameObject.transform.position);
        fishingThread.SetPosition(1, fishingThreadOffset);
        fishingHook.transform.position = fishingThreadOffset;
    }
}
