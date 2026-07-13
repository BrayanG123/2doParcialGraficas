using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("Jugador")]
    public Vida vidaJugador;

    [Header("UI")]
    public GameObject panelGameOver;
    public Text textoGameOver;
    public Button botonReintentar;

    [Header("Mensaje")]
    public string mensajeGameOver = "Game Over";

    private bool gameOverActivo = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (vidaJugador == null)
            BuscarVidaJugador();

        if (vidaJugador != null)
            vidaJugador.AlMorir += MostrarGameOver;

        if (panelGameOver != null)
            panelGameOver.SetActive(false);
        
        if (textoGameOver != null)
            textoGameOver.text = mensajeGameOver;

        if (botonReintentar != null)
            botonReintentar.onClick.AddListener(Reintentar);
    }

    void OnDestroy()
    {
        if (vidaJugador != null)
            vidaJugador.AlMorir -= MostrarGameOver;

        if (botonReintentar != null)
            botonReintentar.onClick.RemoveListener(Reintentar);
    }

    void BuscarVidaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
            vidaJugador = jugador.GetComponent<Vida>();
    }

    void MostrarGameOver(Vida vidaMuerta)
    {
        if (gameOverActivo)
            return;

        if (vidaMuerta == null || !vidaMuerta.esJugador)
            return;

        gameOverActivo = true;

        if (panelGameOver != null)
            panelGameOver.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}