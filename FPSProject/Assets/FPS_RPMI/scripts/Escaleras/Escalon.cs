using System.Collections;
using UnityEngine;

public class Escalon : MonoBehaviour
{
    public Vector3 posicionFinal;
    public float velocidad = 2f;

    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    public void Activar()
    {
        StartCoroutine(MoverEscalon());
    }

    IEnumerator MoverEscalon()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * velocidad;
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);
            yield return null;
        }

        transform.position = posicionFinal;
    }
}