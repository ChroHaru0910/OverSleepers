using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    // ������������܂Ƃ߂�I�u�W�F�N�g
    [SerializeField] GameObject P2;
    // ��������R�}�̏��
    private Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    // ���W���X�g
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    // �����̔Ֆʂ̃R�}�I�u�W�F�N�g�ۑ����X�g
    List<GameObject> mypuzzleObj = new List<GameObject>();

    // �������̍��W���v�Z
    private Vector2 Pv2;
    // ���쒆�̃R�}
    GameObject ParentObj;

    // ���R�������鑬�x
    const float dropTime = 0.5f;
    float dropTimerCnt = 0.0f;

    // �ړ�����
    int Cy = 9;
    int Cx = 3;

    // ���n���������f
    bool isFlg = true;

    // GameManager�ŌĂяo��
    public void Game()
    {
        P2Input();
        Droppuzzle();
    }

    public void P2Input()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanMoveLeft(ParentObj))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.D) && CanMoveRight(ParentObj))
        {
            if (Cx == 9) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // ���Ɉړ��̏����i��: �h���b�v���x�𑁂߂�Ȃǁj
        }
    }

    /// <summary>
    /// �ՖʂɃR�}�𐶐�����
    /// </summary>
    public void GeneObj()
    {
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P2.transform;
        mypuzzleObj.Add(ParentObj);
        isFlg = false;
    }


    // ���R��������
    private void Droppuzzle()
    {
        if (isFlg) { return; }
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                isFlg = true;
                Debug.Log("�ݒu");
            }
            else
            {
                if (SetPuzzle(ParentObj))
                {
                    ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y);
                }
                else
                {
                    isFlg = true;
                    Debug.Log("����Ƀu���b�N����Đݒu����܂���");
                    return;
                }
                Cy--;
            }
        }
    }
    // ���ݑ��쒆�̃R�}�̉��ɕʂ̃R�}�����݂��邩
    private bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, targetY) &&
                Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x))
            {
                Debug.Log("��̈���ɕʂ̋���݂��܂��B");
                return false;
            }
        }
        return true;
    }

    // ���E�ɋ���݂��Ȃ������`�F�b�N����
    private bool CanMoveLeft(GameObject obj)
    {
        if (Cx <= 0) return false;

        float targetX = listRIGHT[Cy].posColumns[Cx - 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false; // ���ɋ���邽�߈ړ��s��
            }
        }
        return true; // ���ɋ�Ȃ����߈ړ���
    }
    private bool CanMoveRight(GameObject obj)
    {
        if (Cx >= listRIGHT[Cy].posColumns.Length - 1) return false;

        float targetX = listRIGHT[Cy].posColumns[Cx + 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false; // �E�ɋ���邽�߈ړ��s��
            }
        }
        return true; // �E�ɋ�Ȃ����߈ړ���
    }

    /// <summary>
    /// ��������p�Y���̋���󂯎��
    /// </summary>
    public GameObject SetObj
    {
        set { puzzleQueue.Enqueue(value); }
    }
    /// <summary>
    /// ���̃R�}�𐶐�����^�C�~���O��Ԃ�
    /// </summary>
    public bool IsFLAG { get { return isFlg; } }

    /// <summary>
    /// �Ֆʂ̍��W��Ⴄ
    /// </summary>
    /// <param name="list">���W�̓��������X�g</param>
    public void RecieveList(List<RIGHTWolrdVec2> list)
    {
        listRIGHT = list;
    }
}
