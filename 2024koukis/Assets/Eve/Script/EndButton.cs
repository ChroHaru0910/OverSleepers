using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    public Button button;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchObj);
    }

    // Update is called once per frame
    public void SwitchObj()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
