using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region field
    // �p�Y���̔Ֆʂ�ۑ�����
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // ���W��ۑ����郊�X�g
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();

    enum GAME
    {
        READY,
        START,
        RESULT,
    }

    private GAME game;
    #endregion

    void Start()
    {
        game = GAME.READY;
        // �Ֆʕۑ�
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();
        // �ۑ��������X�g��Ⴂ�󂯂�
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����[�v�͂�����
        switch(game)
        {
            case GAME.READY: // �Q�[���̏�Ԃ�ready �������f�[�^�����炦����X�^�[�g���Ă���
            // start���\�b�h�Ńf�[�^���������Ⴆ�ĂȂ��ꍇ��
            if (listLEFT.Count ==0 || listRIGHT.Count==0)
                {
                    Debug.Log("�ēx���X�g��Ⴂ�܂�");
                    // �Ֆʕۑ�
                    boardLEFT.LeftSet();
                    boardRIGHT.RightSet();
                    // �ۑ��������X�g��Ⴂ�󂯂�
                    listLEFT = boardLEFT.ReturnList();
                    listRIGHT = boardRIGHT.ReturnList();
                }
            else
                {
                    Debug.Log(listLEFT[0].posColumns[0]);
                    game = GAME.START;
                }
                break;
            case GAME.START: // ��������Q�[���J�n���͓��̎󂯕t���͂�������
                {

                }
                break;
        }
    }
}
