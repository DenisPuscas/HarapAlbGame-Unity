using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 1000f;

    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!MenuManager.Instance.isMenuOpen && !DialogSystem.Instance.dialogUIActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            YRotation += mouseX;
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }
}