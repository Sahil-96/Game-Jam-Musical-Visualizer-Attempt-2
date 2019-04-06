using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//this script will accept all inputs from the player, and send them to the player manager
//it checks movement, and if the player is tryig to raise the bars manually. 
//it also holds the function that moves the bar aswell. 

public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public struct MashPattern
    {
        public string name;    
        
        [UnityEngine.Range(1,5)]
        public float speedIncrease;
    }
    
    public static string keyMashStr;
    public int mashLength;
    
    //public Text KeyMash;

    private float sum;

    [SerializeField]
    private MashPattern[] patterns;
    
    Dictionary<string,float> patternDictionary = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, float> PatternDictionary => patternDictionary;

    public static InputManager instance;

    //public StringBuilder sb = new StringBuilder(10);
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (var item in patterns)
        {
            patternDictionary.Add(item.name,item.speedIncrease);
        }
    }

    private void Update()
    {
        keyMashStr += Input.inputString;
        if (keyMashStr.Length > mashLength)
        {
            keyMashStr = "";
        }
//        KeyMash.text = keyMashStr;

       // print(IncreaseBarWithMash());
    }


    public float IncreaseBarWithMash()
    {
        if (patternDictionary.ContainsKey(keyMashStr))
        {
            string s = keyMashStr;
            keyMashStr = "";
            return patternDictionary[s];
        }

        return 0;
    }
    
    public bool isACycle()
    {
        float xval = Input.GetAxis("HorizontalR");
        float yval = Input.GetAxis("VerticalR");

        float rotationDegree = Mathf.Atan2(yval, xval) * Mathf.Rad2Deg;
        sum += Mathf.Abs(rotationDegree);
//        print(sum);

        if (sum >= 270)
        {
            sum = 0;
            return true;
        }

        return false;
    }
    
    public float MovementInput()
    {
        float xval = Input.GetAxis("Horizontal");
        float yval = Input.GetAxis("Vertical");

        return xval;

//        Debug.Log(xval);
//        obj.transform.Translate(new Vector3(xval,0,0));


    }
}
