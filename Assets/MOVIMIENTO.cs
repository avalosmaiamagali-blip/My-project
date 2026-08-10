using UnityEngine;
using UnityEngine.InputSystem; // Importante para el nuevo Input System

public class Movimiento2D : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 5f;

    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lee las entradas del teclado/mando usando el nuevo Input System
        float entradaX = 0f;
        float entradaY = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) entradaX += 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) entradaX -= 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) entradaY += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) entradaY -= 1f;
        }

        movimiento = new Vector2(entradaX, entradaY).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);
    }
}