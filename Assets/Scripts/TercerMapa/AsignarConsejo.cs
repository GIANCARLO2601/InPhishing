using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsignarConsejo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtConsejo; // Componente UI de TextMeshPro
    [SerializeField] private List<Consejo> consejos; // Lista de consejos (ScriptableObjects)
    private List<Consejo> consejosRestantes; // Consejos no mostrados aún

    void Start()
{
    consejosRestantes = new List<Consejo>(consejos);

    // Debug para verificar los textos de los consejos cargados
    foreach (var consejo in consejosRestantes)
    {
        if (consejo == null)
        {
            Debug.LogError("¡Uno de los consejos es nulo! Verifica la lista en el Inspector.");
        }
        else if (string.IsNullOrEmpty(consejo.textoConsejo))
        {
            Debug.LogError($"El consejo '{consejo.name}' tiene el texto vacío.");
        }
        else
        {
            Debug.Log($"Consejo cargado correctamente: {consejo.textoConsejo}");
        }
    }

    AsignarConsejoAleatorio();
}

    public void AsignarConsejoAleatorio()
    {
        if (consejosRestantes.Count > 0)
        {
            Debug.Log($"Seleccionando consejo aleatorio de {consejosRestantes.Count} disponibles.");

            // Selección aleatoria de un consejo
            int indiceAleatorio = Random.Range(0, consejosRestantes.Count);
            Consejo consejoSeleccionado = consejosRestantes[indiceAleatorio];

            Debug.Log($"Consejo seleccionado: {consejoSeleccionado.textoConsejo}");
            if (consejoSeleccionado == null)
                    {
                        Debug.LogError("El consejo seleccionado es nulo.");
                        return;
                    }

                    // Asegúrate de que el texto del consejo no esté vacío
                    if (string.IsNullOrEmpty(consejoSeleccionado.textoConsejo))
                    {
                        Debug.LogError("El texto del consejo está vacío.");
                    }
                    else
                    {
                        Debug.Log($"Consejo seleccionado: {consejoSeleccionado.textoConsejo}");
                        txtConsejo.text = consejoSeleccionado.textoConsejo;
                    }

            // Remover el consejo ya mostrado
            consejosRestantes.RemoveAt(indiceAleatorio);
        }
        else
        {
            Debug.LogWarning("No hay más consejos disponibles. Reiniciando la lista.");

            // Reiniciar la lista y volver a asignar un consejo
            consejosRestantes = new List<Consejo>(consejos);
            AsignarConsejoAleatorio();
        }
    }
}
