using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This works! Yay!
/// </summary>
public class WaypointMarker : MonoBehaviour
{
    public RectTransform prefab; //Canvas rect transform
    
    private RectTransform waypoint; //The UI that will be instantiated 
    private Transform player;
    private TMP_Text distanceText;
    private Vector3 offset = new Vector3(0, 1.25f, 0);

    private void Awake()
    {
        var canvas = GameObject.Find("Dumpling Task UI").transform;

        //Instantiate waypoint UI prefab at the position of the canvas
        waypoint = Instantiate(prefab, canvas);
        
        //Get the text element from the waypoint UI prefab
        distanceText = waypoint.GetComponentInChildren<TMP_Text>();

        player = GameObject.Find("Dumpling").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        waypoint.position = screenPosition;

        waypoint.gameObject.SetActive(screenPosition.z > 0); //Disappears if behind player

        distanceText.text = Vector3.Distance(player.position, position).ToString("0.0") + "m";
    }
}
