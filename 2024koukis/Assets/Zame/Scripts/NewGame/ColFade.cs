using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColFade 
{
    // ����
    float textAlpha=1.0f;
    float imgAlpha = 0.0f;

    /// <summary>
    /// �e�L�X�g�𖾖ł�����
    /// </summary>
    /// <param name="text">���ł�����e�L�X�g</param>
    /// <param name="r">�ԐF�v�f</param>
    /// <param name="g">�Ηv�f</param>
    /// <param name="b">�v�f</param>
    /// <param name="spd">���ő��x</param>
    public void FadeINOUT(Text text,float r,float g,float b,float spd)
    {
        textAlpha += spd;
        // ���ŏ���
        text.color = new Color(r, g, b, Mathf.Sin(textAlpha));
    }

    /// <summary>
    /// �V�[���`�F���W�O�̃t�F�[�h
    /// </summary>
    /// <param name="img">�t�F�[�h�Ɏg��</param>
    /// <param name="spd">���x</param>
    public void ImgFade(Image img,float spd)
    {
        imgAlpha += spd;
        img.color = new Color(0, 0, 0, imgAlpha);
    }
}
