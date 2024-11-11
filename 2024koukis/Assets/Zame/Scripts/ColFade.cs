using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColFade 
{
    // 明滅
    float textAlpha=1.0f;
    float imgAlpha = 0.0f;

    /// <summary>
    /// テキストを明滅させる
    /// </summary>
    /// <param name="text">明滅させるテキスト</param>
    /// <param name="r">赤色要素</param>
    /// <param name="g">緑要素</param>
    /// <param name="b">青要素</param>
    /// <param name="spd">明滅速度</param>
    public void FadeINOUT(Text text,float r,float g,float b,float spd)
    {
        textAlpha += spd;
        // 明滅処理
        text.color = new Color(r, g, b, Mathf.Sin(textAlpha));
    }

    /// <summary>
    /// シーンチェンジ前のフェード
    /// </summary>
    /// <param name="img">フェードに使う</param>
    /// <param name="spd">速度</param>
    public void ImgFade(Image img,float spd)
    {
        imgAlpha += spd;
        img.color = new Color(0, 0, 0, imgAlpha);
    }
}
