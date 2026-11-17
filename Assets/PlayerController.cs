using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Transform cameraTransform;
public float mouseSensitivity = 60.0f;
public float movementSpeed = 8.0f;
public float verticalRotation = 0f;
public float horizontalRotation = 0f;

public float Distancia;
public bool Taking;
public Transform HandPosition;
public Transform ObjectGrab;

[Header("UI del Jugador")]
public TextMeshProUGUI textoNombre;
public TextMeshProUGUI textoPrecio;
public TextMeshProUGUI TextoPresupuesto;

public TextMeshProUGUI DialogueWindow;
public NPC1 npctext1;
public NPC2 npctext2;
public NPC3 npctext3;
[Header("Sistema de Economía")]
public float Presupuesto = 300.0f;
public AudioManager audioDePartida;

public GameObject papel;
public void Awake()
{
    cameraTransform = GetComponentInChildren<Camera>().transform;
    ActualizarUI("", "");
    ActualizarDineroUI();
    
}

public void Update()
{
    Movement();
    CameraLook();

    if (Input.GetKeyDown(KeyCode.E))
    {
        if (Taking)
        {
            Soltar();
        }
        else
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, Distancia))
            {
                if (hit.transform.TryGetComponent<ItemWorld>(out ItemWorld item))
                {
                    GrabTransform(hit.transform, item);
                }
                else if (hit.transform.CompareTag("Caja Registradora"))
                {
                    Checkout checkoutZona = Object.FindAnyObjectByType<Checkout>();

                    if (checkoutZona != null)
                    {
                        IntentarPagar(checkoutZona);
                    }
                    else
                    {
                        Debug.LogError("No se encontró ningún script 'Brain' en la escena para procesar el cobro.");
                    }
                }

                if (hit.transform.CompareTag("NPC1"))
                {
                    SetText1();
                }

                if (hit.transform.CompareTag("NPC2"))
                {
                    SetText2();
                }

                if (hit.transform.CompareTag("NPC3"))
                {
                    SetText3();
                }
            }
        }
    }
   

    Debug.DrawRay(cameraTransform.position, cameraTransform.forward * Distancia, Color.cyan);
}

private void IntentarPagar(Checkout checkout)
{
    float costoTotal = checkout.precioTotalActual;

    if (costoTotal <= 0)
    {
        Debug.Log("La zona de escaneo está vacía. Nada que pagar.");
        return;
    }

    if (Presupuesto >= costoTotal)
    {
        Presupuesto -= costoTotal;
        ActualizarDineroUI();
        checkout.ProcesarCompra(); // Le ordena a la zona vaciar y limpiar la lista
        Debug.Log("¡Transacción completada exitosamente!");
        papel.SetActive(true);
        audioDePartida.Plata();
    }
    else
    {
        Debug.Log("Saldo insuficiente para pagar los productos en el sensor.");
    }
    
}

private void ActualizarDineroUI()
{
    if (TextoPresupuesto != null) TextoPresupuesto.text = $"Billetera: ${Presupuesto:F2}";
}

private void GrabTransform(Transform TransformToGrab, ItemWorld item)
{
    Taking = true;
    ObjectGrab = TransformToGrab;
    ObjectGrab.SetParent(HandPosition);
    ObjectGrab.localPosition = Vector3.zero;
    ObjectGrab.localRotation = Quaternion.identity;

    if (ObjectGrab.TryGetComponent<Rigidbody>(out Rigidbody rb))
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    if (item.Data != null)
    {
        ActualizarUI(item.Data.itemName, "{item.Data.itemPrice:F2}");
    }
    audioDePartida.Agarrar();
}

private void Soltar()
{
    if (ObjectGrab == null) return;

    ObjectGrab.SetParent(null);
    if (ObjectGrab.TryGetComponent<Rigidbody>(out Rigidbody rb))
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }

    ObjectGrab = null;
    Taking = false;
    ActualizarUI("", "");
}

private void ActualizarUI(string Nombre, string Precio)
{
    if (textoNombre != null) textoNombre.text = Nombre;
    if (textoPrecio != null) textoPrecio.text = Precio;
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

public void SetText1()
{
    DialogueWindow.text = npctext1.dialogodeempleado;
    audioDePartida.NPCS();
}

public void SetText2()
{
    DialogueWindow.text = npctext2.dialogoperron;
    audioDePartida.NPCS();
}

public void SetText3()
{
    DialogueWindow.text = npctext3.ultimodialogo;
    audioDePartida.NPCS();
}


}