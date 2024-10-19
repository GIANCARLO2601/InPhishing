using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    [SerializeField] private Button botonUsarItem; // Botón de usar ítem

    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();
    private int indexItemSeleccionado = -1; // Índice del ítem seleccionado

    private void Start()
    {
        InicializarInventario();
        botonUsarItem.onClick.AddListener(UsarItemSeleccionado); // Asignar función al botón
    }

    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];

        if (itemPorAñadir != null && cantidad > 0)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlotUI(itemPorAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false); // Desactivar el slot si la cantidad es 0
        }
    }

    private void ActualizarInventarioDescripcion(int index)
    {
        var item = Inventario.Instance.ItemsInventario[index];
        if (item != null && item.Cantidad > 0)
        {
            itemIcono.sprite = item.Icono;
            itemNombre.text = item.Nombre;
            itemDescripcion.text = item.Descripcion;
            panelInventarioDescripcion.SetActive(true);
            indexItemSeleccionado = index; // Guardar el índice del ítem seleccionado
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
            indexItemSeleccionado = -1;
        }
    }

    public void SlotInteracionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
        }
    }

    public void UsarItemSeleccionado()
    {
        if (indexItemSeleccionado < 0) return;

        var item = Inventario.Instance.ItemsInventario[indexItemSeleccionado];
        if (item == null || !item.EsConsumible) return;

        var personajeVida = FindObjectOfType<PersonajeVida>();

        if (item is ItemPocionVida pocionVida)
        {
            personajeVida.RestaurarSalud(pocionVida.HPrestauracion);
            Inventario.Instance.QuitarItem(item.ID); // Quitar 1 ítem del inventario
        }
        else if (item is ItemMegaVida megaVida)
        {
            personajeVida.RestaurarSalud(personajeVida.saludMax);
            Inventario.Instance.QuitarItem(item.ID); // Quitar 1 ítem del inventario
        }

        // Actualizar la UI del inventario después de usar un ítem
        var cantidadRestante = item.Cantidad;
        DibujarItemEnInventario(item, cantidadRestante, indexItemSeleccionado);
        ActualizarInventarioDescripcion(indexItemSeleccionado); // Actualizar descripción
    }

    private void OnEnable()
    {
        InventarioSlot.EventosSlotInteracciones += SlotInteracionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventosSlotInteracciones -= SlotInteracionRespuesta;
    }
}
