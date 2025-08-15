using UnityEngine;

public class ParticleHidrogeno : MonoBehaviour
{
    [Header("Configuración de Partículas")]
    public ParticleSystem targetParticleSystem;

    [Header("Referencia UI")]
    public GameObject reportButton;

    [Header("Configuración de Sonido")]
    public AudioClip hydrogenReleaseSound;
    public AudioSource gasAudioSource;
    [Range(0, 1)] public float gasVolume = 0.6f;

    private void Start()
    {
        // Ocultar el botón al inicio
        if (reportButton != null)
        {
            reportButton.SetActive(false);
        }

        // Configuración inicial del audio
        if (gasAudioSource != null && hydrogenReleaseSound != null)
        {
            gasAudioSource.clip = hydrogenReleaseSound;
            gasAudioSource.loop = true;
            gasAudioSource.volume = gasVolume;
        }
    }

    public void StartParticles()
    {
        if (targetParticleSystem != null)
        {
            targetParticleSystem.Play();

            // Mostrar el botón de reporte
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

            // Ocultar el botón de reporte
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