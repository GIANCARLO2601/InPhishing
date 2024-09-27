using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fantasmaMail : Singleton<fantasmaMail>
{
    private bool puedeCambiarEscena = false; // Para saber si el jugador puede interactuar con el fantasma
    [SerializeField] private GameObject npcButtonInteractuar;

    [Header("Items que puede soltar")]
    [SerializeField] private InventarioItem pocionVidaItem;
    [SerializeField] private InventarioItem megaVidaItem;
    [SerializeField] private InventarioItem medallaItem; // Agregar referencia para la medalla

    protected override void Awake()
    {
        base.Awake(); // Llama al método Awake del Singleton para inicializar la instancia
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión detectada con el Player. Puedes interactuar.");
            puedeCambiarEscena = true; // El jugador puede interactuar
            npcButtonInteractuar.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha salido del área del fantasma.");
            puedeCambiarEscena = false; // No puede interactuar
            npcButtonInteractuar.SetActive(false);
        }
    }

    private void Update()
    {
        if (puedeCambiarEscena && Input.GetKeyDown(KeyCode.E))
        {
            // Pausar la escena principal antes de cambiar
            Time.timeScale = 0; // Pausar el tiempo en la escena principal

            // Cambiar a la escena del correo de phishing
            Debug.Log("Cambiando a la escena EscenaMailFail...");
            SceneManager.LoadScene("EscenaMailFail", LoadSceneMode.Additive); // Cargar la nueva escena sin cerrar la principal
        }
    }

    // Método para destruir el objeto cuando se regrese de la escena y soltar un ítem
    public void DestruirObjetoYSoltarItem()
    {
        SoltarItemAleatorio(); // Método para soltar un ítem aleatorio
        OtorgarMedalla(); // Método para dar una medalla
        Destroy(gameObject); // Destruir el fantasma al regresar
        Debug.Log("Fantasma destruido y objetos soltados.");
    }

    // Método para soltar un ítem aleatorio
    private void SoltarItemAleatorio()
    {
        float probabilidad = Random.Range(0f, 100f); // Generar un número aleatorio entre 0 y 100

        InventarioItem itemASoltar;

        if (probabilidad <= 10f) // 10% de probabilidad para Mega Vida
        {
            itemASoltar = megaVidaItem;
            Debug.Log("¡Mega Vida soltada!");
        }
        else // 90% de probabilidad para Poción de Vida
        {
            itemASoltar = pocionVidaItem;
            Debug.Log("Poción de Vida soltada.");
        }

        // Añadir el ítem al inventario del jugador
        Inventario.Instance.AñadirItem(itemASoltar, 1);
    }

    // Método para otorgar una medalla al jugador
    private void OtorgarMedalla()
    {
        if (medallaItem != null)
        {
            Inventario.Instance.AñadirItem(medallaItem, 1); // Añadir la medalla al inventario
            Debug.Log("¡Medalla otorgada!");
        }
    }
}
