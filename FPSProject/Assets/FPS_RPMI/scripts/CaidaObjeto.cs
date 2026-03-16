using UnityEngine;

public class CaidaObjeto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // empieza suspendido
    }

    public void ActivarFisicas()
    {
        rb.isKinematic = false; // ahora cae
    }
}
