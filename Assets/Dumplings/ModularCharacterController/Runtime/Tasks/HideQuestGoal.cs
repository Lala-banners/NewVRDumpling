using UnityEngine;


[System.Serializable]
public class HideQuestGoal : QuestGoal
{
    //Requirements for Hide goal type
    public string itemName;

    public void DropItem(int id)
    {
        if (goalType == GoalType.Hide && id == itemId)
        {
            requiredAmount = 0;
            currentAmount--;
            
            if (currentAmount <= 0)
            {
                isReached = true;
                questState = QuestState.Completed;
                DestroyQuest(this);
                Debug.Log("Drop item completed");
            }
        }
    }
    
    public override bool isCompleted()
    {
        if (item == null)
        {
            return false;
        }

        currentAmount = item.Amount;
        if (item.Amount <= requiredAmount) //if amount is less than or equal to required amount then quest is completed
        {
            DropItem(itemId);
            print("You have hidden all the fuses");
            return true;
        }

        return false;
    }
}
