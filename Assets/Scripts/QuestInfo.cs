using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Scriptable Objects/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [TextArea(5, 10)]
    public string dialog;

    [Header("Options")]
    [TextArea(5, 10)]
    public string acceptOption;
    [TextArea(5, 10)]
    public string declineOption;
    [TextArea(5, 10)]
    public string finalWords;

    [Header("Rewards")]
    public string rewardItem1;
    public string rewardItem2;

    [Header("Requirements")]
    public string requirmentItem;
}
