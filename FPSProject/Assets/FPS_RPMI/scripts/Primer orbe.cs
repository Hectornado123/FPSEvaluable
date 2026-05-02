using UnityEngine;

public class OrbStarter : MonoBehaviour
{
    [Header("Primer orbe de la cadena")]
    public OrbTrigger primerOrbe;

    [Header("Efectos opcionales")]
    public AudioSource audioSource;
    public AudioClip sonidoRomper;

    private bool activado = false;

    // Esto lo llamas cuando le disparas
    public void Romper()
    {
        if (activado) return;

        activado = true;

        // sonido
        if (audioSource && sonidoRomper)
            audioSource.PlayOneShot(sonidoRomper);

        // arranca el puzzle
        if (primerOrbe != null)
            primerOrbe.Activar();

        // simula destrucción
        Destroy(gameObject);
    }

    // TEST rápido con click
    private void OnMouseDown()
    {
        Romper();
    }
}