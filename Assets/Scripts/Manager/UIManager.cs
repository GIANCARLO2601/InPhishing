using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Config")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject minimapa;
    [SerializeField] private GameObject panelBotones;

    private float vidaActual;
    private float vidaMax;

    private void Awake()
    {
        // Hacer que este objeto no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }

    

    private void Update()
    {
        if (vidaPlayer != null && vidaTMP != null)
        {
            ActualizarUIPersonaje();
        }
    }

    public void ActualizarVidaPersonajes(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;

        ActualizarUIPersonaje(); 
    }

    public void ActualizarUIPersonaje()
    {
        if (vidaPlayer != null && vidaTMP != null)
        {
            vidaPlayer.fillAmount = Mathf.Clamp(vidaActual / vidaMax, 0, 1);
            vidaTMP.text = $"{vidaActual}/{vidaMax}";
            Debug.Log($"Actualizando barra de vida: {vidaActual}/{vidaMax}");
        }
        else
        {
            Debug.LogWarning("No se pueden actualizar los elementos de UI porque vidaPlayer o vidaTMP no están asignados.");
        }
    }

    public void abrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void ConfigurarUIParaCombate(bool enCombate)
    {
        if (enCombate)
        {
            panelInventario.SetActive(false);  // Ocultar el inventario
            minimapa.SetActive(false);  
            panelBotones.SetActive(false);      // Ocultar los botones extra
        }
        else
        {
            panelInventario.SetActive(true);  // Mostrar el inventario
            minimapa.SetActive(true);  
            panelBotones.SetActive(true);     // Mostrar los botones
        }
    }
}
