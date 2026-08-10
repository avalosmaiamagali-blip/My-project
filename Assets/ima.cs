using UnityEngine;
using UnityEngine.SceneManagement; // Importante para gestionar escenas

public class CambiarEscena : MonoBehaviour
{
    // Método público para que el botón pueda detectarlo
    public void CargarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}