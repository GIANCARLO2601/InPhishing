using System.Collections;
using UnityEngine;

public class TeleportObjeto : MonoBehaviour
{
    [Header("Colisionadores de Región A y B")]
    public PolygonCollider2D regionA; // Colisionador de la región A (la grande)
    public PolygonCollider2D regionB; // Colisionador de la región B (dentro de A, que se debe evitar)

    [Header("Tiempo de espera")]
    public float tiempoDeEspera = 3f; // Tiempo antes de teletransportar

    private void Start()
    {
        // Iniciar la teletransportación repetida
        StartCoroutine(TeletransportarObjeto());
    }

    IEnumerator TeletransportarObjeto()
    {
        while (true)
        {
            // Esperar los segundos especificados
            yield return new WaitForSeconds(tiempoDeEspera);

            // Generar una nueva posición aleatoria dentro de la región A, pero fuera de la región B
            Vector3 nuevaPosicion = GenerarPosicionFueraDeB();

            // Teletransportar el objeto a la nueva posición
            transform.position = nuevaPosicion;
        }
    }

    Vector3 GenerarPosicionFueraDeB()
    {
        Vector3 nuevaPosicion;

        do
        {
            // Generar una posición aleatoria dentro de la región A
            nuevaPosicion = GenerarPosicionAleatoriaEnRegionA();

        } while (EstaDentroDeRegionB(nuevaPosicion)); // Repetir si la posición está dentro de la región B

        return nuevaPosicion;
    }

    Vector3 GenerarPosicionAleatoriaEnRegionA()
    {
        Bounds boundsA = regionA.bounds;
        Vector3 randomPos;

        do
        {
            // Generar una posición aleatoria dentro de los límites de la región A
            float randomX = Random.Range(boundsA.min.x, boundsA.max.x);
            float randomY = Random.Range(boundsA.min.y, boundsA.max.y);

            randomPos = new Vector3(randomX, randomY, transform.position.z);

        } while (!regionA.OverlapPoint(randomPos)); // Asegurarse de que la posición esté dentro del colisionador de la región A

        return randomPos;
    }

    bool EstaDentroDeRegionB(Vector3 posicion)
    {
        // Verificar si la posición está dentro del colisionador de la región B
        return regionB.OverlapPoint(posicion);
    }
}
