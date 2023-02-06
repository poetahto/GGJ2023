using System.Collections;
using System.Collections.Generic;
using Collectables;
using Effects;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    public List<GameObject> PickupOptions;
    public List<GameObject> selectedOptions;
    public List<Transform> SpawnOptions;
    public int optionCount;
    public List<GameObject> choices;
    public DecisionItemFactory itemFactory;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            choices = new List<GameObject>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNewPickups()
    {
       for(int i = 0; i < optionCount; i++)
        {
            // int RandomIndex = Random.Range(0, PickupOptions.Count);
            // GameObject randomlySelectedPickupPrefab = PickupOptions[RandomIndex];
            var pickupInstance = itemFactory.Create();
            selectedOptions.Add(pickupInstance);
            PickupOptions.Remove(pickupInstance);
            // GameObject pickupInstance = Instantiate(randomlySelectedPickupPrefab, SpawnOptions[i].position,Quaternion.identity);
            pickupInstance.transform.SetPositionAndRotation(SpawnOptions[i].position, Quaternion.identity);
            DontDestroyOnLoad(pickupInstance);

            choices.Add(pickupInstance);
            print(pickupInstance.name);
            pickupInstance.GetComponent<Collectable>().onCollect.AddListener(ConcludeChoice);
        }
       foreach(GameObject g in selectedOptions)
        {
            PickupOptions.Add(g);
        }
        selectedOptions.Clear();
    }
    void ConcludeChoice(ItemCollectEvent data)
    {
        foreach(GameObject choice in choices)
        {
            choice.GetComponent<Collectable>().onCollect.RemoveListener(ConcludeChoice);
            
            if (data.item!=choice)
                Destroy(choice);
        }
        choices.Clear();
        TransitionManager.instance.encounterComplete = true;
        FindObjectOfType<DoorBehaviour>().OpenDoor();
    }
}
