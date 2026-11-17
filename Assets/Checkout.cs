using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkout : MonoBehaviour
{
    public TextMeshProUGUI textoNombres;
    public TextMeshProUGUI textoPrecios;

    public float precioTotalActual { get; private set; }

    private List<ItemWorld> itemsEnCaja = new List<ItemWorld>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemWorld>(out ItemWorld item))
        {
            if (!itemsEnCaja.Contains(item))
            {
                itemsEnCaja.Add(item);
                GenerarTicket();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemWorld>(out ItemWorld item))
        {
            if (itemsEnCaja.Contains(item))
            {
                itemsEnCaja.Remove(item);
                GenerarTicket();
            }
        }
    }

    public void GenerarTicket()
    {
        if (textoNombres == null || textoPrecios == null) return;

        if (itemsEnCaja.Count == 0)
        {
            precioTotalActual = 0f;
            textoNombres.text = "CAJA VACÍA";
            textoPrecios.text = "$0.00";
            return;
        }

        string columnaNombres = "ARTÍCULO\n\n";
        string columnaPrecios = "PRECIO\n\n";
        precioTotalActual = 0f;

        foreach (ItemWorld item in itemsEnCaja)
        {
            if (item.Data != null)
            {
                columnaNombres += $"{item.Data.itemName}\n";
                columnaPrecios += $"${item.Data.itemPrice:F2}\n"; 
                precioTotalActual += item.Data.itemPrice;
            }
        }

        columnaNombres += "\n---------------------\nTOTAL:";
        columnaPrecios += $"\n---------------------\n${precioTotalActual:F2}";

        textoNombres.text = columnaNombres;
        textoPrecios.text = columnaPrecios;
    }

    public void ProcesarCompra()
{

    if (itemsEnCaja.Count == 0) return;


    foreach (ItemWorld item in itemsEnCaja)
    {
        if (item != null)
        {
           
        }
    }


    string columnaNombres = " COMPRADO\n\n";
    string columnaPrecios = "PAGADO\n\n";
    float precioTotal = 0f;

    foreach (ItemWorld item in itemsEnCaja)
    {
        if (item.Data != null)
        {
            columnaNombres += $"{item.Data.itemName}\n";
            columnaPrecios += $"${item.Data.itemPrice:F2}\n"; 
            precioTotal += item.Data.itemPrice;
        }
    }

    columnaNombres += "\n---------------------\nTOTAL PAGADO:";
    columnaPrecios += $"\n---------------------\n${precioTotal:F2}";


    textoNombres.text = columnaNombres;
    textoPrecios.text = columnaPrecios;


    itemsEnCaja.Clear();
    precioTotalActual = 0f; 
}
    
}