using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider barraDeVida;

    [Header("Configuración de UI")]
    public GameObject textoGameOver; // Arrastra tu texto de UI aquí desde la jerarquía

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

        // Asegurarse de que el texto esté oculto al inicio
        if (textoGameOver != null)
        {
            textoGameOver.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
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

        // Muestra el texto "Te moriste"
        if (textoGameOver != null)
        {
            textoGameOver.SetActive(true);
        }

        // Desactiva el Sprite y los controles para que no siga moviéndose,
        // sin destruir el objeto completo para evitar errores de referencias
        GetComponent<SpriteRenderer>().enabled = false;
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        MonoBehaviour movimiento = GetComponent("Movimiento2D") as MonoBehaviour;
        if (movimiento != null) movimiento.enabled = false;
    }
}