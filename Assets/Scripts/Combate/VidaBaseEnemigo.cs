using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBaseEnemigo : MonoBehaviour
{
    [Header("Configuración de Salud")]
    [SerializeField] protected float saludInicial = 100f;
    [SerializeField] protected float saludMaxima = 100f;

    public float Salud { get; protected set; }

    protected virtual void Start()
    {
        Salud = saludInicial;  // Iniciar con la salud inicial
    }

    // Método para recibir daño
    public virtual void recibirDaño(float cantidad)
    {
        if (cantidad <= 0) return;  // Ignorar daño no positivo

        Salud -= cantidad;  // Reducir la salud
        Salud = Mathf.Clamp(Salud, 0, saludMaxima);  // Limitar entre 0 y salud máxima

        Debug.Log($"El enemigo ha recibido {cantidad} de daño. Salud actual: {Salud}/{saludMaxima}");

        if (Salud <= 0)
        {
            EnemigoDerrotado();  // Llamar al método si la salud llega a 0
        }
    }

    protected virtual void EnemigoDerrotado()
    {
        Debug.Log("¡Enemigo derrotado!");
        Destroy(gameObject);  // Destruir el objeto del enemigo
    }
}
