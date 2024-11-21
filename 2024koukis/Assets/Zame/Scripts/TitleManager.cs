using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    // �t�F�[�h��������e�L�X�g
    [SerializeField] Text text;
    // �������e
    ColFade colFade;
    // ���ő��x
    [SerializeField] float spdFade;
    // �F�w��
    [SerializeField] float r, g, b;

    // �V�[�g�؂�ւ��̃t�F�[�h
    [SerializeField] Image img;
    // ���ő��x
    [SerializeField] float spdImg;
    bool flg = false; 

    // ���̃V�[����
    [SerializeField] string sceneName;
    void Start()
    {
        // �C���X�^���X����
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
