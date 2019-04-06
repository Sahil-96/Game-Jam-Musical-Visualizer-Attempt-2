using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this gives the time of when each musical node is going to raise and fall to the Grow on Beats script

public class BPeerM : MonoBehaviour
{
    public static BPeerM instance;

    public float bpm;
    private float beatInterval, beatIntervalD8;
    private float beatTimer, beatTimerD8;
    public static bool beatFull;
    public static int beatCountFull;
    public static bool beatD8;

    public static int beatCountD8;
    

    private int note = -1;
    private int transpose = -4;

    public AudioSource clip;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
    }

    void BeatDetection()
    {
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            Debug.Log("Full");
        }

        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;

        if (beatIntervalD8 <= beatTimerD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatCountD8++;
            Debug.Log("D8");
        }
    }


}
