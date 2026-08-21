using UnityEngine;
using UnityEngine.InputSystem;

public class CombateJugador : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public Transform controladorAtaque; 
    public float radioAtaque = 0.5f;     
    public float danoAtaque = 25f;       
    public LayerMask capaEnemigo;        

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