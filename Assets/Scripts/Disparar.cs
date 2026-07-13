// using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;

public class Disparar : MonoBehaviour
{
    [Header("Disparo")]
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 0.5f;
    
    [Header("Audio y efectos")]
    public AudioClip sonidoDisparo;
    public AudioClip sonidoRecarga;
    public GameObject muzzle;

    [Header("Municion")]
    public int balasMaximas = 10;
    public float tiempoRecarga = 1.5f;
    public KeyCode teclaRecarga = KeyCode.R;

    public event Action<int, int, bool> AlCambiarMunicion;

    private AudioSource fuente;
    private float proximoDisparo = 0f;
    private int balasActuales;
    private bool recargando = false;


    void Awake()
    {
        fuente = GetComponent<AudioSource>();
        balasActuales = balasMaximas;       
    }
    void Start()
    {
        // fuente = GetComponent<AudioSource>();
        if (muzzle != null) 
            muzzle.SetActive(false);
        
        NotificarMunicion();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaRecarga))
            IntentarRecargar();

        if (Input.GetMouseButtonDown(0) && Time.time >= proximoDisparo)
        {
            IntentarDisparar();
        //    proximoDisparo = Time.time + cadencia;
        //    Disparo(); 
        }    
    }

    void IntentarDisparar()
    {
        if (recargando)
            return;

        if (balasActuales <= 0)
        {
            IntentarRecargar();
            return;
        }

        balasActuales--;
        NotificarMunicion();

        proximoDisparo = Time.time + cadencia;
        Disparo();
    }

    void Disparo()
    {
        if (camara == null)
        {
            Debug.LogWarning("No hay camara asignada en Disparar");
            return;
        }

        if (sonidoDisparo != null && fuente != null) 
            fuente.PlayOneShot(sonidoDisparo);
        
        if (muzzle != null) 
        { 
            muzzle.SetActive(true);
            Invoke("ApagarMuzzle", 0.05f);
        }

        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, alcance))
        {
            Vida vida = hit.collider.GetComponentInParent<Vida>();

            if (vida != null) 
                vida.RecibirDano(dano);
        }
    }

    void IntentarRecargar()
    {
        if (recargando)
            return;

        if (balasActuales >= balasMaximas)
            return;
        
        StartCoroutine(Recargar());
    }

    IEnumerator Recargar()
    {
        recargando = true;
        NotificarMunicion();

        if (sonidoRecarga != null && fuente != null)
            fuente.PlayOneShot(sonidoRecarga);
        
        yield return new WaitForSeconds(tiempoRecarga);

        balasActuales = balasMaximas;
        recargando = false;
        NotificarMunicion();
    }

    void ApaagarMuzzle()
    {
        if (muzzle != null)
        {
            muzzle.SetActive(false);
        }
    }

    void NotificarMunicion()
    {
        AlCambiarMunicion?.Invoke(balasActuales, balasMaximas, recargando);
    }

    public int BalasActuales()
    {
        return balasActuales;
    }

    public int BalasMaximas()
    {
        return balasMaximas;
    }

    public bool EstaRecargando()
    {
        return recargando;
    }
}
