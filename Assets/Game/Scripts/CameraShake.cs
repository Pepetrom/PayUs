using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    float shakeDuration = 0f;
    float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        GameManager.instance.mainCamera = this;
    }
    private void Update()
    {
        /*
        // Mantenha a c�mera em sua posi��o original enquanto n�o houver shake.
        if (shakeDuration <= 0f)
        {
            cameraTransform.localPosition = originalPosition;
        }*/
    }

    // Inicie o shake da c�mera com a dura��o e a magnitude especificadas.
    public void StartShake(float duration, float magnitude)
    {
        originalPosition = cameraTransform.localPosition;
        shakeDuration = duration;
        shakeMagnitude = magnitude;

        // Inicie a corrotina para executar o shake.
        StartCoroutine(Shake());
    }

    // Corrotina para realizar o shake da c�mera.
    private IEnumerator Shake()
    {
        while (shakeDuration > 0)
        {
            // Gere um deslocamento aleat�rio com base na magnitude do shake.
            Vector3 randomShake = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalPosition + randomShake;

            // Aguarde um frame e diminua a dura��o do shake.
            yield return null;
            shakeDuration -= Time.deltaTime;
        }

        // Restaure a posi��o original da c�mera quando o shake estiver conclu�do.
        cameraTransform.localPosition = originalPosition;
    }
}
