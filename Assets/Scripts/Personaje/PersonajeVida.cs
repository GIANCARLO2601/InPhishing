using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;
    public bool Derrotado{get; private set;}
    private BoxCollider2D _boxCollider2D;
    public bool PuedeSerCurado => Salud < saludMax;
    
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start(){
        base.Start();
        ActualizarBarraVida(Salud,saludMax);
    }
    public void RestaurarSalud(float cantidad)
    {
        if(Derrotado){
            return;
        }
        if(PuedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud =saludMax;
            }
            ActualizarBarraVida(vidaActual:Salud,saludMax);
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            recibirDaño(10);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }
    protected override void PersonajeDerrotado()
    {
        _boxCollider2D.enabled=false;
        Derrotado=true;
        EventoPersonajeDerrotado?.Invoke();
    }
    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled=true;
        Derrotado=false;
        Salud=saludInicial;
        ActualizarBarraVida(Salud,saludInicial);
    }
    protected override void ActualizarBarraVida(float vidaActual,float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonajes(vidaActual,vidaMax);
    }
}
