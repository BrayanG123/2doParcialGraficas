using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Vida))]
public class MuerteJugadorTemporal : MonoBehaviour
{
    public Vida vida;

    void Awake()
    {
        vida = GetComponent<Vida>();
    }

    void OnEnable()
    {
        vida.AlMorir += ManejarMuerte;
    }

    void OnDisable()
    {
        vida.AlMorir -= ManejarMuerte;
    }

    void ManejarMuerte(Vida vidaMuerta)
    {
        if (!vidaMuerta.esJugador)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}