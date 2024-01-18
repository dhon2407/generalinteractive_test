using Sirenix.OdinInspector;
using UnityEngine;

namespace Cinematics
{
    [HideMonoScript]
    public class CameraController : MonoBehaviour
    {
        public Vector3 CurrentPosition => transform.position;

        public void MoveTo(Vector3 position)
        {
            position.z = transform.position.z;
            transform.position = position;
        }
    }
}