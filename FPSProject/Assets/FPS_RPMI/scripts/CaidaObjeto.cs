using UnityEngine;

public class CaidaObjeto : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true; // empieza suspendido
        }
        else
        {
            Debug.LogError("No hay Rigidbody en " + gameObject.name);
        }
    }

    public void ActivarFisicas()
    {
        if (rb != null)
        {
            rb.isKinematic = false; // ahora cae
        }
    }
}
