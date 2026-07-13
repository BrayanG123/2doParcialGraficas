// using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Vida : MonoBehaviour
{
    
    public int vidaMax = 3;
    public bool esJugador = false;
    public bool destruirAlMorir = true;

    public event Action<Vida> AlMorir;
    public event Action<Vida, int> AlRecibirDano;
    public event Action<Vida, int, int> AlCambiarVida;
    private int vidaActual;
    private bool estaMuerto = false;

    void Start()
    {
        vidaActual = vidaMax;
        NotificarCambioVida();
    }

    public void RecibirDano(int cantidad)
    {
        if (estaMuerto)
            return;
        
        if (cantidad <= 0)
            return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMax);

        AlRecibirDano?.Invoke(this, cantidad);
        NotificarCambioVida();

        if (vidaActual <= 0) 
            Morir();
    }

    public void Curar(int cantidad)
    {
        if (estaMuerto)
            return;
        
        if (cantidad <= 0)
            return;

        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMax);

        NotificarCambioVida();
    }

    void Morir()
    {
        if (estaMuerto)
            return;

        estaMuerto = true;
        AlMorir?.Invoke(this);

        if (!esJugador && destruirAlMorir)
            Destroy(gameObject);

        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reinicia
        // else
        //     Destroy(gameObject);
    }

    void NotificarCambioVida()
    {
        AlCambiarVida?.Invoke(this, vidaActual, vidaMax);
    }

    public int VidaActual()
    {
        return vidaActual;
    }

    public int VidaMaxima()
    {
        return vidaMax;
    }

    public bool EstaMuerto()
    {
        return estaMuerto;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
