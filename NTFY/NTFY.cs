
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class NTFY : UdonSharpBehaviour
{
    //NTFY System

    //UI
    public Image ntfyIcon;
    public TextMeshProUGUI ntfyPlayername;
    public GameObject ntfyPN;

    [SerializeField] private Color NTFYColor;
    //SYSTEM
    bool ntfy;
    bool isTimerRunning;
    float remainingTime;
    public float TotalTimeRemaining; // Public float, for user to set custom Total On-Screen time for NTFY System.
    private float ntfyColorFloat = 0f;

    public void Start()
    {
        //Setting NTFY Color Default settings.
        NTFYColor.a = 0.05f;
        NTFYColor.r = 0;
        NTFYColor.g = 50;
        NTFYColor.b = 50;
    }

    public void Update()
    {
        if (isTimerRunning && ntfy) //Checks that the Timer is running, and NTFY Bool has been set to TRUE.
        {
            remainingTime -= Time.deltaTime; //Subtracting from overall timer.
            ntfyColorFloat = Mathf.PingPong(Time.time, 0.6f); // NTFY Icon will "Pulse" while icon is enabled.
            //Setting Color Values.
            NTFYColor.a = ntfyColorFloat;
            ntfyIcon.color = NTFYColor;
            //Enabling GameObject.
            ntfyPN.SetActive(true);

            if (remainingTime < 0.0f) //When Timer hits approx~ 0 it will stop.
            {
                isTimerRunning = false;
                //Setting Color Values.
                NTFYColor.a = 0f;
                ntfyIcon.color = NTFYColor;
                //Disabling GameObject.
                ntfyPN.SetActive(false);
                ntfy = false; //Setting Bool to FALSE.
            }
        }
    }
    public void NTFYFunction()
    {
        //Setting Color Values.
        NTFYColor.r = 0;
        NTFYColor.g = 0;
        NTFYColor.b = 20;
        ntfyIcon.color = NTFYColor;
        remainingTime = TotalTimeRemaining; //Timing Setup.
        ntfy = true;
        isTimerRunning = true;
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        NTFYFunction();
        ntfyPlayername.color = Color.white;
        ntfyPlayername.text = player.displayName + "";
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        NTFYFunction();
        NTFYColor.a = 0f;
        ntfyPlayername.color = Color.red;
        ntfyPlayername.text = player.displayName + "";
    }

    }
