using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FeedbackDanoUI : MonoBehaviour
{
    [Header("Jugador")]
    public Vida vidaJugador;

    [Header("UI")]
    public Image imagenDano;

    [Header("Efecto")]
    public Color colorDano = new Color(1f, 0f, 0f, 0.45f);
    public float duracion = 0.35f;

    private Coroutine rutinaActual;

    void Start()
    {
        if (vidaJugador == null)
            BuscarVidaJugador();

        if (vidaJugador != null)
            vidaJugador.AlRecibirDano += MostrarFeedbackDano;

        OcultarImagen();
    }

    void OnDestroy()
    {
        if (vidaJugador != null)
            vidaJugador.AlRecibirDano -= MostrarFeedbackDano;
    }

    void BuscarVidaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
            vidaJugador = jugador.GetComponent<Vida>();
    }

    void MostrarFeedbackDano(Vida vidaDanada, int cantidad)
    {
        if (vidaDanada == null || !vidaDanada.esJugador)
            return;

        if (imagenDano == null)
            return;

        if (rutinaActual != null)
            StopCoroutine(rutinaActual);

        rutinaActual = StartCoroutine(AnimarDano());
    }

    IEnumerator AnimarDano()
    {
        imagenDano.color = colorDano;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.unscaledDeltaTime;

            float progreso = tiempo / duracion;
            float alpha = Mathf.Lerp(colorDano.a, 0f, progreso);

            Color colorActual = colorDano;
            colorActual.a = alpha;
            imagenDano.color = colorActual;

            yield return null;
        }

        OcultarImagen();
        rutinaActual = null;
    }

    void OcultarImagen()
    {
        if (imagenDano == null)
            return;

        Color colorTransparente = colorDano;
        colorTransparente.a = 0f;
        imagenDano.color = colorTransparente;
    }
}