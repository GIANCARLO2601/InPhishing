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
    private void Start(){
        itemsInventario= new InventarioItem[numeroDeSlots];
    }
    public void AñadirItem(InventarioItem itemPorAñadir,int cantidad)
    {
        if(itemPorAñadir==null)
        {
            return;
        }
        List<int> indexes=VerificarExistencias(itemPorAñadir.ID);
        if(itemPorAñadir.EsAcumulable){
            if(indexes.Count>0){
                for(int i=0;i<indexes.Count;i++)
                {
                    if(itemsInventario[indexes[i]].Cantidad<itemPorAñadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad+=cantidad;
                        if(itemsInventario[indexes[i]].Cantidad>itemPorAñadir.AcumulacionMax)
                        {
                            int diferencia= itemsInventario[indexes[i]].Cantidad-itemPorAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad=itemPorAñadir.AcumulacionMax;
                            AñadirItem(itemPorAñadir,diferencia);
                        }
                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir,
                        itemsInventario[indexes[i]].Cantidad,indexes[i]);
                        return;
                        
                    }
                }
            }
        }

        if(cantidad<=0){
            return;
        }
        if(cantidad>itemPorAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemPorAñadir,itemPorAñadir.AcumulacionMax);
            cantidad-=itemPorAñadir.AcumulacionMax;
            AñadirItem(itemPorAñadir,cantidad);

        }
        else{
            AñadirItemEnSlotDisponible(itemPorAñadir,cantidad);
        }

    }
    private List<int> VerificarExistencias(String itemID)
    {
        List<int> indexesDeItem=new List<int>();
        for(int i=0;i<itemsInventario.Length;i++){
            if(itemsInventario[i]!=null)
            {
                if(itemsInventario[i].ID==itemID)
                {
                indexesDeItem.Add(i);
                }
            }
            
        }
        return indexesDeItem;
    }
    private void AñadirItemEnSlotDisponible(InventarioItem item,int cantidad)
    {
        for(int i=0;i<itemsInventario.Length;i++)
        {
            if(itemsInventario[i]==null)
            {
                itemsInventario[i]=item.CopiarItem();
                itemsInventario[i].Cantidad=cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item,cantidad,i);
                return;
            }
        }
    }
}
