using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DireccionMovimiento
{
    Horizontal,
    Vertical
}

public class WaypointMovimiento : MonoBehaviour
{
    [SerializeField] private DireccionMovimiento direccion;
    [SerializeField] private float velocidad;

    public Vector3 PosicionActual => _Welpoints.ObtenerPosicionMovimiento(puntoActualIndex);

    private Welpoints _Welpoints;
    private int puntoActualIndex;

    private void Start()
    {
        puntoActualIndex = 0;
        _Welpoints = GetComponent<Welpoints>();
    }

    private void Update()
{
    MoverPersonaje();
    if (ComprobarPuntoActualAlcanzado())
    {
        ActualizarIndexMovimiento();
    }
}

    private void MoverPersonaje()
    {
    transform.position = Vector3.MoveTowards(transform.position, PosicionActual, velocidad * Time.deltaTime);
    }
    private bool ComprobarPuntoActualAlcanzado()
    {
    float distanciaHaciaPuntoActual = (transform.position - PosicionActual).magnitude;
    if (distanciaHaciaPuntoActual < 0.1f)
    {
        return true;
    }

    return false;

    }
    private void ActualizarIndexMovimiento()
{
    if (puntoActualIndex == _Welpoints.Puntos.Length - 1)
    {
        puntoActualIndex = 0;
    }
    else
    {
        if (puntoActualIndex < _Welpoints.Puntos.Length - 1)
        {
            puntoActualIndex++;
        }
    }
}



}
