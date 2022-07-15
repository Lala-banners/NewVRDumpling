using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnTest : MonoBehaviour
{
    public Realtime realtime;
    public Transform spawnPosition;

    public void SpawnCube(Realtime realtime)
    {
        //Instantiate the CubePlayer for this client once sucessfully connected to the room
        Realtime.Instantiate("CubePlayer", //Prefab name
            position: spawnPosition.position,       //Start 1 meter in the air
            rotation: Quaternion.identity,          //No rotation
            ownedByClient: false,                   //Make sure RealtimeView is not owned by this client
            preventOwnershipTakeover: false,        //Do not prevent other clients from calling RequestOwnership()
            useInstance: realtime);                 //Use this instance of Realtime that fired didConnectToRoom event
    }
}
