using System.Collections;
using TMPro;
using UnityEngine;

public class ColisionConsejo : MonoBehaviour
{
    [Header("Referencias del UI")]
    [SerializeField] private GameObject panelConsejo;
    [SerializeField] private TextMeshProUGUI textConsejo;

    private bool jugadorEnRango = false;
    private bool consejoMostrado = false;

    void Start()
    {
        if (panelConsejo != null)
        {
            panelConsejo.SetActive(false);  // Desactiva el panel al inicio
        }
        else
        {
            Debug.LogError("PanelConsejo no asignado en el Inspector.");
        }

        if (AsignarConsejo.Instance == null)
        {
            Debug.LogError("No se encontró la instancia de AsignarConsejo.");
        }
    }

    void Update()
    {
        if (jugadorEnRango && !consejoMostrado)
        {
            Debug.Log("mostrarConsejo");
            MostrarPanelConsejo();
        }
    }

    private void MostrarPanelConsejo()
    {
        if (AsignarConsejo.Instance != null && panelConsejo != null)
        {
            panelConsejo.SetActive(true);
            AsignarConsejo.Instance.AsignarConsejoAleatorio();  // Usar el consejo

            consejoMostrado = true;
            StartCoroutine(CerrarConsejoYDestruir(2f));
        }
        else
        {
            Debug.LogError("No se pudo mostrar el consejo: AsignarConsejo o PanelConsejo es nulo.");
        }
    }

    private IEnumerator CerrarConsejoYDestruir(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (panelConsejo != null)
        {
            panelConsejo.SetActive(false);  // Desactiva el panel si aún existe
        }

        Debug.Log("Destruyendo el objeto del fantasma.");
        Destroy(gameObject);  // Destruye el GameObject del fantasma
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("Jugador colisionó con el fantasma.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador dejó de colisionar con el fantasma.");
        }
    }
}
