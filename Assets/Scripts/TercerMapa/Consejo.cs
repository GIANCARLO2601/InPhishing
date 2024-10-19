using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoConsejo", menuName = "Consejo")]
public class Consejo : ScriptableObject
{
    [Header("Consejo")]
    [TextArea] public string textoConsejo; // Cambia el nombre a "textoConsejo"
}
