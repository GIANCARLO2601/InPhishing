using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem invetarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario.Instance.AñadirItem(invetarioItemReferencia, cantidadPorAgregar);
            Destroy(gameObject);
        }
    }

}
