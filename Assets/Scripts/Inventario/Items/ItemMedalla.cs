using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Medalla")]
public class ItemMedalla : InventarioItem
{
    [Header("Medalla Info")]
    public string TipoMedalla; // Puedes agregar información específica de la medalla
}