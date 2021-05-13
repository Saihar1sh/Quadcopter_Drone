using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Gradient))]
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider throttleSlider;

    public Gradient gradient;

    [SerializeField]
    private Image throttleFill;

    [SerializeField]
    private TextMeshProUGUI forceText;

    [SerializeField]
    private Toggle invertXtoggle, invertYtoggle;

    [SerializeField]
    private DroneMovementController droneMovement;

    private void Awake()
    {
    }
    private void Update()
    {
        droneMovement.invertPitch = invertYtoggle.isOn ? 1f : -1f;
        droneMovement.invertRoll = invertXtoggle.isOn ? 1f : -1f;
    }
    public void SetThrottleMaxValue()
    {
        throttleSlider.minValue = 4f;
        throttleSlider.maxValue = 7.4f;
        throttleSlider.value = 0;
        throttleFill.color = gradient.Evaluate(0);
    }
    public void SetThrottleValue(float value)
    {
        throttleSlider.value = value;
        forceText.text = value.ToString();
        throttleFill.color = gradient.Evaluate(throttleSlider.normalizedValue);
    }
}
