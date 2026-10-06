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
    [HideInInspector]
    public bool bloqueando = false;

    void Update()
    {
        // Bloqueo: mantener la tecla Q para bloquear
        if (Keyboard.current != null)
        {
            bloqueando = Keyboard.current.qKey.isPressed;
        }

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

    // Devuelve true si actualmente bloqueando y el enemigo está dentro del area frontal (controladorAtaque)
    public bool EstaBloqueandoA(Collider2D enemigo)
    {
        if (!bloqueando || controladorAtaque == null || enemigo == null) return false;

        // Comprobar distancia entre el punto de bloqueo (controladorAtaque) y el enemigo
        float distancia = Vector2.Distance(controladorAtaque.position, enemigo.transform.position);
        return distancia <= radioAtaque;
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