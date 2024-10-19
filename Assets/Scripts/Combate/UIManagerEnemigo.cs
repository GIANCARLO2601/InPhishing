using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerEnemigo : Singleton<UIManagerEnemigo>
{
    [Header("Vida del Enemigo")]
    [SerializeField] private Image barraVidaEnemigo;
    [SerializeField] private TextMeshProUGUI textoVidaEnemigo;

    [Header("Sprite del Enemigo")]
    [SerializeField] private Image imagenEnemigoUI;

    public void AsignarSpriteEnemigo(Sprite sprite)
    {
        if (sprite != null)
        {
            imagenEnemigoUI.sprite = sprite;
            Debug.Log("Sprite del enemigo asignado correctamente.");
        }
        else
        {
            Debug.LogError("El sprite recibido es nulo.");
        }
    }

    public void ActualizarVidaEnemigo(float pVidaActual, float pVidaMaxima)
    {
        if (barraVidaEnemigo != null && textoVidaEnemigo != null)
        {
            barraVidaEnemigo.fillAmount = Mathf.Clamp(pVidaActual / pVidaMaxima, 0, 1);
            textoVidaEnemigo.text = $"{pVidaActual} / {pVidaMaxima}";
            Debug.Log($"Vida del enemigo actualizada: {pVidaActual} / {pVidaMaxima}");
        }
        else
        {
            Debug.LogWarning("No se pueden actualizar los elementos de UI porque faltan referencias.");
        }
    }
}
