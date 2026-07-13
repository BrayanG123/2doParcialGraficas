using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorNivel : MonoBehaviour
{
    [Header("Enemigos")]
    public List<Vida> enemigos = new List<Vida>();

    [Header("UI")]
    public Text textoEnemigos;
    public Text textoMensaje;
    public GameObject panelVictoria;

    [Header("Mensajes")]
    public string mensajeFaltanEnemigos = "Elimina a todos los enemigos antes de salir";
    public string mensajeVictoria = "Victoria";

    private int enemigosRestantes;
    private bool jugadorEnMeta = false;
    private bool nivelTerminado = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        
        if (textoMensaje != null)
            textoMensaje.text = "";

        PrepararListaEnemigos();
        ActualizarUIEnemigos();
        RevisarVictoria();
    }

    void OnDestroy()
    {
        DesuscribirEnemigos();
    }

    void PrepararListaEnemigos()
    {
        if (enemigos.Count == 0)
        {
            Vida[] vidaEnEscena = FindObjectsByType<Vida>(FindObjectsSortMode.None);

            foreach (Vida vida in vidaEnEscena)
            {
                if (vida != null && !vida.esJugador)
                    enemigos.Add(vida);
            }
        }

        enemigos.RemoveAll(enemigo => enemigo == null);
        enemigosRestantes = 0;

        foreach (Vida enemigo in enemigos)
        {
            if (enemigo != null && !enemigo.EstaMuerto())
            {
                enemigosRestantes++;
                enemigo.AlMorir += ManejarMuerteEnemigo;
            }
        }
    }

    void DesuscribirEnemigos()
    {
        foreach (Vida enemigo in enemigos)
        {
            if (enemigo != null)
                enemigo.AlMorir -= ManejarMuerteEnemigo;
        }
    }

    void ManejarMuerteEnemigo(Vida enemigo)
    {
        if (enemigo == null || enemigo.esJugador)
            return;

        enemigo.AlMorir -= ManejarMuerteEnemigo;

        enemigosRestantes--;

        if (enemigosRestantes < 0)
            enemigosRestantes = 0;

        ActualizarUIEnemigos();
        RevisarVictoria();
    }

    public void JugadorEntroMeta()
    {
        jugadorEnMeta = true;

        if (enemigosRestantes > 0)
            MostrarMensaje(mensajeFaltanEnemigos);

        RevisarVictoria();
    }

    public void JugadorSalioMeta()
    {
        jugadorEnMeta = false;
        
        if (!nivelTerminado)
            MostrarMensaje("");
    }

    void RevisarVictoria()
    {
        if (nivelTerminado)
            return;
        
        if (jugadorEnMeta && enemigosRestantes <= 0)
            GanarNivel();
    }

    void GanarNivel()
    {
        nivelTerminado = true;
        MostrarMensaje(mensajeVictoria);

        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void ActualizarUIEnemigos()
    {
        if (textoEnemigos != null)
        {
            textoEnemigos.text = "Enemigos: " + enemigosRestantes;
        }
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoMensaje != null)
            textoMensaje.text = mensaje;
    }
}