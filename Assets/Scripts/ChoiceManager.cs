using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public List<GameObject> PickupOptions;
    public List<GameObject> selectedOptions;
    public List<Transform> SpawnOptions;
    public int optionCount;
    public List<GameObject> choices;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewPickups();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNewPickups()
    {
       for(int i = 0; i < optionCount; i++)
        {
            int RandomIndex = Random.Range(0, PickupOptions.Count);
            GameObject randomlySelectedPickupPrefab = PickupOptions[RandomIndex];
            selectedOptions.Add(randomlySelectedPickupPrefab);
            PickupOptions.Remove(randomlySelectedPickupPrefab);
            GameObject pickupInstance = Instantiate(randomlySelectedPickupPrefab, SpawnOptions[i].position,Quaternion.identity);
            choices.Add(pickupInstance);
            pickupInstance.GetComponent<Collectable>().onCollect.AddListener(ConcludeChoice);
        }
       foreach(GameObject g in selectedOptions)
        {
            PickupOptions.Add(g);
        }
        selectedOptions.Clear();
    }
    void ConcludeChoice(GameObject g)
    {
        foreach(GameObject choice in choices)
        {
            if(g!=choice)
            Destroy(choice);
        }
    }
}
