using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion info")] // Esto agrega un encabezado en el Inspector
    public float HPrestauracion;
}
