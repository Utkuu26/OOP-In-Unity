using UnityEngine;
using System.Collections;

public class PlayerPickupHandler : MonoBehaviour
{
    private bool isSlowed = false;
    public GameObject slowMotionVFX;

    public void ActivateSlowMotion()
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowMotionCoroutine());
        }
    }

    private IEnumerator SlowMotionCoroutine()
    {
        isSlowed = true;
        Time.timeScale = 0.8f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (slowMotionVFX != null)
            slowMotionVFX.SetActive(true);

        yield return new WaitForSecondsRealtime(3f); // Gerçek zamanlý 3 saniye bekle

        if (slowMotionVFX != null)
            slowMotionVFX.SetActive(false);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlowed = false;
    }
}
