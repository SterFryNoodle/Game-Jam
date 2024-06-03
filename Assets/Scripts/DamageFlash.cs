using UnityEngine;
using UnityEngine.UI;  // For accessing the UI components like Image
using System.Collections;  // This namespace is required for using IEnumerator

public class DamageEffect : MonoBehaviour
{
    public Image flashImage;  // Reference to the UI Image that will flash
    public float flashTime = 0.5f;  // Duration of the flash effect

    private void Start()
    {
        if (flashImage != null)
            flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);  // Ensure it's transparent initially
    }

    public void FlashScreen()
    {
        if (flashImage != null)
            StartCoroutine(DoFlash());
    }

    private IEnumerator DoFlash()
    {
        // Set alpha to 0.25 to make the image visible
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0.25f);
        // Wait for flashTime seconds
        yield return new WaitForSeconds(flashTime);
        // Set alpha back to 0 to make the image invisible again
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0f);
    }
}
