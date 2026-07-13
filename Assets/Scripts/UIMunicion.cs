using UnityEngine;
using UnityEngine.UI;

public class UIMunicion : MonoBehaviour
{
    public Disparar arma;
    public Text textoMunicion;

    void Start()
    {
        if (arma == null)
            arma = FindFirstObjectByType<Disparar>();

        if (arma != null)
        {
            arma.AlCambiarMunicion += ActualizarMunicion;
            ActualizarMunicion(arma.BalasActuales(), arma.BalasMaximas(), arma.EstaRecargando());
        }
    }

    void OnDestroy()
    {
        if (arma != null)
            arma.AlCambiarMunicion -= ActualizarMunicion;
    }

    void ActualizarMunicion(int actuales, int maximas, bool recargando)
    {
        if (textoMunicion == null)
            return;
        
        if (recargando)
            textoMunicion.text = "Recargando...";
        else
            textoMunicion.text = "Balas: " + actuales + "/" + maximas;
    }
}