using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccionPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject[] fantasmasAActivar; // Array de los fantasmas a activar

    public void ActivarNPCFantasmas()
    {
        foreach (GameObject fantasma in fantasmasAActivar)
        {
            fantasma.SetActive(true); // Activamos cada fantasma
        }

        Debug.Log("Fantasmas activados.");
    }
}
