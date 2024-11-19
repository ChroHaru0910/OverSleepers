using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    // フェード処理するテキスト
    [SerializeField] Text text;
    // 処理内容
    ColFade colFade;
    // 明滅速度
    [SerializeField] float spdFade;
    // 色指定
    [SerializeField] float r, g, b;

    // シート切り替えのフェード
    [SerializeField] Image img;
    // 明滅速度
    [SerializeField] float spdImg;
    bool flg = false; 

    // 次のシーン名
    [SerializeField] string sceneName;
    void Start()
    {
        // インスタンス生成
        colFade = new ColFade();
    }

    // Update is called once per frame
    void Update()
    {
        colFade.FadeINOUT(text, r, g, b, spdFade);
        if(Input.anyKeyDown)
        {
            flg = true;   
        }
        if(flg)
        {
            colFade.ImgFade(img, spdImg);
            if (img.color.a >= 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
