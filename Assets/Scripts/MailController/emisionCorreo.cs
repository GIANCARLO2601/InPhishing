using UnityEngine;
using TMPro; // Para los textos de UI

public class emisionCorreo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remitenteTMP;
    [SerializeField] private TextMeshProUGUI asuntoTMP;
    [SerializeField] private TextMeshProUGUI cuerpoTMP;
    [SerializeField] private TextMeshProUGUI enlaceVisibleTMP;

    // Este campo se asignará con un ScriptableObject de tipo CorreoPhishing
    public mail correoPhishing;

    void Start()
    {
        if (correoPhishing != null)
        {
            // Mostrar los detalles del correo en la UI
            remitenteTMP.text = correoPhishing.Remitente;
            asuntoTMP.text = correoPhishing.Asunto;
            cuerpoTMP.text = correoPhishing.Cuerpo;
            enlaceVisibleTMP.text = correoPhishing.EnlaceVisible;

            // Puedes usar el EnlaceReal al hacer clic en el enlace visible
        }
    }
}
