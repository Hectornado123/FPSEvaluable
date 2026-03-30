using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject objetoAEliminar; // El objeto que vas a destruir
    public float velocidad = 2f;
    public float alturaMaxima = 20f;

    void Update()
    {
        // Cuando el objeto ya no exista (fue destruido)
        if (objetoAEliminar == null)
        {
            if (transform.position.y < alturaMaxima)
            {
                Vector3 pos = transform.position;
                pos.y += velocidad * Time.deltaTime;
                pos.y = Mathf.Min(pos.y, alturaMaxima);
                transform.position = pos;
            }
        }
    }
}
