using UnityEngine;
using UnityEngine.InputSystem;

public class CombateJugador : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public Transform controladorAtaque; 
    public float radioAtaque = 0.5f;     
    public float danoAtaque = 25f;       
    public LayerMask capaEnemigo;        

    [Header("Efecto visual de ataque")]
    [Tooltip("Prefab del efecto (partículas, sprite, etc.) que se instanciará en la zona de ataque")]
    public GameObject efectoAtaque;
    [Tooltip("Tiempo en segundos antes de destruir el efecto instanciado")]
    public float duracionEfecto = 0.6f;

    [Header("Tiempo entre Ataques")]
    public float tiempoEntreAtaques = 0.5f;
    private float tiempoSiguienteAtaque = 0f;

    void Update()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            // Detecta la tecla E
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                Atacar();
                tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
            }
        }
    }

    private void Atacar()
    {
        // Instanciar efecto visual en la posición del controlador de ataque
        if (efectoAtaque != null && controladorAtaque != null)
        {
            GameObject go = Instantiate(efectoAtaque, controladorAtaque.position, Quaternion.identity);
            Destroy(go, duracionEfecto);
        }

        Collider2D[] objetosDetectados = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque, capaEnemigo);

        foreach (Collider2D enemigo in objetosDetectados)
        {
            SaludEnemigo saludEnemigo = enemigo.GetComponent<SaludEnemigo>();
            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDano(danoAtaque);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (controladorAtaque == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}