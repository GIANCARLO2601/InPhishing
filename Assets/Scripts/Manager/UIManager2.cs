using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager2 : Singleton<UIManager2>
{
    
    [Header("Config")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private TextMeshProUGUI vidaTMP;
    // Start is called before the first frame update
    private float vidaActual;
    private float vidaMax;
    

    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
    }
    public void ActualizarVidaPersonajes(float pVidaActual,float pVidaMax)
    {
        vidaActual=pVidaActual;
        vidaMax=pVidaMax;
    }
    private void ActualizarUIPersonaje(){
        vidaPlayer.fillAmount=Mathf.Lerp(vidaPlayer.fillAmount,
        vidaActual/vidaMax, 10f * Time.deltaTime);
        vidaTMP.text=$"{vidaActual}/{vidaMax}";
    }
}
