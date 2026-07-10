// using Unity.VisualScripting;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 0.5f;
    public AudioClip sonidoDisparo;
    public GameObject muzzle;

    private AudioSource fuente;
    private float proximoDisparo = 0f;

    void Start()
    {
        fuente = GetComponent<AudioSource>();
        if (muzzle != null) 
            muzzle.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximoDisparo)
        {
           proximoDisparo = Time.time + cadencia;
           Disparo(); 
        }    
    }

    void Disparo()
    {
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

    void ApaagarMuzzle()
    {
        if (muzzle != null)
        {
            muzzle.SetActive(false);
        }
    }
}
