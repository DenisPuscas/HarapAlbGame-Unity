using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; set; }

    public Animator doorAnim;

    public GameObject chestOpen;
    public GameObject chestClose;

    public GameObject oldWomenNPC;

    public GameObject treeBorder1;
    public GameObject treeBorder2;

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

    void Start()
    {
        chestOpen.SetActive(false);
        treeBorder2.SetActive(false);
    }

    public void OpenDoor()
    {
        doorAnim.SetBool("DoorOpen", true);

    }

    public void OpenChest()
    {
        chestClose.SetActive(false);
        chestOpen.SetActive(true);
    }

    public void MoveTreeBorder()
    {
        treeBorder1.SetActive(false);
        treeBorder2.SetActive(true);
    }
}
