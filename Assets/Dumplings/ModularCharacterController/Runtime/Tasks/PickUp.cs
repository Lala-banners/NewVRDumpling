using System;
using System.Linq;

using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GatherQuestGoal gather;
    private HideQuestGoal hide;
    public Transform hidingSpot1, hidingSpot2,hidingSpot3, hidingSpot4;
    public GameObject[] fuseObject;

    private const string 
        hidingspot1 = "Hiding Spot 1",
        hidingspot2 = "Hiding Spot 2",
        hidingspot3 = "Hiding Spot 3",
        hidingspot4 = "Hiding Spot 4";

    private void Start()
    {
        gather = FindObjectOfType<GatherQuestGoal>();
        hide = FindObjectOfType<HideQuestGoal>();
    }

    // Collect Fuse
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem item))
        {
            item.transform.SetParent(gameObject.transform);
            gather.ItemCollected(0);
        }
        
        DropFuse(other, 0, hidingSpot1, hidingspot1);
        DropFuse(other, 1, hidingSpot2, hidingspot2);
        DropFuse(other, 2, hidingSpot3, hidingspot3);
        DropFuse(other, 3, hidingSpot4, hidingspot4);
    }

    private void DropFuse(Collider other, int index, Transform parent, string name)
    {
        if (other.gameObject.name == name)
        {
            fuseObject[index].transform.SetParent(parent, false);
            fuseObject[index].transform.localPosition = new Vector3(0, 0, 0);
            fuseObject[index].layer = LayerMask.NameToLayer("ChefInteractable");
            hide.DropItem(0);
        }
    }
}
