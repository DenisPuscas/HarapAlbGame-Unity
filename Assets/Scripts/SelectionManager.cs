using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public GameObject interaction_info_UI;
    public string onTarget;
    Text interaction_text;

    public Image centerDotIcon;
    public Image handIcon;

    public bool canTakeTheItem;

    private void Start()
    {
        interaction_text = interaction_info_UI.GetComponent<Text>();
        onTarget = "";
        canTakeTheItem = false;
    }

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

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.2f))
        {
            InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
            NPC npc = hit.transform.GetComponent<NPC>();

            if (npc)
            {
                interaction_text.text = "Talk";
                interaction_info_UI.SetActive(true);

                if (Input.GetMouseButtonDown(0) && !npc.isTalking)
                {
                    npc.StartConversation();
                }

                if (npc.isTalking)
                {
                    interaction_info_UI.SetActive(false);
                    centerDotIcon.gameObject.SetActive(false);
                }
            } 
            else if (interactableObject)
            {
                interaction_text.text = interactableObject.GetItemName();
                interaction_info_UI.SetActive(true);
                onTarget = interactableObject.GetItemName();

                if (interactableObject.CompareTag("Pickable"))
                {
                    centerDotIcon.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                    canTakeTheItem = true;
                }
                else
                {
                    centerDotIcon.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);
                    canTakeTheItem = false;
                }
            }
            else
            {
                interaction_info_UI.SetActive(false);
                onTarget = "";

                centerDotIcon.gameObject.SetActive(true);
                handIcon.gameObject.SetActive(false);
                canTakeTheItem = false;
            }
        }
        else
        {
            interaction_info_UI.SetActive(false);
            onTarget = "";

            centerDotIcon.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false);
            canTakeTheItem = false;
        }


    }
}
