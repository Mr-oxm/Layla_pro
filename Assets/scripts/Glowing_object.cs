using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowing_object : MonoBehaviour
{
    public float speed = 0.5f; // Speed of the color change
    private SpriteRenderer spriteRenderer;
    public float brightness = 1.0f; // Initial brightness value (1.0 corresponds to 100% brightness)
    public float brightnessMin = 0.85f; // Initial brightness value (1.0 corresponds to 100% brightness)
    private bool increasing = false; // Flag to indicate whether brightness is increasing or decreasing

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the new brightness value based on the speed and time
        brightness += (increasing ? 1 : -1) * speed * Time.deltaTime;

        // Clamp the brightness value between 0.85 and 1.0
        brightness = Mathf.Clamp(brightness, brightnessMin, 1.0f);

        // Set the new color with adjusted brightness
        Color newColor = Color.HSVToRGB(0,  0, brightness);
        spriteRenderer.color = newColor;

        // If brightness reaches 0.85 or 1.0, change the direction of the change
        if (brightness <= brightnessMin || brightness >= 1.0f)
        {
            increasing = !increasing;
        }
    }
}
