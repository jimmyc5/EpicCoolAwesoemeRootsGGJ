using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float totalDistance = 0f;
    public Image barFill;
    public TextMeshProUGUI potText;
    private int currentPot = 1;
    private float[] fillRequirements = { 1f, 2f, 40f, 900f};
    private bool restarting = false;

    public Animator potGraphics;

    //time to transition between scenes
    public float transitionTime = 2f;

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
        //DontDestroyOnLoad(gameObject);
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

        if(totalDistance > fillRequirements[currentPot])
        {
            currentPot++;
            potGraphics.SetInteger("Pot", currentPot);
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (!restarting)
            {
                restarting = true;
                RestartScene();
            }
        }
    }
    public void RestartScene()
    {

        //fade out
        ScreenFade sf = FindObjectOfType<ScreenFade>();
        if (sf)
        {
            sf.FadeOut();
        }
        // restart level, make sure to reset the liquid (water/lava) levels to what they were when the scene started
        StartCoroutine(LoadLevelFromName(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevelFromName(string sceneName)
    {
        yield return new WaitForSeconds(transitionTime);

        // to be called when restarting only: set water/lava back to what they were when the scene began


        SceneManager.LoadScene(sceneName);
        restarting = false;
    }
}
