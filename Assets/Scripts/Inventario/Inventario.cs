using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventario : Singleton<Inventario>
{
    [SerializeField] private int numeroDeSlots;
    public int NumeroDeSlots => numeroDeSlots;

    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    public void AñadirItem(InventarioItem itemPorAñadir, int cantidad)
    {
        if (itemPorAñadir == null) return;

        List<int> indexes = VerificarExistencias(itemPorAñadir.ID);

        if (itemPorAñadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAñadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;

                        if (itemsInventario[indexes[i]].Cantidad > itemPorAñadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorAñadir.AcumulacionMax;
                            AñadirItem(itemPorAñadir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(
                            itemPorAñadir, itemsInventario[indexes[i]].Cantidad, indexes[i]);

                        return;
                    }
                }
            }
        }

        if (cantidad <= 0) return;

        if (cantidad > itemPorAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax);
            cantidad -= itemPorAñadir.AcumulacionMax;
            AñadirItem(itemPorAñadir, cantidad);
        }
        else
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad);
        }
    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesDeItem = new List<int>();

        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null && itemsInventario[i].ID == itemID)
            {
                indexesDeItem.Add(i);
            }
        }

        return indexesDeItem;
    }

    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    // Nueva función para quitar un item del inventario
    public void QuitarItem(string itemID, int cantidad = 1)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null && itemsInventario[i].ID == itemID)
            {
                itemsInventario[i].Cantidad -= cantidad;

                if (itemsInventario[i].Cantidad <= 0)
                {
                    itemsInventario[i] = null;  // Eliminar el item si la cantidad llega a 0
                }

                InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[i], 0, i);
                Debug.Log($"Se ha quitado {cantidad} del item {itemID} del inventario.");
                return;
            }
        }

        Debug.LogWarning($"No se encontró el item con ID {itemID} en el inventario.");
    }
}
