using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float totalDistance = 0f;
    public Image barFill;
    public TextMeshProUGUI potText;
    private int currentPot = 1;
    private float[] fillRequirements = { 50f, 70f, 100f};

    private void Awake()
    {
        // this is the code to ensure there's only one gameManager in a scene at a time
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //This is the code that carries the GM over between scenes
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        barFill.fillAmount = totalDistance / fillRequirements[currentPot];
    }

    // Update is called once per frame
    void Update()
    {
        barFill.fillAmount = totalDistance / fillRequirements[currentPot];
        potText.text = currentPot.ToString();
    }
}
