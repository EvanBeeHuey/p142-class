using UnityEngine;

public class Player : MonoBehaviour
{
        Rigidbody rb;

        public GameObject playerCamera;

        private Vector3 direction;
        private float cameraRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
            rb = GetComponent<Rigidbody>();
            //playerCamera = GetComponentInChildren<Camera>();

            Cursor.lockState = CursorLockMode.Locked;
    }

        // Update is called once per frame
        void Update()
        {
            UpdateMouseLook();
            UpdateDirection();
            
            //float hInput = Input.GetAxis("Horizontal");
            //float vInput = Input.GetAxis("Vertical");

            rb.linearVelocity = new Vector3(direction.x * 10, rb.linearVelocity.y, direction.z * 10);
        }

        private void UpdateMouseLook()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up * x);

            cameraRotation = Mathf.Clamp(cameraRotation - y, -30f, 30f);
            if (playerCamera)
                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
        }

        private void UpdateDirection()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            direction = new Vector3(x, 0f, z).normalized;

            direction = transform.right * direction.x + transform.forward * direction.z;
        }
}
