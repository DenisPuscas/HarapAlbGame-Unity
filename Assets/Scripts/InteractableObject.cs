using UnityEditor.Analytics;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;

    public string GetItemName()
    {
        return itemName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && SelectionManager.Instance.onTarget == itemName)
        {
            if (itemName == "Chest")
            {
                if (EquipSystem.Instance.itemList.Contains("Key"))
                {
                    EquipSystem.Instance.RemoveFromSlot("Key");
                    ObjectManager.Instance.OpenChest();
                }
            } 
            else
            {
                EquipSystem.Instance.AddToSlots(itemName);
                Destroy(gameObject);
            }
        }
    }

}
