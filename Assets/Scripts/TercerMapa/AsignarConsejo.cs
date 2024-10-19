using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsignarConsejo : MonoBehaviour
{
    public static AsignarConsejo Instance { get; private set; }  // Singleton

    [SerializeField] private TextMeshProUGUI txtConsejo;
    [SerializeField] private List<Consejo> consejos;  // Lista de consejos (ScriptableObjects)
    private List<Consejo> consejosRestantes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Mantener este objeto entre escenas
            Debug.Log("AsignarConsejo Singleton inicializado.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("AsignarConsejo duplicado. Destruyendo...");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        consejosRestantes = new List<Consejo>(consejos);  // Hacer copia de los consejos
        if (txtConsejo == null)
        {
            Debug.LogError("TextMeshProUGUI no asignado en AsignarConsejo.");
        }
    }

    public void AsignarConsejoAleatorio()
    {
        if (consejosRestantes.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, consejosRestantes.Count);
            Consejo consejoSeleccionado = consejosRestantes[indiceAleatorio];

            if (txtConsejo != null)
            {
                txtConsejo.text = consejoSeleccionado.textoConsejo;
                Debug.Log($"Consejo mostrado: {consejoSeleccionado.textoConsejo}");
            }
            else
            {
                Debug.LogError("TextMeshProUGUI no asignado en AsignarConsejo.");
            }

            consejosRestantes.RemoveAt(indiceAleatorio);
        }
        else
        {
            Debug.LogWarning("No hay más consejos disponibles. Reiniciando lista.");
            consejosRestantes = new List<Consejo>(consejos);
        }
    }
}
