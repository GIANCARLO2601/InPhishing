using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDerrotado : MonoBehaviour
{
    [SerializeField] private InventarioItem llave; // La llave que se otorgará al derrotar al boss

    private void Start()
    {
        // Verificar si el boss ha sido derrotado
        if (PlayerPrefs.GetInt("BossDerrotado", 0) == 1)
        {
            // Si ya fue derrotado, le damos la llave al jugador
            Inventario.Instance.AñadirItem(llave, 1);
            Debug.Log("Jugador ha recibido la llave.");
        }
    }

    public void OtorgarLlave()
    {
        // Marcar el boss como derrotado
        PlayerPrefs.SetInt("BossDerrotado", 1);

        // Regresar a la escena principal después de derrotar al boss
        SceneManager.LoadScene("EscenaPrincipal");
    }
}
