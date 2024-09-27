using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PisoBoss : MonoBehaviour
{
    [SerializeField] private string idMedallaNecesaria; // ID de la medalla necesaria
    [SerializeField] private GameObject mensajeMedallaNecesaria; // Mensaje que se mostrará si no tiene la medalla
    [SerializeField] private GameObject pisoadestruir;
    private CompositeCollider2D compositeCollider; // Referencia al CompositeCollider2D

    private void Awake()
    {
        // Obtenemos el CompositeCollider2D del objeto
        compositeCollider = GetComponent<CompositeCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Validamos si el jugador tiene la medalla necesaria
            if (JugadorTieneMedalla())
            {
                Debug.Log("Jugador tiene la medalla. Se permite el paso.");
                Destroy(pisoadestruir); // Apagamos o destruimos el piso
            }
            else
            {
                Debug.Log("Jugador no tiene la medalla. No se permite el paso.");
                MostrarMensajeMedalla(); // Mostramos el mensaje indicando que necesita la medalla
            }
        }
    }

    private bool JugadorTieneMedalla()
    {
        // Verificar si el inventario del jugador contiene la medalla necesaria
        foreach (var item in Inventario.Instance.ItemsInventario)
        {
            if (item != null && item.ID == idMedallaNecesaria)
            {
                return true; // El jugador tiene la medalla
            }
        }
        return false; // El jugador no tiene la medalla
    }

    

    private void MostrarMensajeMedalla()
    {
        if (mensajeMedallaNecesaria != null)
        {
            mensajeMedallaNecesaria.SetActive(true); // Mostramos el mensaje
            StartCoroutine(EsconderMensajeDespuesDeTiempo(2f)); // Escondemos el mensaje después de 2 segundos
        }
    }

    private IEnumerator EsconderMensajeDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        mensajeMedallaNecesaria.SetActive(false); // Escondemos el mensaje
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador ha salido del área del boss.");
            compositeCollider.enabled = true; // Restaurar el collider a su estado original si fue desactivado
        }
    }
}
