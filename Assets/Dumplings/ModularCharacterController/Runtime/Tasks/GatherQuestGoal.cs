using UnityEngine;

[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
    //Requirements for Gather goal type
    public string itemName;

    public void ItemCollected(int id)
    {
        if (goalType == GoalType.Gather && id == itemId)
        {
            currentAmount++;
            
            if (currentAmount >= requiredAmount) //Added from Quest
            {
                isReached = true;
                questState = QuestState.Completed;
                DestroyQuest(this);
                Debug.Log("QUEST COMPLETE");
            }
        }
    }

    //Each quest will have different way of being completed
    public override bool isCompleted()
    {
        if (item == null)
        {
            return false;
        }

        currentAmount = item.Amount;
        
        if (item.Amount >= requiredAmount) //if amount is greater or equal to required amount then quest is completed
        {
            ItemCollected(itemId);
            print("Quest has been completed");
            return true;
        }

        return false;
    }
}
