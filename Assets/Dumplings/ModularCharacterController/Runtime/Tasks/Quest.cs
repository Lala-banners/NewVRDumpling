using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Quest")]
public class Quest : ScriptableObject
{
    [Header("Quest Requirements")]
    public string title;
    public string description;
    public List<QuestGoal> goals;

    [Header("Rewards")]
    public int experienceReward;
    public int goldReward;

}
