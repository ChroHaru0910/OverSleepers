using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public abstract class PuzzlePlayerBase : MonoBehaviour
{
    #region Fields
    protected List<Vector2> boardPositions; // ���W���X�g
    protected bool isLose;                  // �s�k�t���O
    protected bool canNextPuzzle;           // ���̋�𐶐��ł��邩
    protected float dropSpeed;              // ��̗������x
    protected GameObject currentPiece;      // ���ݑ��쒆�̋�

    public bool IsLose => isLose;           // �s�k�t���O�̃v���p�e�B
    public bool CanNextPuzzle => canNextPuzzle; // ���̋���t���O�̃v���p�e�B
    public float SetUp { set => dropSpeed = value; } // �O�����痎�����x��ݒ�
    public GameObject SetObj { set => currentPiece = value; } // ����Z�b�g
    #endregion

    #region Abstract Methods
    public abstract void Game();            // �Q�[�������̍X�V
    public abstract void RecieveList(List<Vector2> positions); // ���W���X�g�̎󂯎��
    public abstract void GeneObj();         // ��̐���
    public abstract void STARTSET();        // �Q�[���J�n���̏���
    #endregion
}
