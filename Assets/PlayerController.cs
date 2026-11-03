using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Transform cameraTransform;
    public float mouseSensitivity = 60.0f;
    public float movementSpeed = 8.0f;
    public float verticalRotation = 0f;
    public float horizontalRotation = 0f;
    public Transform playerHands;
    public float rayDistance = 5f;

    public Transform GrabbedTransform;


    public void Awake()
    {

        cameraTransform = GetComponentInChildren<Camera>().transform;

    }


    public void Update()
    {
        Movement();
        CameraLook();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance))
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!GrabbedTransform)
                {
                    if (hit.transform.GetComponent<ItemData>())
                    {

                        GrabTransform(hit.transform);
                    }
                }
                else
                {

                }
            }
            //print(hit.transform.gameObject.name);
        }
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * rayDistance, Color.cyan);


    }
    public void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 0.1f;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(verticalRotation, -85, 85);


        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(moveDirection);
    }
    private void GrabTransform(Transform TransformToGrab)
    {
        GrabbedTransform = TransformToGrab;
        GrabbedTransform.SetParent(playerHands);
        GrabbedTransform.localPosition = Vector3.zero;
        GrabbedTransform.GetComponent<Rigidbody>().isKinematic = true;
    }




}