using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteractuar;
    [SerializeField] private NPCDialogo npcDialogo;

    public NPCDialogo Dialogo => npcDialogo;

    // Este método se llama cuando ocurre una colisión física
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si la colisión fue con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BOOS Colisión detectada con el Player");
            BossDialogoManager.Instance.NPCDisponible = this;
            npcButtonInteractuar.SetActive(true);
        }
    }

    // Este método se llama cuando se termina la colisión física
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha salido del área del BOOS");
            BossDialogoManager.Instance.NPCDisponible = null;
            npcButtonInteractuar.SetActive(false);
        }
    }
}
