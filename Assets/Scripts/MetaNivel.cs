using UnityEngine;

public class MetaNivel : MonoBehaviour
{
    public ControladorNivel controladorNivel;

    void Start()
    {
        if (controladorNivel == null)
            controladorNivel = FindFirstObjectByType<ControladorNivel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (controladorNivel != null)
            controladorNivel.JugadorEntroMeta();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (controladorNivel != null)
            controladorNivel.JugadorSalioMeta();
    }
}