using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventosSlotInteracciones;
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    // Start is called before the first frame update
    public int Index { get; set; }

    public void ActualizarSlotUI(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void ClickSlot()
    {
        EventosSlotInteracciones?.Invoke(TipoDeInteraccion.Click, Index);
    }
}
