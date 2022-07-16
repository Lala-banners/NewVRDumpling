using Photon.Pun;
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
        [SerializeField] GameObject ovrCameraRig;
        // DESKTOP Stuff
        private GameObject desktopCameras;
        // Ethan is the name of the third person character
        [SerializeField] GameObject ethanPunPrefab;
        [SerializeField] Transform ethanSpawnPos;
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
                Debug.Log("Oops no VR person!");
            }
            else
            {
                ovrCameraRig.SetActive(true);
            }

        }

        void CreateDesktopBody()
        {
            //Create the third person controller character, and set it's properties
            GameObject obj = PhotonNetwork.Instantiate(ethanPunPrefab.name, ethanSpawnPos.position, ethanSpawnPos.rotation);
            desktopCameras = GameObject.Find("3rd Person Camera");
            desktopCameras.SetActive(true);
            obj.GetComponent<CapsuleCollider>().enabled = true;
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
