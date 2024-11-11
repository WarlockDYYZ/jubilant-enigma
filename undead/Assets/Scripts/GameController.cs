using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance{  get; private set; }
    public int totalLevel = 3;
    public int level = 1;
    public List<float> TimeLimit;
    float levelTime;
    float currentTime = 0;

    int EnemyCount;

    public Slider slider;
    public GameObject TimeText;
    public GameObject EnemyNum;
    public GameObject LevelText;
    public GameObject WinImage;
    public GameObject DeadImage;


    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        initUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        

        currentTime += Time.deltaTime;

        updateTimeSlider(currentTime);

        updateTimeText(levelTime - currentTime);


        if (currentTime >= levelTime)
        {
            winGame();
        }
    }

    
        void updateTimeSlider(float time)
    {
        slider.value = time;
    }


    void initTimeSlider(float time)
    {
        slider.maxValue = time;
    }


    void initUI() 
    {
        GameObject UISystem = GameObject.Find("UI System");
        Transform TimeSlider = UISystem.transform.Find("TimeSlider");
        slider = TimeSlider.GetComponent<Slider>();
        TimeText = UISystem.transform.Find("TimeText").gameObject;
        EnemyNum = UISystem.transform.Find("EnemyNum").gameObject;
        LevelText = UISystem.transform.Find("LevelText").gameObject;
        WinImage = UISystem.transform.Find("WinImage").gameObject;
        DeadImage = UISystem.transform.Find("DeadImage").gameObject;


        WinImage.SetActive(false);
        DeadImage.SetActive(false);

        levelTime = TimeLimit[level - 1];
        
        initTimeSlider(levelTime);

        updateTimeText(levelTime);

        initEnemyCount();
    }


    void updateTimeText(float time)
    {
        TimeText.GetComponent<Text>().text = formatTime(time);
    }


    public void minusEnemCount()
    {

        EnemyCount--;
        EnemyNum.GetComponent<Text>().text = EnemyCount.ToString();

        if (EnemyCount == 0)
        {
            winGame();
        }
    }


    void initEnemyCount()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        EnemyNum.GetComponent<Text>().text = EnemyCount.ToString();
    }


    string formatTime(float time)
    {

        int second = (int)time;
        int minute = second / 60;
        second = second % 60;
        return (minute < 10 ? "0" + minute : minute) + ":" +
               (second < 10 ? "0" + second : second);
    }



    public void winGame()
    {
        isGameOver = true;
        Debug.Log("Win.");
        WinImage.SetActive(true);
    }


    public void loseGame()
    {
        isGameOver = true;
        Debug.Log("Fail.");
        DeadImage.SetActive(true);
    }
}