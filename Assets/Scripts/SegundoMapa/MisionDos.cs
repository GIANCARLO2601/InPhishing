using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionDos : MonoBehaviour
{
    [SerializeField] private GameObject[] npcsActivar;  // NPCs a activar tras completar la misión
    [SerializeField] private int totalEstatuas = 3;  // Número total de estatuas en la misión
    private int estatuasCompletadas = 0;  // Contador de estatuas completadas
    public MiniEstatua EstatuaActual { get; private set; }  // Estatua actual

    public void EstablecerEstatuaActual(MiniEstatua estatua)
    {
        EstatuaActual = estatua;
        Debug.Log($"Estatua actual establecida: {estatua.name}");
    }

    public void RegistrarEstatuaCompletada()
    {
        estatuasCompletadas++;
        Debug.Log($"Estatuas completadas: {estatuasCompletadas} / {totalEstatuas}");

        if (estatuasCompletadas >= totalEstatuas)
        {
            OtorgarRecompensa();
        }
    }

    private void OtorgarRecompensa()
    {
        Debug.Log("¡Todas las estatuas completadas! Recompensa otorgada.");
        ActivarNPC();  // Activar los NPCs como recompensa
    }

    public void ActivarNPC()
    {
        foreach (GameObject npc in npcsActivar)
        {
            if (npc != null)
            {
                npc.SetActive(true);
                Debug.Log($"{npc.name} activado.");
            }
        }
    }
}
