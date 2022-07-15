using System;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest curQuest;
    private GameObject player;
    
    [SerializeField] private Transform goalContent;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private TMP_Text titleText, descriptionText, experienceText, goldText;

    private void Awake()
    {
        titleText.text = curQuest.title;
        descriptionText.text = curQuest.description;

        //Go through all tasks to see if any have been completed and update UI if necessary
        foreach(var goal in curQuest.goals)
        {
            GameObject goalObject = Instantiate(goalPrefab, goalContent);
			
            goalObject.transform.Find("Text").GetComponentInChildren<TMP_Text>().text = curQuest.description;
			
            GameObject countObject = goalObject.transform.Find("Count").gameObject;

            if(goal.isReached)
            {
                countObject.SetActive(false);
                goalObject.transform.Find("Done").gameObject.SetActive(true);
                Debug.Log("Quest Complete!");
                CloseWindow();
            }
            else
            {
                countObject.GetComponentInChildren<TMP_Text>().text = goal.currentAmount + "/" + goal.requiredAmount;
            }
        }
        experienceText.text = curQuest.experienceReward.ToString();
        goldText.text = curQuest.goldReward.ToString();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);

        for(int i = 0; i < goalContent.childCount; i++)
        {
            Destroy(goalContent.GetChild(i).gameObject);
        }
    }
}
