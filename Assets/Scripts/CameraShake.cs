using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    float shakeDuration = 0.1f;
    float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        GameManager.instance.cameraShake = this;
    }
    public void SmallShake()
    {
        StartShake(0.1f, 0.01f);
    }
    public void StartShake(float duration, float magnitude)
    {
        originalPosition = cameraTransform.localPosition;
        shakeDuration = duration;
        shakeMagnitude = magnitude;

        // Inicie a corrotina para executar o shake.
        StartCoroutine(Shake());
    }

    // Corrotina para realizar o shake da câmera.
    private IEnumerator Shake()
    {
        while (shakeDuration > 0)
        {
            // Gere um deslocamento aleatório com base na magnitude do shake.
            Vector3 randomShake = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalPosition + randomShake;

            // Aguarde um frame e diminua a duração do shake.
            yield return null;
            shakeDuration -= Time.deltaTime;
        }

        // Restaure a posição original da câmera quando o shake estiver concluído.
        cameraTransform.localPosition = originalPosition;
    }
}
