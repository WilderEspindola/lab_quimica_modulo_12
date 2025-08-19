using UnityEngine;
using TMPro;

public class ParticleControllerFire : MonoBehaviour
{
    [Header("Control de Fuego")]
    public ParticleSystem targetParticleSystemFire;

    [Header("Configuración de Temperatura")]
    public TextMeshProUGUI temperatureDisplay; // Asignar el texto "X (°K)"
    public float minTemperature = 298f; // 298K = 25°C
    public float maxTemperature = 2500f; // 800K máximo
    public float heatingRate = 30f;     // Grados K por segundo al calentar
    public float coolingRate = 20f;     // Grados K por segundo al enfriar

    [Header("Eventos de Cambio de Temperatura")]
    public System.Action<float> OnTemperatureChanged; // Temperatura en Kelvin

    [Header("Efectos de Sonido")]
    public AudioClip fireSound;          // Sonido del fuego (arrastrar el clip desde el Inspector)
    public AudioSource fireAudioSource;  // AudioSource asignado al mismo objeto
    [Range(0, 1)] public float fireVolume = 0.5f; // Volumen ajustable

    private float currentKelvin;
    private bool isHeating = false;

    void Start()
    {
        currentKelvin = minTemperature;
        UpdateTemperatureDisplay();

        // Precarga el sonido
        if (fireAudioSource != null && fireSound != null)
        {
            fireAudioSource.clip = fireSound;
            fireAudioSource.loop = true;
            fireAudioSource.volume = fireVolume;
        }
    }

    void Update()
    {
        if (isHeating)
        {
            // Modo calentamiento
            currentKelvin = Mathf.Min(currentKelvin + heatingRate * Time.deltaTime, maxTemperature);
        }
        else
        {
            // Modo enfriamiento
            currentKelvin = Mathf.Max(currentKelvin - coolingRate * Time.deltaTime, minTemperature);
        }

        UpdateTemperatureDisplay();
    }

    public void StartParticles()
    {
        if (targetParticleSystemFire != null)
        {
            targetParticleSystemFire.Play();
        }

        // Inicia sonido
        if (fireAudioSource != null && !fireAudioSource.isPlaying)
        {
            fireAudioSource.Play();
        }

        isHeating = true;
    }

    public void StopParticles()
    {
        if (targetParticleSystemFire != null)
        {
            targetParticleSystemFire.Stop();
        }

        // Detiene sonido
        if (fireAudioSource != null)
        {
            fireAudioSource.Stop();
        }

        isHeating = false;
    }

    // Modifica UpdateTemperatureDisplay para notificar cambios
    private void UpdateTemperatureDisplay()
    {
        if (temperatureDisplay != null)
        {
            temperatureDisplay.text = $"{Mathf.RoundToInt(currentKelvin)} (°K)";
            float tempRatio = Mathf.InverseLerp(minTemperature, maxTemperature, currentKelvin);
            temperatureDisplay.color = Color.Lerp(Color.white, Color.red, tempRatio);

            // Notificar el cambio de temperatura
            OnTemperatureChanged?.Invoke(currentKelvin);
        }
    }


    // Método para acceder a la temperatura actual
    public float GetCurrentTemperature()
    {
        return currentKelvin;
    }
}