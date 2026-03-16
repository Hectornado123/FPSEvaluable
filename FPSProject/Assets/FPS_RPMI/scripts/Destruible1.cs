using UnityEngine;

public class Destruible1 : MonoBehaviour
{
    public CaidaObjeto objetoQueCae;

    public void Activar()
    {
        if (objetoQueCae != null)
        {
            objetoQueCae.ActivarFisicas();
        }
    }
}
