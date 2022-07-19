using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//
//For handling local objects and sending data over the network
//
namespace Networking.Pun2
{
    public class AsymmetricManager : MonoBehaviourPun
    {
        //VR STUFF
        [SerializeField] GameObject VRheadPrefab;
        [SerializeField] GameObject VRhandRPrefab;
        [SerializeField] GameObject VRhandLPrefab;
        [SerializeField] GameObject ovrCameraRig;
        // DESKTOP Stuff
        [SerializeField] GameObject desktopCameras;
        // Ethan is the name of the third person character
        [SerializeField] GameObject ethanPunPrefab;
        [SerializeField] Transform ethanSpawnPos;
        [SerializeField] UnityStandardAssets.Cameras.FreeLookCam ethanCamera;
        bool vrMode;

        private void Awake()
        {
            /// If the game starts in Room scene, and is not connected, sends the player back to Lobby scene to connect first.
            if (!PhotonNetwork.NetworkingClient.IsConnected)
            {
                SceneManager.LoadScene("Photon2Lobby");
                return;
            }

            //Detects if running through a headset or not
            if (OVRPlugin.GetSystemHeadsetType() != OVRPlugin.SystemHeadset.None)
            {
                vrMode = true;
                Debug.Log("Headset detected, enabling vr mode");
            }
            else Debug.Log("Headset not detected, enabling desktop mode");
        }

        private void Start()
        {
            if (!vrMode)
            {
                CreateDesktopBody();
            }
            else
            {
                CreateVRBody();
            }

        }

        void CreateVRBody()
        {
            ovrCameraRig.SetActive(true);
            //Instantiate Head
            GameObject obj = (PhotonNetwork.Instantiate(VRheadPrefab.name, OculusPlayer.instance.head.transform.position, OculusPlayer.instance.head.transform.rotation, 0));

            //Instantiate right hand
            obj = (PhotonNetwork.Instantiate(VRhandRPrefab.name, OculusPlayer.instance.rightHand.transform.position, OculusPlayer.instance.rightHand.transform.rotation, 0));

            //Instantiate left hand
            obj = (PhotonNetwork.Instantiate(VRhandLPrefab.name, OculusPlayer.instance.leftHand.transform.position, OculusPlayer.instance.leftHand.transform.rotation, 0));
        }

        void CreateDesktopBody()
        {
            //Create the third person controller character, and set it's properties
            desktopCameras.SetActive(true);
            GameObject obj = PhotonNetwork.Instantiate(ethanPunPrefab.name, ethanSpawnPos.position, ethanSpawnPos.rotation);
            obj.GetComponent<CapsuleCollider>().enabled = true;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            ethanCamera.SetTarget(obj.transform);
        }
    }
}
