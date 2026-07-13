using UnityEngine;

[RequireComponent(typeof(Vida))]
public class EnemigoDisparador : MonoBehaviour
{
    public Transform jugador;
    public Transform puntoDisparo;

    public int dano = 1;
    public float alcance = 30f;
    public float distanciaAtaque = 6f;
    public float cadencia = 1.2f;
    public float alturaObjetivo = 1f;

    public LayerMask capasImpacto = ~0;

    public AudioClip sonidoDisparo;
    public GameObject muzzle;

    private AudioSource fuente;
    private Vida vida;
    private float proximoDisparo = 0f;

    void Awake()
    {
        fuente = GetComponent<AudioSource>();
        vida = GetComponent<Vida>();
    }

    void Start()
    {
        if (jugador == null)
        {
            GameObject jugadorEncontrado = GameObject.FindGameObjectWithTag("Player");

            if (jugadorEncontrado != null)
                jugador = jugadorEncontrado.transform;
        }

        if (muzzle != null)
            muzzle.SetActive(false);
    }

    void Update()
    {
        if (jugador == null)
            return;

        if (vida != null && vida.EstaMuerto())
            return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia > distanciaAtaque)
            return;

        if (Time.time < proximoDisparo)
            return;

        DispararAlJugador();
    }

    void DispararAlJugador()
    {
        proximoDisparo = Time.time + cadencia;

        Vector3 origen = ObtenerOrigenDisparo();
        Vector3 objetivo = jugador.position + Vector3.up * alturaObjetivo;
        Vector3 direccion = objetivo - origen;

        if (direccion.sqrMagnitude <= 0.001f)
            return;

        if (sonidoDisparo != null && fuente != null)
            fuente.PlayOneShot(sonidoDisparo);

        if (muzzle != null)
        {
            muzzle.SetActive(true);
            Invoke(nameof(ApagarMuzzle), 0.05f);
        }

        if (Physics.Raycast(origen, direccion.normalized, out RaycastHit hit, alcance, capasImpacto))
        {
            Vida vidaGolpeada = hit.collider.GetComponentInParent<Vida>();

            if (vidaGolpeada != null && vidaGolpeada.esJugador)
            {
                vidaGolpeada.RecibirDano(dano);
            }
        }
    }

    Vector3 ObtenerOrigenDisparo()
    {
        if (puntoDisparo != null)
            return puntoDisparo.position;

        return transform.position + Vector3.up * 1.2f;
    }

    void ApagarMuzzle()
    {
        if (muzzle != null)
            muzzle.SetActive(false);
    }
}