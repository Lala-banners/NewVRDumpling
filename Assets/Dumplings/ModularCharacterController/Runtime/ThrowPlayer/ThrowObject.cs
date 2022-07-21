using UnityEngine;
namespace Dumplings.ModularCharacterController.Runtime.ThrowPlayer
{
    public class ThrowObject : MonoBehaviour
    {
        [SerializeField] private Vector3 ThrowForce = Vector3.zero;
        void OnCollisionEnter(Collision collision)
        {
            if(!collision.transform.CompareTag("Player"))
                return;
                
            if(collision.rigidbody == null)
                return;
 
            collision.rigidbody.AddForce(ThrowForce);
            
            print("Throw");
        }
    }
}
