using UnityEngine;

public class SaludEnemigo : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float vidaMaxima = 50f;
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibió " + cantidad + " de daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("¡El enemigo ha sido derrotado!");
        Destroy(gameObject); // Elimina al monstruo de la escena
    }
}