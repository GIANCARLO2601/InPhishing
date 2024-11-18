using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Batalla : MonoBehaviour
{
    private PersonajeVida personajeVida;

    [Header("Referencias del UI")]
    public Button botonOpcion1;
    public Button botonOpcion2;
    public Button botonOpcion3;

    public InventarioItem llaveItem;
    public TextMeshProUGUI textoPregunta;
    public TextMeshProUGUI textoResultado;

    [Header("Vida del Enemigo")]
    [SerializeField] private EnemigoVida enemigoVida;

    private Asignar asignar;

    private void Start()
    {
        personajeVida = FindObjectOfType<PersonajeVida>();
        enemigoVida = FindObjectOfType<EnemigoVida>();
        asignar = GetComponent<Asignar>();

        if (enemigoVida == null || personajeVida == null)
        {
            Debug.LogError("Error al encontrar los componentes necesarios.");
        }
    }

    public void AsignarListeners(bool esCorrectaA, bool esCorrectaB, bool esCorrectaC)
    {
        // Limpiar listeners previos para evitar duplicaciones
        botonOpcion1.onClick.RemoveAllListeners();
        botonOpcion2.onClick.RemoveAllListeners();
        botonOpcion3.onClick.RemoveAllListeners();

        // Asignar nuevos listeners
        botonOpcion1.onClick.AddListener(() => VerificarRespuesta(esCorrectaA));
        botonOpcion2.onClick.AddListener(() => VerificarRespuesta(esCorrectaB));
        botonOpcion3.onClick.AddListener(() => VerificarRespuesta(esCorrectaC));
    }

    private void VerificarRespuesta(bool esCorrecta)
    {
        if (esCorrecta)
        {
            textoResultado.text = "¡Respuesta correcta!";
            enemigoVida.recibirDaño(10);

            if (enemigoVida.Salud <= 0)
            {
                OtorgarRecompensa();
                RegresarALaEscenaPrincipal();
            }
            else
            {
                asignar.ResponderCorrectamente();
            }
        }
        else
        {
            textoResultado.text = "Respuesta incorrecta. Intenta de nuevo.";
            personajeVida.recibirDaño(20);
            asignar.ResponderIncorrectamente();
        }
    }

    public void OtorgarRecompensa()
    {
        if (llaveItem != null)
        {
            Inventario.Instance.AñadirItem(llaveItem, 1);
            Debug.Log("Llave agregada al inventario.");
        }
    }

    public void RegresarALaEscenaPrincipal()
    {
        SceneManager.UnloadSceneAsync("EscenaCombate");
        BossInteraccion.Instance?.DestruirEnemigo();

        Scene mainScene = SceneManager.GetSceneByName("EscenaPrincipal");
        if (!mainScene.isLoaded)
        {
            SceneManager.LoadScene("EscenaPrincipal");
        }
        else
        {
            SceneManager.SetActiveScene(mainScene);
        }

        UIManager.Instance.ConfigurarUIParaCombate(false);
        Time.timeScale = 1;
    }
}
