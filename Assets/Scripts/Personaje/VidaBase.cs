using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    public float Salud { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludMax); // Para inicializar la barra con la salud inicial
    }

    public void recibirDaño(float cantidad)
    {
        if (cantidad <= 0)
        {
            return;
        }

        Debug.Log("Cantidad de daño: " + cantidad);
        Debug.Log("Salud antes del daño: " + Salud);

        if (Salud > 0f)
        {
            Salud -= cantidad;
            Salud = Mathf.Clamp(Salud, 0, saludMax); // Asegurarnos que no baje de 0

            ActualizarBarraVida(Salud, saludMax);
            Debug.Log("Salud actual: " + Salud);

            if (Salud <= 0f)
            {
                ActualizarBarraVida(Salud, saludMax);
                PersonajeDerrotado();
            }
        }
    }

    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ActualizarVidaPersonajes(vidaActual, vidaMax);
        }
        else
        {
            Debug.LogError("UIManager.Instance no está asignado.");
        }
    }

    protected virtual void PersonajeDerrotado()
    {
        // Lógica cuando el personaje se queda sin vida
        Debug.Log("¡Personaje derrotado!");
    }
}
