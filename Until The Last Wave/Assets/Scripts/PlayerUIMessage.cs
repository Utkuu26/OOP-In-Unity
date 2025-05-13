using UnityEngine;
using TMPro;

public class PlayerUIMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    private float duration = 2f;

    public void ShowMessage(string msg)
    {
        CancelInvoke();
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        Invoke(nameof(HideMessage), duration);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
