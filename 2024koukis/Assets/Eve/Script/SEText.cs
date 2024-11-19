using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEText : MonoBehaviour
{
    [SerializeField] GameObject[] SEBar;

    private int oneThird = 33;
    private int twoThird = 66;

    public Text text;

    public int seNum;
    private int minNum = 0;
    private int maxNum = 100;
    public int changeNum = 5;
    // Start is called before the first frame update
    void Start()
    {
        seNum = 50;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "SE\n" + seNum + "%";
        BarController();
    }

    void BarController()
    {
        if (seNum >= twoThird)
        {
            SEBar[0].SetActive(true);
            SEBar[1].SetActive(true);
            SEBar[2].SetActive(true);
        }
        else if (seNum < twoThird && seNum >= oneThird)
        {
            SEBar[0].SetActive(true);
            SEBar[1].SetActive(true);
            SEBar[2].SetActive(false);
        }
        else if (seNum < oneThird && seNum > minNum)
        {
            SEBar[0].SetActive(true);
            SEBar[1].SetActive(false);
            SEBar[2].SetActive(false);
        }
        else if (seNum == minNum)
        {
            SEBar[0].SetActive(false);
            SEBar[1].SetActive(false);
            SEBar[2].SetActive(false);
        }
    }

    public void IncButton()
    {
        if(maxNum>seNum)
        {
            seNum += changeNum;
        }
    }

    public void DecButton()
    {
        if(minNum<seNum)
        {
            seNum -= changeNum;
        }
    }

}
