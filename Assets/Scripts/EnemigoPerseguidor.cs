// using System.Numerics;
// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoPerseguidor : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAtaque = 6f;
    public float distanciaDeteccion = 30f;
    public float velocidad = 3.5f;
    public float velocidadGiro = 8f;

    private NavMeshAgent agente;
    private Vida vida;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
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

        agente.speed = velocidad;
        agente.stoppingDistance = distanciaAtaque;
    }

    void Update()
    {
        if (jugador == null)
            return;

        if (vida != null && vida.EstaMuerto())
        {
            agente.isStopped = true;
            return;
        }

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia > distanciaDeteccion)
        {
            agente.isStopped = true;
            return;
        }

        if (distancia > distanciaAtaque)
            PerseguirJugador();
        else
            DetenerYMirarJugador();
    }

    void PerseguirJugador()
    {
        agente.isStopped = false;
        agente.SetDestination(jugador.position);
    }

    void DetenerYMirarJugador()
    {
        agente.isStopped = true;
        MirarAlJugador();
    }

    void MirarAlJugador()
    {
        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0f;

        if (direccion.sqrMagnitude <= 0.001f)
            return;

        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotacionObjetivo,
            velocidadGiro * Time.deltaTime
        );
    }
}