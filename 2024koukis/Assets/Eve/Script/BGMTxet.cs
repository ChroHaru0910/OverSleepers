using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTxet : MonoBehaviour
{
    [SerializeField] GameObject[] BGMBar;

    private int oneThird = 33;
    private int twoThird = 66;

    public Text text;

    private int bgmNum;
    private int minNum = 0;
    private int maxNum = 100;
    private int changeNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        bgmNum = 50;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "BGM\n" + bgmNum + "%";

        BarController();
    }

    void BarController()
    {
        if (bgmNum >= twoThird)
        {
            BGMBar[0].SetActive(true);
            BGMBar[1].SetActive(true);
            BGMBar[2].SetActive(true);
            Debug.Log("67%à»è„");
        }
        else if (bgmNum < twoThird && bgmNum >= oneThird)
        {
            BGMBar[0].SetActive(true);
            BGMBar[1].SetActive(true);
            BGMBar[2].SetActive(false);
            Debug.Log("34%à»è„");
        }
        else if (bgmNum < oneThird && bgmNum > minNum)
        {
            BGMBar[0].SetActive(true);
            BGMBar[1].SetActive(false);
            BGMBar[2].SetActive(false);
            Debug.Log("1%à»è„");
        }
        else if (bgmNum == minNum)
        {
            BGMBar[0].SetActive(false);
            BGMBar[1].SetActive(false);
            BGMBar[2].SetActive(false);
            Debug.Log("0%");
        }
    }

    public void IncButton()
    {
        if (maxNum > bgmNum)
        {
            bgmNum += changeNum;
        }
    }

    public void DecButton()
    {
        if (minNum < bgmNum)
        {
            bgmNum -= changeNum;
        }
    }
}
