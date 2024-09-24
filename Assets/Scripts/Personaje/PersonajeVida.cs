using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement; // Importa el manejador de escenas

public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;
    public bool Derrotado { get; private set; }
    private BoxCollider2D _boxCollider2D;
    public bool PuedeSerCurado => Salud < saludMax;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        ActualizarBarraVida(Salud, saludMax);

        // Restore player position if it exists
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if (Derrotado)
        {
            return;
        }
        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if (Salud > saludMax)
            {
                Salud = saludMax;
            }
            ActualizarBarraVida(vidaActual: Salud, saludMax);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            recibirDaño(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }

    protected override void PersonajeDerrotado()
    {
        _boxCollider2D.enabled = false;
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke();
    }

    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonajes(vidaActual, vidaMax);
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemigo"))
    {
        // Save player's position and enemy before combat
        GameManager.Instance.SavePlayerPosition(transform.position);
        GameManager.Instance.SaveEnemyData(collision.gameObject.name);

        // Load the combat scene
        GameManager.Instance.LoadCombatScene();
    }
}


}
