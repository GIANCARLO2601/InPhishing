using UnityEngine;

public class BossInteraccion : Singleton<BossInteraccion>
{
     // Singleton

    [SerializeField] private GameObject npcButtonInteractuar;  // Botón para interactuar
    [SerializeField] private NPCDialogo npcDialogo;  // Referencia al diálogo
    [SerializeField] private SpriteRenderer spriteRendererBoss;  // SpriteRenderer del Boss

    public NPCDialogo Dialogo => npcDialogo;  // Propiedad para acceder al diálogo

     protected override void Awake()
    {
        base.Awake();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión detectada con el Player.");
            BossDialogoManager.Instance.NPCDisponible = this;

            // Enviar el sprite del Boss al BossDialogoManager
            if (spriteRendererBoss != null)
            {
                BossDialogoManager.Instance.AsignarSpriteBoss(spriteRendererBoss.sprite);
                Debug.Log("Sprite del Boss enviado al BossDialogoManager.");
            }
            else
            {
                Debug.LogError("SpriteRenderer del Boss no asignado.");
            }

            npcButtonInteractuar.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha salido del área del Boss.");
            BossDialogoManager.Instance.NPCDisponible = null;
            npcButtonInteractuar.SetActive(false);
        }
    }

    public void DestruirEnemigo()
    {
        Debug.Log("Boss destruido.");
        Destroy(gameObject);
    }
}
