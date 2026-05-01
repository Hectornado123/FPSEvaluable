using UnityEngine;

public class ReemplazarObjeto : MonoBehaviour
{
    public GameObject nuevoPrefab; // arrastras aquí el objeto nuevo desde el inspector

    public void Reemplazar(GameObject viejo)
    {
        Transform t = viejo.transform;

        GameObject nuevo = Instantiate(nuevoPrefab, t.position, t.rotation, t.parent);
        nuevo.transform.localScale = t.localScale;

        Destroy(viejo);
    }
}