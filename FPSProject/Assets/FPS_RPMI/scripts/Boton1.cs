using System.Collections;
using UnityEngine;

public class Boton1 : MonoBehaviour
{
    public Transform cubo;          // El cubo que quieres mover
    public float altura = 2f;       // Distancia que sube (editable en inspector)
    public float velocidad = 2f;    // Velocidad del movimiento

    private Vector3 posicionInicial;
    private bool enMovimiento = false;

    void Start()
    {
        if (cubo != null)
            posicionInicial = cubo.position;
    }

    void OnMouseDown() // Se ejecuta al hacer click sobre ESTE objeto
    {
        if (!enMovimiento)
            StartCoroutine(MoverCubo());
    }

    IEnumerator MoverCubo()
    {
        enMovimiento = true;

        Vector3 posicionFinal = posicionInicial + Vector3.right * altura;

        // Subir
        while (Vector3.Distance(cubo.position, posicionFinal) > 0.01f)
        {
            cubo.position = Vector3.MoveTowards(cubo.position, posicionFinal, velocidad * Time.deltaTime);
            yield return null;
        }

        // Esperar 5 segundos
        yield return new WaitForSeconds(2.5f);

        // Bajar
        while (Vector3.Distance(cubo.position, posicionInicial) > 0.01f)
        {
            cubo.position = Vector3.MoveTowards(cubo.position, posicionInicial, velocidad * Time.deltaTime);
            yield return null;
        }

        enMovimiento = false;
    }
}