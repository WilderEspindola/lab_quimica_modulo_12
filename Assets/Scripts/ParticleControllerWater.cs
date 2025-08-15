using UnityEngine;

public class ParticleControllerWater : MonoBehaviour
{
    [Header("Configuraci�n de Part�culas")]
    public ParticleSystem targetParticleSystem; // Nombre m�s espec�fico

    [Header("Configuraci�n de Sonido")]
    public AudioClip waterSound;
    public AudioSource waterAudioSource;
    [Range(0, 1)] public float soundVolume = 0.7f;

    private void Start()
    {
        // Configuraci�n inicial del audio
        if (waterAudioSource != null && waterSound != null)
        {
            waterAudioSource.clip = waterSound;
            waterAudioSource.loop = true;
            waterAudioSource.volume = soundVolume;
        }
    }

    public void StartParticles()
    {
        if (targetParticleSystem != null)
        {
            targetParticleSystem.Play();

            // Iniciar sonido del agua
            if (waterAudioSource != null && !waterAudioSource.isPlaying)
            {
                waterAudioSource.Play();
            }
        }
    }

    public void StopParticles()
    {
        if (targetParticleSystem != null)
        {
            targetParticleSystem.Stop();

            // Detener sonido del agua
            if (waterAudioSource != null)
            {
                waterAudioSource.Stop();
            }
        }
    }
}