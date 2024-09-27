using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Llave")]
public class ItemLlave : InventarioItem
{
    [Header("Llave Info")]
    public string TipoLlave; // Si quieres especificar el tipo de llave
}
