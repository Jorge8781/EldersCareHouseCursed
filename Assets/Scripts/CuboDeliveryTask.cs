using UnityEngine;
using TMPro;
using System.Collections;

public class CuboDeliveryTask : MonoBehaviour
{
    [Header("Configuración")]
    public Transform puntoEntrega;
    public float distanciaEntrega = 1.5f;

    [Header("Mensajes en pantalla")]
    public TMP_Text mensajeCompletado;

    private bool tareaCompletada = false;

    void Start()
    {
        mensajeCompletado.text = "";
    }

    void Update()
    {
        if (tareaCompletada)
            return;

        float distancia = Vector3.Distance(
            transform.position,
            puntoEntrega.position
        );

        if (distancia <= distanciaEntrega)
        {
            tareaCompletada = true;
            StartCoroutine(MostrarMensaje());
        }
    }

    IEnumerator MostrarMensaje()
    {
        mensajeCompletado.text = "¡Tarea completada!";
        yield return new WaitForSeconds(4f);
        mensajeCompletado.text = "";
    }
}

