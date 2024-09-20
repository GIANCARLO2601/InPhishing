using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje :MonoBehaviour
{
    public PersonajeVida PersonajeVida {get; private set;}
    public PeronsajeAnimaciones PeronsajeAnimaciones  {get; private set;}
    private void Awake()
    {
        PersonajeVida=GetComponent<PersonajeVida>();
        PeronsajeAnimaciones=GetComponent<PeronsajeAnimaciones>();
        
    }
    public void RestaurarPersonaje(){
        PersonajeVida.RestaurarPersonaje();
        PeronsajeAnimaciones.RevivirPersonaje();
    }
      
}