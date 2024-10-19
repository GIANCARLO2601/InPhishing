using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    [SerializeField] private string idLlaveNecesaria;  // ID de la llave necesaria
    [SerializeField] private GameObject mensajeLlaveNecesaria;  // Mensaje mostrado si no tiene la llave
    private BoxCollider2D boxCollider;  // Referencia al BoxCollider2D
    private bool jugadorEnRango = false;  // Controla si el jugador está cerca de la puerta

    private void Awake()
    {
        // Obtenemos el BoxCollider2D del objeto
        boxCollider = GetComponent<BoxCollider2D>();

        // Verificamos que el BoxCollider2D esté presente
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D no encontrado en el objeto.");
        }
    }

    private void Update()
    {
        // Si el jugador está en rango y presiona la tecla M
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.M))
        {
            if (JugadorTieneLlave())  // Verificamos si tiene la llave
            {
                Debug.Log("Jugador tiene la llave y presionó M. Se permite el paso.");
                EliminarLlave();  // Eliminamos la llave del inventario
                DesactivarCollider();  // Desactivamos el collider de la puerta
            }
            else
            {
                Debug.Log("Jugador no tiene la llave. No se permite el paso.");
                MostrarMensajeLlave();  // Mostramos el mensaje indicando que necesita la llave
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mensajeLlaveNecesaria.SetActive(true);
            jugadorEnRango = true;  // Marcamos que el jugador está cerca de la puerta
            Debug.Log("Jugador en el área de la puerta.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mensajeLlaveNecesaria.SetActive(false);
            jugadorEnRango = false;  // El jugador se alejó de la puerta
            Debug.Log("Jugador ha salido del área de la puerta.");
        }
    }

    private bool JugadorTieneLlave()
    {
        // Verificamos si el inventario del jugador contiene la llave necesaria
        foreach (var item in Inventario.Instance.ItemsInventario)
        {
            if (item != null && item.ID == idLlaveNecesaria)
            {
                return true;  // El jugador tiene la llave
            }
        }
        return false;  // El jugador no tiene la llave
    }

    private void EliminarLlave()
    {
        // Usamos la función QuitarItem del Inventario para eliminar la llave
        Inventario.Instance.QuitarItem(idLlaveNecesaria);
        Debug.Log("Llave eliminada del inventario.");
    }

    private void DesactivarCollider()
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = false;  // Desactivamos el collider para permitir el paso
            Debug.Log("BoxCollider desactivado. El jugador puede pasar.");
        }
    }

    private void MostrarMensajeLlave()
    {
        if (mensajeLlaveNecesaria != null)
        {
            mensajeLlaveNecesaria.SetActive(true);  // Mostramos el mensaje
            StartCoroutine(EsconderMensajeDespuesDeTiempo(2f));  // Escondemos el mensaje después de 2 segundos
        }
    }

    private IEnumerator EsconderMensajeDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        mensajeLlaveNecesaria.SetActive(false);  // Escondemos el mensaje
    }
}
