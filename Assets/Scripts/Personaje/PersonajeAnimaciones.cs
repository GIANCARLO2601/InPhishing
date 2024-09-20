using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeronsajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;

    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento ;
    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");
    private void Awake()
    {
        _animator=GetComponent<Animator>();
        _personajeMovimiento=GetComponent<PersonajeMovimiento>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarLayers();
        if(_personajeMovimiento.EnMovimiento==false)
        {
            return;
        }
        
        _animator.SetFloat("X",_personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat("Y",_personajeMovimiento.DireccionMovimiento.y);
    }

    private void ActivarLayer(string nombreLayer)
    {
        for(int i = 0 ; i<_animator.layerCount;i++)
        {
            _animator.SetLayerWeight(i , weight:0);
        }
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer),weight:1);
    }
    private void ActualizarLayers(){
        if(_personajeMovimiento.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }else{
            ActivarLayer(layerIdle);
        }
    }
    private void PersonajeDerrotadoRespuesta()
    {
        if(_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle))==1)
        {
            _animator.SetBool(derrotado,true);
        }
    }
    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }
    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }
    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado,false);
    }
}
