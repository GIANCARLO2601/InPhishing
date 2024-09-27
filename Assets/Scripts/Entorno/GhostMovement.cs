using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento del fantasma
    public float changeDirectionTime = 3.0f; // Tiempo entre cambios de dirección
    public GameObject mapa; // Referencia al objeto del mapa

    private Vector3 targetPosition;
    private float timer;

    private Vector2 mapBoundsX;
    private Vector2 mapBoundsY;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public bool PuedeMoverse { get; set; } = true;
    void Start()
    {
        if (!PuedeMoverse) return;
        // Obtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtener los límites del objeto mapa usando su BoxCollider2D
        SetMapBounds();

        // Inicializa el primer objetivo
        SetNewRandomTargetPosition();
        timer = changeDirectionTime;
    }

    void Update()
    {
        // Mover el fantasma hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Verificar si llegó a la posición objetivo o si el temporizador ha llegado a 0
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f || timer <= 0)
        {
            // Establecer una nueva posición objetivo aleatoria
            SetNewRandomTargetPosition();
            timer = changeDirectionTime;
        }

        // Reducir el tiempo del temporizador
        timer -= Time.deltaTime;
    }

    void SetNewRandomTargetPosition()
    {
        float randomX = Random.Range(mapBoundsX.x, mapBoundsX.y);
        float randomY = Random.Range(mapBoundsY.x, mapBoundsY.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z); // Mantiene la posición Z del fantasma

        // Cambiar la dirección visual del fantasma según el movimiento en el eje X
        if (targetPosition.x < transform.position.x)
        {
            // Si el objetivo está a la izquierda, girar el sprite
            spriteRenderer.flipX = true;
        }
        else if (targetPosition.x > transform.position.x)
        {
            // Si el objetivo está a la derecha, no girar el sprite
            spriteRenderer.flipX = false;
        }
    }

    void SetMapBounds()
    {
        if (mapa != null)
        {
            // Obtener el BoxCollider2D del objeto "primer mapa"
            BoxCollider2D mapCollider = mapa.GetComponent<BoxCollider2D>();

            if (mapCollider != null)
            {
                // Obtener los límites en el eje X e Y
                Vector3 mapMin = mapCollider.bounds.min; // Límite inferior izquierdo
                Vector3 mapMax = mapCollider.bounds.max; // Límite superior derecho

                mapBoundsX = new Vector2(mapMin.x, mapMax.x); // Limites en el eje X
                mapBoundsY = new Vector2(mapMin.y, mapMax.y); // Limites en el eje Y
            }
            else
            {
                Debug.LogError("El objeto 'primer mapa' no tiene un BoxCollider2D.");
            }
        }
        else
        {
            Debug.LogError("El objeto 'primer mapa' no ha sido asignado.");
        }
    }
}
