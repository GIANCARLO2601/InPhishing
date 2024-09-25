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
    

    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    private void Start()
    {
        
        InicializarInventario();
    }

    private void InicializarInventario()
    {
        
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index=i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }
    public void DibujarItemEnInventario(InventarioItem itemPorAñadir,int cantidad, int itemIndex){
        InventarioSlot slot=slotsDisponibles[itemIndex];
        if(itemPorAñadir!=null){
            slot.ActivarSlotUI(true);
            slot.ActualizarSlotUI(itemPorAñadir,cantidad);
        }
        else{
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarInventarioDescipcion(int index){
        if (Inventario.Instance.ItemsInventario[index]!=null)
        {
            itemIcono.sprite=Inventario.Instance.ItemsInventario[index].Icono;
            
            itemNombre.text=Inventario.Instance.ItemsInventario[index].Nombre;
            
            itemDescripcion.text=Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);

        }
        else{
                        panelInventarioDescripcion.SetActive(false);

        }
    }
    public void SlotInteracionRespuesta(TipoDeInteraccion tipo,int index){
        if(tipo==TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescipcion(index);
        }
    }
    public void OnEnable()
    {
        InventarioSlot.EventosSlotInteracciones +=SlotInteracionRespuesta;
    }
    public void OnDisable(){
        
        InventarioSlot.EventosSlotInteracciones -=SlotInteracionRespuesta;
    }
}
