using UnityEngine;

public class AbrirPuerta1 : MonoBehaviour
{
    public GameObject objetoQueSube; // Asigna aquí el objeto desde el Inspector
    public float altura = 20f; // Altura que subirá

    void OnDestroy()
    {
        if (objetoQueSube != null)
        {
            objetoQueSube.transform.position += new Vector3(0, altura, 0);
        }
    }
}
