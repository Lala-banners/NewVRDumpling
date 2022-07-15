using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task 
{
    [Header("Quest Requirements")]
    public string title;
    public string description;
    public QuestGoal goal;
    
    [Header("Rewards")] //To be changed
    public int experienceReward;
    public int goldReward;
}
