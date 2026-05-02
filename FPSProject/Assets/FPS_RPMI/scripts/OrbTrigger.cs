using UnityEngine;

public class OrbTrigger : MonoBehaviour
{
    [Header("Escalon")]
    public Transform escalon;

    [Header("Objetivo")]
    public Transform objetivo;

    [Header("Duracion")]
    public float duracion = 1.5f;

    [Header("Curva")]
    public AnimationCurve curva = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Rebote")]
    public bool usarRebote = true;
    public float fuerzaRebote = 0.2f;
    public float duracionRebote = 0.2f;

    [Header("Sonido")]
    public AudioSource audioSource;
    public AudioClip sonidoImpacto;

    [Header("Siguiente orbe (ARRASTRABLE)")]
    public Transform siguienteOrbe;

    private OrbTrigger siguienteOrbeScript;

    private bool activado = false;
    private bool rebotando = false;

    private float tiempo = 0f;
    private float tiempoRebote = 0f;

    private Vector3 posicionInicial;

    void Start()
    {
        if (siguienteOrbe != null)
            siguienteOrbeScript = siguienteOrbe.GetComponent<OrbTrigger>();
    }

    void Update()
    {
        if (activado && escalon != null && objetivo != null && !rebotando)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracion);

            float tCurva = curva.Evaluate(t);

            escalon.position = Vector3.Lerp(posicionInicial, objetivo.position, tCurva);

            if (t >= 1f)
            {
                if (usarRebote)
                {
                    rebotando = true;
                    tiempoRebote = 0f;

                    if (audioSource && sonidoImpacto)
                        audioSource.PlayOneShot(sonidoImpacto);
                }
                else
                {
                    Finalizar();
                }
            }
        }

        if (rebotando)
        {
            tiempoRebote += Time.deltaTime;
            float t = tiempoRebote / duracionRebote;

            float offset = Mathf.Sin(t * Mathf.PI) * fuerzaRebote;

            escalon.position = objetivo.position + new Vector3(0, offset, 0);

            if (t >= 1f)
            {
                rebotando = false;
                escalon.position = objetivo.position;
                Finalizar();
            }
        }
    }

    void Finalizar()
    {
        activado = false;
        tiempo = 0f;

        if (siguienteOrbeScript != null)
            siguienteOrbeScript.Activar();
    }

    public void Activar()
    {
        activado = true;
        tiempo = 0f;
        posicionInicial = escalon.position;
    }

    private void OnMouseDown()
    {
        Activar();
        Destroy(gameObject);
    }
}