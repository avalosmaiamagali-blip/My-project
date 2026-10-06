using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Instancia pública para acceder desde cualquier script (Singleton)
    public static AudioManager Instance { get; private set; }

    [Header("---- Audio Sources ----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("---- Audio Clips ----")]
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- MÉTODOS PARA REPRODUCIR ---

    /// <summary>
    /// Reproduce un tema de música de fondo por su nombre.
    /// </summary>
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Música '{name}' no encontrada.");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary>
    /// Reproduce un efecto de sonido (SFX) por su nombre.
    /// </summary>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning($"SFX '{name}' no encontrado.");
            return;
        }

        // PlayOneShot permite superponer varios efectos de sonido sin cortarlos
        sfxSource.PlayOneShot(s.clip);
    }

    // --- MÉTODOS DE CONTROL DE VOLUMEN Y SILENCIO ---

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

// Clase auxiliar para mapear el nombre con el archivo de audio
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}