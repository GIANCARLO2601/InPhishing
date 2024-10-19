using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisiones : MonoBehaviour
{
    [Header("Objetos específicos necesarios para obtener la llave")]
    [SerializeField] private GameObject[] objetosNecesarios; // Arreglo con los objetos específicos
    private HashSet<GameObject> objetosEncontrados = new HashSet<GameObject>(); // Almacena los objetos encontrados
    [SerializeField] private InventarioItem medallaItem;

    private bool llaveEntregada = false; // Controla si ya se entregó la llave
    private void OtorgarMedalla()
    {
        if (medallaItem != null)
        {
            Inventario.Instance.AñadirItem(medallaItem, 1); // Añadir la medalla al inventario
            Debug.Log("¡Medalla otorgada!");
        }
    }
    void Start()
    {
        // Verificar que se asignaron los 6 objetos en el Inspector
        if (objetosNecesarios.Length != 6)
        {
            Debug.LogError("Debes asignar exactamente 6 objetos en el Inspector.");
        }
        else
        {
            Debug.Log("Objetos necesarios para obtener la llave:");
            foreach (var objeto in objetosNecesarios)
            {
                Debug.Log($"- {objeto.name}");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objetoColisionado = collision.gameObject; // Obtener el objeto con el que colisionó

        // Verificar si el objeto está en el arreglo y no se ha registrado antes
        if (System.Array.Exists(objetosNecesarios, obj => obj == objetoColisionado) 
            && !objetosEncontrados.Contains(objetoColisionado))
        {
            objetosEncontrados.Add(objetoColisionado); // Agregar el objeto encontrado al conjunto
            Debug.Log($"Colisionaste con: {objetoColisionado.name}");

            VerificarObjetosEncontrados(); // Verificar si se encontraron los 6 objetos
        }
    }

    private void VerificarObjetosEncontrados()
    {
        if (objetosEncontrados.Count == objetosNecesarios.Length) // Si se encontraron todos los objetos
        {
            if (!llaveEntregada)
            {
                EntregarLlave(); // Entrega la llave
            }
        }
        else
        {
            // Mostrar los objetos que aún faltan por encontrar
            Debug.Log("Faltan los siguientes objetos para obtener la llave:");
            foreach (var objeto in objetosNecesarios)
            {
                if (!objetosEncontrados.Contains(objeto))
                {
                    Debug.Log($"- {objeto.name}");
                }
            }
        }
    }

    private void EntregarLlave()
    {
        llaveEntregada = true; // Marcar que la llave ha sido entregada
        OtorgarMedalla();
        Debug.Log("¡Felicidades! Has encontrado todos los objetos y se te ha entregado la llave.");
        // Aquí puedes agregar lógica adicional, como abrir puertas o avanzar de nivel
    }
}
