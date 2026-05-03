using UnityEngine;

public class LaserFX : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] LineRenderer line;

    [Header("Impacto (arrastrar VFX aquí)")]
    [SerializeField] GameObject impactVFX;

    [Header("Ajustes")]
    [SerializeField] float duration = 0.05f;

    public void Shoot(Vector3 start, Vector3 end, Vector3 normal)
    {
        if (line != null)
        {
            line.SetPosition(0, start);
            line.SetPosition(1, end);
        }

        //  IMPACTO (VFX ANIDADO)
        if (impactVFX != null)
        {
            GameObject impact = Instantiate(impactVFX, end, Quaternion.LookRotation(normal));
            Destroy(impact, 0.5f);
        }

        Invoke(nameof(DestroySelf), duration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}