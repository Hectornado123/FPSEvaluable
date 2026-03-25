using UnityEngine;

public class BaldosaPresion : MonoBehaviour
{
    [SerializeField] GameObject objetoInvisible;
    [SerializeField] float alturaSubida = 5f; // 5 metros
    [SerializeField] float velocidad = 3f;

    int objetosEncima = 0;

    Vector3 posicionInicial;
    Vector3 posicionSubida;

    void Start()
    {
        if (objetoInvisible != null)
        {
            posicionInicial = objetoInvisible.transform.position;
            posicionSubida = posicionInicial + Vector3.up * alturaSubida;
        }
    }

    void Update()
    {
        if (objetoInvisible == null) return;

        if (objetosEncima > 0)
        {
            objetoInvisible.transform.position = Vector3.Lerp(
                objetoInvisible.transform.position,
                posicionSubida,
                velocidad * Time.deltaTime
            );
        }
        else
        {
            objetoInvisible.transform.position = Vector3.Lerp(
                objetoInvisible.transform.position,
                posicionInicial,
                velocidad * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        objetosEncima++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;

        objetosEncima--;
        if (objetosEncima < 0) objetosEncima = 0;
    }
}