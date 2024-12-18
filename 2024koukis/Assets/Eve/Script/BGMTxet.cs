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

    public int bgmNum;
    
    private int minNum = 0;
    private int maxNum = 100;
    public int changeNum = 5;


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
        }
        else if (bgmNum < twoThird && bgmNum >= oneThird)
        {
            BGMBar[0].SetActive(true);
            BGMBar[1].SetActive(true);
            BGMBar[2].SetActive(false);
        }
        else if (bgmNum < oneThird && bgmNum > minNum)
        {
            BGMBar[0].SetActive(true);
            BGMBar[1].SetActive(false);
            BGMBar[2].SetActive(false);
        }
        else if (bgmNum == minNum)
        {
            BGMBar[0].SetActive(false);
            BGMBar[1].SetActive(false);
            BGMBar[2].SetActive(false);
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
