using UnityEngine;

public class BaldosaPresion : MonoBehaviour
{
    [SerializeField] GameObject objetoInvisible;

    int objetosEncima = 0;

    void Start()
    {
        if (objetoInvisible != null)
        {
            objetoInvisible.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Evita contar el propio collider de la baldosa
        if (other.isTrigger) return;

        objetosEncima++;

        if (objetoInvisible != null)
        {
            objetoInvisible.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;

        objetosEncima--;

        if (objetosEncima <= 0)
        {
            objetosEncima = 0;

            if (objetoInvisible != null)
            {
                objetoInvisible.SetActive(false);
            }
        }
    }
}