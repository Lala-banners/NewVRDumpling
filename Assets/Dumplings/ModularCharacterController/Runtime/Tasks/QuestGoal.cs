using UnityEngine;

public enum QuestState
{
    Active,
    Completed,
    Failed
}

public enum GoalType
{
    Gather,
    Hide
}

[System.Serializable]
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;
    public GoalType goalType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;

    [Header("Player")]
    public ItemData item;

    public abstract bool isCompleted();

    public void DestroyQuest(QuestGoal goal)
    {
        if(isReached)
        {
            Destroy(goal.gameObject);
        }
    }
    
    public void ItemCollected(int id)
    {
        if (goalType == GoalType.Gather && id == itemId)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount) //Added from Quest
            {
                isReached = true;
                questState = QuestState.Completed;
                Debug.Log("QUEST COMPLETE");
            }
        }
    }
}
