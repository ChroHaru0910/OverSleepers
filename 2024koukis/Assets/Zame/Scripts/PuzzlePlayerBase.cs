using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlayerBase : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g
    [SerializeField] protected GameObject player;

    // ��̃L���[�ƃ��X�g
    protected Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    protected List<GameObject> mypuzzleObj = new List<GameObject>();

    // ��̐e�I�u�W�F�N�g
    protected GameObject ParentObj;

    // �������Ԃƃ^�C�}�[
    protected const float dropTime = 0.5f;
    protected float dropTimerCnt = 0.0f;

    // �Ֆʂ�Y���W��X���W
    protected int Cy = 9;
    protected int Cx = 3;

    // �t���O�ƈʒu���X�g
    protected bool isFlg = true;
    protected List<Vector2> posColumns = new List<Vector2>();

    // �V��������L���[�ɒǉ�����v���p�e�B
    public GameObject SetObj
    {
        set { puzzleQueue.Enqueue(value); }
    }

    // �t���O��true���ǂ�����Ԃ��v���p�e�B
    public bool IsFLAG { get { return isFlg; } }

    // �Q�[���̃��[�v���s��
    public virtual void Game()
    {
        HandleInput();
        Droppuzzle();
    }

    // �e�R���g���[���œ��͏���������
    protected virtual void HandleInput() { }

    // ���R��������
    protected void Droppuzzle()
    {
        if (isFlg) return;
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
                    ParentObj.transform.position = new Vector2(posColumns[Cx].x, posColumns[Cx].y);
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

    // ��̉��ɕʂ̋���݂��邩���`�F�b�N
    protected bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, targetY) &&
                Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x))
            {
                return false;
            }
        }
        return true;
    }

    // ����Ɉړ��ł��邩�ǂ���
    protected bool CanMoveLeft(GameObject obj)
    {
        float targetX = obj.transform.position.x - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetX) &&
                Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y))
            {
                return false;
            }
        }
        return true;
    }

    // ��E�Ɉړ��ł��邩�ǂ���
    protected bool CanMoveRight(GameObject obj)
    {
        float targetX = obj.transform.position.x + 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetX) &&
                Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y))
            {
                return false;
            }
        }
        return true;
    }

    // ����Ɉړ��ł��邩�ǂ���
    protected bool CanMoveDown(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x) &&
                Mathf.Approximately(puzzle.transform.position.y, targetY))
            {
                return false;
            }
        }
        return true;
    }

    // ��𐶐����郁�\�b�h�iGene���\�b�h�j
    protected void Gene(GameObject puzzlePrefab)
    {
        if (puzzleQueue.Count > 0)
        {
            ParentObj = Instantiate(puzzleQueue.Dequeue());
            ParentObj.transform.position = new Vector2(posColumns[Cx].x, posColumns[Cx].y);
            mypuzzleObj.Add(ParentObj);
            isFlg = false; // ��𐶐�������A�������J�n����
        }
    }
}
