using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timers_Test : MonoBehaviour
{
    [Header("Bar Options")]
    [SerializeField] private bool resetingBar = true;
    [SerializeField] private float barStopValue = .5f;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerNormalizedText;
    [SerializeField] private TextMeshProUGUI[] percentageText;

    [Header("Timers")]
    [SerializeField] private float timerMax;
    private float timer;
    private bool timerBool;
    private float separatorBarTimer;

    [Header("TranfomBar")]
    [SerializeField] private Transform barTransform;
    [Header("ImageBar")]
    [SerializeField] private Image barImage;
    [Header("BarWithPercentage")]
    [SerializeField] private Image barPercetangeImage;
    [Header("CircleBar")]
    [SerializeField] private Image circleBarImage;
    [Header("CutOutMaskBar")]
    [SerializeField] private Image circleBarCutOutMaskImage;
    [Header("CircleBarWithPercentage")]
    [SerializeField] private Image circleBarCutOutPercentMaskImage;
    [SerializeField] private Image circleBarCutOutPercentMaskImage_02;
    [Header("BarWithParticles")]
    [SerializeField] private Slider sliderBar;
    [SerializeField] private ParticleSystem particleSystemSliderBar;
    [Header("SeparatorsBar")]
    [SerializeField] private Image separatorsBarImage;
    [SerializeField] private Transform separatorContainer;
    [Header("FlashingBar")]
    [SerializeField] private Image flashingBarImage;
    [Header("RoundedEndCircleBar")]
    [SerializeField] private Image circleBarRoundedEndImage;
    [SerializeField] private RectTransform barRoundedEndHolder;
    [SerializeField] private Image circleBarRoundedEndImage_02;
    [SerializeField] private RectTransform barRoundedEndHolder_02;
    [Header("GradientImageBar")]
    [SerializeField] private Image GradientImageBarImage;
    [Header("GradientCircleBar")]
    [SerializeField] private Image circleGradientBarImage;
    [Header("GradientCircleRoundedEndBar")]
    [SerializeField] private Image circleGradientRoundedEndBarImage;
    [SerializeField] private RectTransform circleGradientRoundedEndBarHolder;
    [SerializeField] private RectTransform roundedEndColorImag;

    [Header("Tutorial")]
    public Image circleFillImage;
    public RectTransform handlerEdgeImage;
    public RectTransform fillHandler;

    

    private void Awake()
    {
        timer = timerMax;
        separatorBarTimer = timerMax;
    }

    private void Start()
    {
        //Spawning the separators for separatorBar
        Transform separatorTemplate = separatorContainer.Find("SeparatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        float valueAmountPerSeparator = .1f;
        float barSize = separatorContainer.parent.GetComponent<RectTransform>().rect.width;
        float barOneAmountSize = barSize / timerMax;
        int valueSeparatorCount = Mathf.FloorToInt(timerMax / valueAmountPerSeparator);

        for (int i = 1; i < valueSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneAmountSize * i * valueAmountPerSeparator, 0,0);
        }
    }

    private void Update()
    {
        //This code for particles doesn't work that well, it's just to show that it's something that can be done
        if (!resetingBar && GetTimerNormalized() <= barStopValue)
        {
            particleSystemSliderBar.Stop();
        }
        else
        {
            timer -= Time.deltaTime;
            particleSystemSliderBar.Play();
        }

        if (timer <= 0 && resetingBar)
        {
            timer += timerMax;
        }

        //Transform bar
        barTransform.localScale = new Vector3(GetTimerNormalized(), 1, 1);

        //Image.fillAmount bar
        barImage.fillAmount = GetTimerNormalized();

        //Image.fillAmount bar with percentage text
        barPercetangeImage.fillAmount = GetTimerNormalized();

        //Image.fillAmount circle
        circleBarImage.fillAmount = GetTimerNormalized();

        //Image.fillAmount circle with Cutout mask
        circleBarCutOutMaskImage.fillAmount = GetTimerNormalized();

        //Image.fillAmount circle with Cutout mask_02
        circleBarCutOutPercentMaskImage_02.fillAmount = GetTimerNormalized();

        //Image.fillAmount circle with Cutout mask and percentage in middle
        circleBarCutOutPercentMaskImage.fillAmount = GetTimerNormalized();

        //Slider.value bar
        sliderBar.value = GetTimerNormalized();
        if (timerBool)
        {
            particleSystemSliderBar.Stop();
        }
        else
        {
            particleSystemSliderBar.Play();
        }

        //Image.fill amount bar with separators
        //separatorsBarImage.fillAmount = GetTimerNormilzed();        
        separatorsBarImage.fillAmount = GetSeparatorTimerNormalized();        

        //Image.fill amount bar with flashing
        flashingBarImage.fillAmount = GetTimerNormalized();

        if (GetTimerNormalized() <= 0.5f)
        {
            int flashEvery = 3;
            if ((int)(timer * 100) % flashEvery == 0)
            {
                flashingBarImage.color = new Color(0.3349057f, 0.6265489f, 1, 1);
            }
            else
            {                
                flashingBarImage.color = Color.white;
            }
        }
        else
        {
            flashingBarImage.color = Color.white;
        }

        //Image.fill amount circle bar with rounded ends
        FillCircleValue();
        RoundedEndCircleBar(circleBarRoundedEndImage, barRoundedEndHolder);
        RoundedEndCircleBar(circleBarRoundedEndImage_02, barRoundedEndHolder_02);

        //Image.fillanount with gradient Image 
        GradientImageBarImage.fillAmount = GetTimerNormalized();

        //Image.fillamount circle bar with gradient Image
        circleGradientBarImage.fillAmount = GetTimerNormalized();

        //Image.fillamount circle gradient Image with rounded end bar
        RoundedEndCircleBar(circleGradientRoundedEndBarImage, circleGradientRoundedEndBarHolder, roundedEndColorImag, true);

        //Setting Texts
        timerText.SetText("Timer: " + timer.ToString("N1"));
        timerNormalizedText.SetText("Normalized timer: " + GetTimerNormalized().ToString("N1"));

        for (int i = 0; i < percentageText.Length; i++)
        {
            percentageText[i].SetText(GetPercetangeTimerNormalized().ToString("N0") + "%");
        }
    }

    public float GetTimerNormalized()
    {
        return timerBool ? 1 - timer / timerMax : timer / timerMax;
    }

    private float GetPercetangeTimerNormalized()
    {
        return timerBool ? (1 - timer / timerMax) * 100 : (timer / timerMax) * 100;
    }

    private float GetSeparatorTimerNormalized()
    {
        return separatorBarTimer / timerMax;
    }

    public void TimerBoolToggle(bool val)
    {
        timerBool = val;
    }

    public void PlusToSeparatorTimer()
    {
        if (separatorBarTimer < timerMax)
        {
            separatorBarTimer += .1f;
        }
    }

    public void MinusToSeparatorTimer()
    {
        if (separatorBarTimer > 0)
        {
            separatorBarTimer -= .1f;
        }

        if (separatorBarTimer <= 0)
        {
            separatorBarTimer = 0;
        }
    }

    //from this tutorial https://www.youtube.com/watch?v=4PL92mSdNsk&t=185s
    private void FillCircleValue()
    {
        float fillAmount = GetTimerNormalized();
        circleFillImage.fillAmount = fillAmount;
        float angle = fillAmount * 360;

        //Moves the rounded end 
        fillHandler.localEulerAngles = new Vector3(0, 0, -angle);

        //Moves the color
        handlerEdgeImage.localEulerAngles = new Vector3(0, 0, angle);
    }
    private void RoundedEndCircleBar(Image barImage, RectTransform roundedEndHolder, RectTransform roundedEndColorImage = null, bool useRoundedEndColorImage = false)
    {
        float fillAmount = GetTimerNormalized();
        barImage.fillAmount = fillAmount;
        float angle = fillAmount * 360;

        //Rotates the holder and that moves the roundedEnd
        //Angle must be minus or it won't be rotating with the fill amount
        roundedEndHolder.localEulerAngles = new Vector3(0, 0, -angle);

        //colorImage moves with the holder so this script
        //counteracts the rotation of holder, so that the color is shown properly
        if (useRoundedEndColorImage)
        {
            roundedEndColorImage.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
