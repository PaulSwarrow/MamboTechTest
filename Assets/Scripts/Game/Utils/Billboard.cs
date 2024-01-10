using UnityEngine;

namespace Game.Utils
{
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            Transform cameraTransform = Camera.main.transform;
            transform.LookAt(cameraTransform);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
        
    }
}