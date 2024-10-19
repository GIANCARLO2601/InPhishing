using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsignarAcertijo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI preguntaTMP;
    [SerializeField] private TextMeshProUGUI respuestaATMP;
    [SerializeField] private TextMeshProUGUI respuestaBTMP;
    [SerializeField] private TextMeshProUGUI respuestaCTMP;

    public List<CombateAcertijo> listaDePreguntas;
    private List<CombateAcertijo> preguntasDisponibles;
    public CombateAcertijo preguntaActual;

    [Header("Items que puede soltar")]
    [SerializeField] private InventarioItem pocionVidaItem;
    [SerializeField] private InventarioItem megaVidaItem;
    [SerializeField] private InventarioItem medallaItem;

     // Referencia al objeto que deseas destruir

    private bool todasRespuestasCorrectas = false;

    void Start()
    {
        preguntasDisponibles = new List<CombateAcertijo>(listaDePreguntas);
        AsignarPreguntaAleatoria();
    }

    public void AsignarPreguntaAleatoria()
    {
        if (preguntasDisponibles.Count == 0)
        {
            Debug.Log("¡Todas las preguntas han sido respondidas correctamente!");
            todasRespuestasCorrectas = true;
            OtorgarRecompensa();
            RegresarALaEscenaPrincipal();
            return;
        }

        int indiceAleatorio = Random.Range(0, preguntasDisponibles.Count);
        preguntaActual = preguntasDisponibles[indiceAleatorio];

        preguntaTMP.text = preguntaActual.acertijo;
        respuestaATMP.text = preguntaActual.respuestaA;
        respuestaBTMP.text = preguntaActual.respuestaB;
        respuestaCTMP.text = preguntaActual.respuestaC;

        preguntasDisponibles.RemoveAt(indiceAleatorio);
    }

    public void SeleccionarRespuestaA()
    {
        if (preguntaActual.respuestaACorrecta)
        {
            Debug.Log("¡Respuesta A correcta!");
            AsignarPreguntaAleatoria();
        }
        else
        {
            Debug.Log("Respuesta A incorrecta.");
        }
    }

    public void SeleccionarRespuestaB()
    {
        if (preguntaActual.respuestaBCorrecta)
        {
            Debug.Log("¡Respuesta B correcta!");
            AsignarPreguntaAleatoria();
        }
        else
        {
            Debug.Log("Respuesta B incorrecta.");
        }
    }

    public void SeleccionarRespuestaC()
    {
        if (preguntaActual.respuestaCCorrecta)
        {
            Debug.Log("¡Respuesta C correcta!");
            AsignarPreguntaAleatoria();
        }
        else
        {
            Debug.Log("Respuesta C incorrecta.");
        }
    }

    public void OtorgarRecompensa()
    {
        SoltarItemAleatorio();
        OtorgarMedalla();
    }

    private void SoltarItemAleatorio()
    {
        float probabilidad = Random.Range(0f, 100f);

        InventarioItem itemASoltar;

        if (probabilidad <= 10f)
        {
            itemASoltar = megaVidaItem;
            Debug.Log("¡Mega Vida soltada!");
        }
        else
        {
            itemASoltar = pocionVidaItem;
            Debug.Log("Poción de Vida soltada.");
        }

        Inventario.Instance.AñadirItem(itemASoltar, 1);
    }

    private void OtorgarMedalla()
    {
        if (medallaItem != null)
        {
            Inventario.Instance.AñadirItem(medallaItem, 1);
            Debug.Log("¡Medalla otorgada!");
        }
    }

    public void RegresarALaEscenaPrincipal()
    {
        // Reanudar el tiempo
        Time.timeScale = 1;

        // Descargar la escena de acertijo
        SceneManager.UnloadSceneAsync("EscenaAcertijo");

        // Verificar si la escena principal está cargada y activarla
        Scene mainScene = SceneManager.GetSceneByName("EscenaPrincipal");
        if (mainScene.isLoaded)
        {
            SceneManager.SetActiveScene(mainScene); // Activar la escena principal si ya está cargada
        }

        // Mostrar el inventario y reconfigurar la UI
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ConfigurarUIParaCombate(false); // Reconfigura la UI fuera de combate (mostrar el inventario, minimapa, etc.)
            Debug.Log("UI reconfigurada al regresar a la EscenaPrincipal.");
        }
        else
        {
            Debug.LogWarning("UIManager.Instance es nulo. Asegúrate de que está inicializado.");
        }

        
    }
}
