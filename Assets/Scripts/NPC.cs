using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Animator anim;
    public bool isTalking;

    public TextMeshProUGUI dialogText, option1Text, option2Text;

    public Button option1Btn, option2Btn;

    public List<Quest> quests;
    private Quest currentQuest = null;
    private int questIndex = 0;
    private bool allQuestsCompleted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        dialogText = DialogSystem.Instance.dialogText;
        option1Btn = DialogSystem.Instance.option1Btn;
        option1Text = DialogSystem.Instance.option1Btn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        option2Btn = DialogSystem.Instance.option2Btn;
        option2Text = DialogSystem.Instance.option2Btn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        
    }

    public void StartConversation()
    {
        isTalking = true;

        if (!allQuestsCompleted)
        {
            currentQuest = quests[questIndex];
            StartQuestDialog();
        }
        else
        {
            FinalQuestDialog();
            if (gameObject.name == "Spanul")
            {
                anim.SetTrigger("pointing");
            }
        }
    }

    private void StartQuestDialog()
    {
        DialogSystem.Instance.OpenDialogUI();

        dialogText.text = currentQuest.info.dialog;
        option1Text.text = currentQuest.info.acceptOption;
        option2Text.text = currentQuest.info.declineOption;
        option1Btn.onClick.RemoveAllListeners();

        if (option1Text)
        {
            option1Btn.onClick.AddListener(() =>
            {
                ReceiveRewardAndCompleteQuest();
            });
        }
        
        option2Btn.onClick.RemoveAllListeners();
        option2Btn.onClick.AddListener(() =>
        {
            DialogSystem.Instance.CloseDialogUI();
            isTalking = false;
        });

        if (AreQuestRequirmentsCompleted())
        {
            option1Btn.interactable = true;
        }
        else
        {
            option1Btn.interactable = false;
        }

        if (gameObject.name == "OldWomen")
        {
            if (questIndex == 0)
            {
                anim.SetTrigger("asking");
            }
            else
            {
                anim.SetTrigger("talking");
            }
        }
    }

    private void ReceiveRewardAndCompleteQuest()
    {
        currentQuest.isCompleted = true;

        if (currentQuest.info.requirmentItem != "")
        {
            EquipSystem.Instance.RemoveFromSlot(currentQuest.info.requirmentItem);
        }

        if (currentQuest.info.rewardItem1 != "")
        {
            EquipSystem.Instance.AddToSlots(currentQuest.info.rewardItem1);
        }

        if (currentQuest.info.rewardItem2 != "")
        {
            EquipSystem.Instance.AddToSlots(currentQuest.info.rewardItem2);
        }

        questIndex++;

        if (questIndex < quests.Count)
        {
            currentQuest = quests[questIndex];
            StartQuestDialog();
        }
        else
        {
            DialogSystem.Instance.CloseDialogUI();
            allQuestsCompleted = true;

            if (gameObject.name == "GreenKing")
            {
                ObjectManager.Instance.OpenDoor();
                ObjectManager.Instance.oldWomenNPC.SetActive(false);
            }
            else if (gameObject.name == "Spanul")
            {
                ObjectManager.Instance.MoveTreeBorder();
                anim.SetTrigger("pointing");
            }

            FinalQuestDialog();
        }
    }

    private bool AreQuestRequirmentsCompleted()
    {
        string requiredItem = currentQuest.info.requirmentItem;

        if (requiredItem == "")
        {
            return true;
        }

        foreach (string item in EquipSystem.Instance.itemList)
        {
            if (item == requiredItem)
            {
                return true;
            }
        }

        return false;
    }

    private void FinalQuestDialog()
    {
        DialogSystem.Instance.OpenDialogUI();

        dialogText.text = currentQuest.info.finalWords;
        option1Text.text = "";
        option2Text.text = currentQuest.info.declineOption;
        option1Btn.interactable = false;
        option2Btn.onClick.RemoveAllListeners();
        option2Btn.onClick.AddListener(() =>
        {
            DialogSystem.Instance.CloseDialogUI();
            isTalking = false;
        });
    }
}
