using UnityEngine;

public class MoverBloque : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private float t = 0f;
    private bool haciaB = true;

    void Update()
    {
        if (haciaB)
        {
            t += Time.deltaTime * velocidad;
        }
        else
        {
            t -= Time.deltaTime * velocidad;
        }

        transform.position = Vector3.Lerp(puntoA.position, puntoB.position, t);

        if (t >= 1f)
        {
            haciaB = false;
        }
        else if (t <= 0f)
        {
            haciaB = true;
        }
    }
}
