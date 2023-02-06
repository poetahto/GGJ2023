using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public GameObject DoorCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OpenDoor()
    {
        Destroy(DoorCollider);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            TransitionManager.instance.DoorEntered();
            print("door entered");
        }
    }
}
