using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    [SerializeField] private string idLlaveNecesaria; // ID de la llave necesaria
    [SerializeField] private GameObject mensajeLlaveNecesaria; // Mensaje que se mostrará si no tiene la llave
    private BoxCollider2D boxCollider; // Referencia al BoxCollider2D

    private void Awake()
    {
        // Obtenemos el BoxCollider2D del propio objeto
        boxCollider = GetComponent<BoxCollider2D>();

        // Verificamos que el BoxCollider2D esté presente
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D no encontrado en el objeto.");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Validamos si el jugador tiene la llave necesaria
            if (JugadorTieneLlave())
            {
                Debug.Log("Jugador tiene la llave. Se permite el paso.");
                DesactivarCollider(); // Desactivamos el BoxCollider2D
            }
            else
            {
                Debug.Log("Jugador no tiene la llave. No se permite el paso.");
                MostrarMensajeLlave(); // Mostramos el mensaje indicando que necesita la llave
            }
        }
    }

    private bool JugadorTieneLlave()
    {
        // Verificar si el inventario del jugador contiene la llave necesaria
        foreach (var item in Inventario.Instance.ItemsInventario)
        {
            if (item != null && item.ID == idLlaveNecesaria)
            {
                return true; // El jugador tiene la llave
            }
        }
        return false; // El jugador no tiene la llave
    }

    private void DesactivarCollider()
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = false; // Desactivamos el collider
            Debug.Log("BoxCollider desactivado. El jugador puede pasar.");
        }
    }

    private void MostrarMensajeLlave()
    {
        if (mensajeLlaveNecesaria != null)
        {
            mensajeLlaveNecesaria.SetActive(true); // Mostramos el mensaje
            StartCoroutine(EsconderMensajeDespuesDeTiempo(2f)); // Escondemos el mensaje después de 2 segundos
        }
    }

    private IEnumerator EsconderMensajeDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        mensajeLlaveNecesaria.SetActive(false); // Escondemos el mensaje
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador ha salido del área del boss.");
        }
    }
}
