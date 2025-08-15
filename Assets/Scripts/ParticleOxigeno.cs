using UnityEngine;

public class ParticleOxigeno : MonoBehaviour
{
    [Header("Configuraci�n de Part�culas")]
    public ParticleSystem targetParticleSystem;

    [Header("Referencia UI")]
    public GameObject reportButton;

    [Header("Configuraci�n de Sonido")]
    public AudioClip oxygenReleaseSound;  // Sonido de ox�geno liber�ndose
    public AudioSource gasAudioSource;    // Componente AudioSource
    [Range(0, 1)] public float gasVolume = 0.6f; // Volumen ajustable

    private void Start()
    {
        // Ocultar el bot�n al inicio
        if (reportButton != null)
        {
            reportButton.SetActive(false);
        }

        // Configuraci�n inicial del audio
        if (gasAudioSource != null && oxygenReleaseSound != null)
        {
            gasAudioSource.clip = oxygenReleaseSound;
            gasAudioSource.loop = true;
            gasAudioSource.volume = gasVolume;
            gasAudioSource.playOnAwake = false;
        }
    }

    public void StartParticles()
    {
        if (targetParticleSystem != null)
        {
            targetParticleSystem.Play();

            // Mostrar el bot�n de reporte
            if (reportButton != null)
            {
                reportButton.SetActive(true);
            }

            // Iniciar sonido de gas
            if (gasAudioSource != null && !gasAudioSource.isPlaying)
            {
                gasAudioSource.Play();
            }
        }
    }

    public void StopParticles()
    {
        if (targetParticleSystem != null)
        {
            targetParticleSystem.Stop();

            // Ocultar el bot�n de reporte
            if (reportButton != null)
            {
                reportButton.SetActive(false);
            }

            // Detener sonido de gas
            if (gasAudioSource != null)
            {
                gasAudioSource.Stop();
            }
        }
    }
}