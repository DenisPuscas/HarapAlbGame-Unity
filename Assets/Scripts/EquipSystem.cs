using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    public GameObject inventory;

    public List<GameObject> slotsList = new();
    public List<string> itemList = new();

    public Sprite blackFrame;
    public Sprite whiteFrame;

    public GameObject weaponHolder;

    private GameObject selectedItem;
    private int selectedNumber;

    private GameObject selectedItemModel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        PopulateSlotList();
        selectedNumber = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(5);
        }
    }

    private void SelectSlot(int number)
    {
        foreach (GameObject slot in slotsList)
        {
            slot.GetComponent<Image>().sprite = blackFrame;
        }

        if (selectedNumber != number)
        {
            GameObject slot = slotsList[number - 1];
            slot.GetComponent<Image>().sprite = whiteFrame;

            if (selectedItemModel != null)
            {
                DestroyImmediate(selectedItemModel.gameObject);
                selectedItemModel = null;
            }

            if (slot.transform.childCount > 0)
            {
                selectedItem = slot.transform.GetChild(0).gameObject;
                if (selectedItem.CompareTag("Equipable"))
                {
                    EquipItem(selectedItem);
                }
            }

            selectedNumber = number;
        }
        else
        {
            selectedItem = null;
            selectedNumber = -1;

            if (selectedItemModel != null)
            {
                DestroyImmediate(selectedItemModel.gameObject);
                selectedItemModel = null;
            }
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventory.transform)
        {
            slotsList.Add(child.gameObject);
        }
    }

    public void AddToSlots(string itemName)
    {
        GameObject availableSlot = FindNextEmptySlot();
        GameObject itemToEquip = Instantiate(Resources.Load<GameObject>(itemName), availableSlot.transform.position, availableSlot.transform.rotation);
        itemToEquip.transform.SetParent(availableSlot.transform);
        itemList.Add(itemName);
    }

    public void RemoveFromSlot(string itemName)
    {
        foreach (GameObject slot in slotsList)
        {
            if (slot.transform.childCount > 0)
            {
                GameObject item = slot.transform.GetChild(0).gameObject;
                if (item.name.Replace("(Clone)", "") == itemName)
                {
                    Destroy(item);
                }
            }
        }
        itemList.Remove(itemName);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    private void EquipItem(GameObject item)
    {
        string itemName = item.name.Replace("(Clone)", "");
        print(itemName);
        selectedItemModel = Instantiate(Resources.Load<GameObject>(itemName + "_Model"), new Vector3(0.6f, 0, 0.6f), Quaternion.Euler(-60f, 0, 60f));
        selectedItemModel.transform.SetParent(weaponHolder.transform, false);
    }

}
