using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = 0.3f;
    public float gyroSensitivity = 1.5f;

    private Vector3 startPosition;
    private Vector3 velocity;

    void Start()
    {
        startPosition = transform.position;

#if UNITY_ANDROID || UNITY_IOS
        if (Gyroscope.current != null)
            InputSystem.EnableDevice(Gyroscope.current);
#endif
    }

    void Update()
    {
        if (Camera.main == null)
            return;

        Vector2 inputOffset = Vector2.zero;

#if UNITY_ANDROID || UNITY_IOS
        
        if (Gyroscope.current != null)
        {
            Vector3 rotation = Gyroscope.current.angularVelocity.ReadValue();
            inputOffset = new Vector2(rotation.y, rotation.x) * gyroSensitivity;
        }
#else
        
        if (Pointer.current != null)
        {
            Vector2 pointerPosition = Pointer.current.position.ReadValue();
            inputOffset = Camera.main.ScreenToViewportPoint(pointerPosition);
            inputOffset -= new Vector2(0.5f, 0.5f);
        }
#endif

        Vector3 targetPosition =
            startPosition + (Vector3)(inputOffset * offsetMultiplier);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
