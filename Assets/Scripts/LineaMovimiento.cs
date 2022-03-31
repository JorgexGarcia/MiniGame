using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaMovimiento : MonoBehaviour
{
    //Variable
    public Transform inicio;
    public Transform destino;

    //Método para ver solo en modo edición el movimiento que seguira una plataforma movil
    private void OnDrawGizmosSelected()
    {
        if (inicio != null && destino != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(inicio.position, destino.position);
            Gizmos.DrawSphere(inicio.position, 0.15f);
            Gizmos.DrawSphere(destino.position, 0.15f);
        }
    }
}
