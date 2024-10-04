using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    public float Salud {get ; protected set; }      

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Salud = saludInicial;

    }

     
    public void recibirDaño(float cantidad)
    {
        Debug.Log("cantidad " + cantidad);
        Debug.Log("salud " + Salud);
        if(cantidad <= 0 )
        {
            return;
        }
        if( Salud > 0f )
        {
            Debug.Log("salud " + Salud);
            Salud -= cantidad;
            ActualizarBarraVida(Salud,saludMax);
            Debug.Log("Salud actual: " + Salud);
            if(Salud <= 0f)
            {
                ActualizarBarraVida(Salud,saludMax);
                PersonajeDerrotado();
            }
        }

    }
    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonajes(vidaActual, vidaMax);

    }
    protected virtual void PersonajeDerrotado()
    {

    }
}
