using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorAcertijo : MonoBehaviour
{
    private bool acertijoResuelto = false;

    public void ResolverAcertijo()
    {
        if (!acertijoResuelto)
        {
            acertijoResuelto = true;

            MisionDos mision = FindObjectOfType<MisionDos>();

            if (mision != null && mision.EstatuaActual != null)
            {
                mision.EstatuaActual.CompletarEstatua();  // Marcar la estatua como completada
                Debug.Log("Acertijo resuelto. Estatua completada.");
            }

            RegresarAEscenaPrincipal();
        }
    }

    private void RegresarAEscenaPrincipal()
    {
        SceneManager.UnloadSceneAsync("EscenaAcertijo");

        Scene mainScene = SceneManager.GetSceneByName("EscenaPrincipal");
        if (mainScene.IsValid())
        {
            SceneManager.SetActiveScene(mainScene);
            Debug.Log("Regresando a la escena principal.");
        }
    }
}
