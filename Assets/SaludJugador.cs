using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider barraDeVida;

    [Header("Configuración de Daño")]
    [Tooltip("Cantidad de vida que pierde por segundo mientras el enemigo lo toca")]
    public float danoPorSegundo = 25f; 

    void Start()
    {
        vidaActual = vidaMaxima;

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaActual;
        }
    }

    // Se ejecuta de manera continua MIENTRAS el enemigo esté en contacto con Juancito
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            // Resta daño proporcional al tiempo transcurrido en el frame
            RecibirDano(danoPorSegundo * Time.deltaTime);
        }
    }

    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual;
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("¡Juancito ha muerto!");
        Destroy(gameObject);
    }
}