using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionDos : MonoBehaviour
{
    private DialogoManager dialogoManager;

    [Header("NPCs que se activarán")]
    [SerializeField] private GameObject[] npcsActivar; // Array de NPCs que se activarán

    public void ActivarNPC()
    {
        foreach (GameObject fantasma in npcsActivar)
        {
            fantasma.SetActive(true); // Activamos cada fantasma
        }

        Debug.Log("npc activados.");
    }
}
