using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    // ������������܂Ƃ߂�I�u�W�F�N�g
    [SerializeField] GameObject P1;
    // ��������R�}�̏��
    private Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    // ���W���X�g
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();
    // �����̔Ֆʂ̃R�}�I�u�W�F�N�g�ۑ����X�g
    List<GameObject> mypuzzleObj = new List<GameObject>();

    // �������̍��W���v�Z
    private Vector2 Pv2;
    // ���쒆�̃R�}
    GameObject ParentObj;

    // ���R�������鑬�x
    const float CONSTdropTime = 0.5f; // �ő厞��
    float dropTime = 0.5f;            // ���Ԑݒ�
    float dropTimerCnt = 0.0f;        // �J�E���g�p

    // �ړ�����
    int Cy = 9;
    int Cx = 3;

    // ���n���������f
    bool isFlg = true;
    // ��������
    bool gameLoseFlg = false;

    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;


    // GameManager�ŌĂяo��
    public void Game()
    {
        P1Input();
        Droppuzzle();
        ChainPuzzle();
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void LoseMethod()
    {
        for (int i = 0; i < mypuzzleObj.Count; i++)
        {
            // �^�񒆂̈�ԏ�ɒu���ꂽ�畉��
            if (mypuzzleObj[i].transform.position.y >= listLEFT[9].posColumns[3].y &&
                mypuzzleObj[i].transform.position.x == listLEFT[9].posColumns[3].x)
            {
                gameLoseFlg = true;
            }
        }
    }

    public bool LOSEFLAG
    {
        get { return gameLoseFlg; }
    }

    /// <summary>
    /// ���͏���
    /// </summary>
    private void P1Input()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMoveLeft(ParentObj))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && CanMoveRight(ParentObj))
        {
            if (Cx == 9) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dropTime = 0.1f;  // �h���b�v���x�𑬂�����
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            dropTime = CONSTdropTime;  // �ʏ�̗������x�ɖ߂�
        }
    }

    /// <summary>
    /// �ՖʂɃR�}�𐶐�����
    /// </summary>
    public void GeneObj()
    {
        // �A�����̏ꍇ�͐������Ȃ�
        if (puzzleCleared) { return; }
        timer = 0;
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P1.transform;
        mypuzzleObj.Add(ParentObj);
        isFlg = false;
    }

    // ��������
    private void Droppuzzle()
    {
        if (isFlg) { return; }
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                isFlg = true;
            }
            else
            {
                if (SetPuzzle(ParentObj))
                {
                    ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y);
                    Cy--;
                }
                else
                {
                    CheckAndClearPuzzle();
                    LoseMethod();
                    isFlg = true;
                }
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
                return false;
            }
        }
        return true;
    }

    // ���E�ɋ���݂��Ȃ������`�F�b�N����
    private bool CanMoveLeft(GameObject obj)
    {
        if (Cx <= 0) return false;

        float targetX = listLEFT[Cy].posColumns[Cx - 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMoveRight(GameObject obj)
    {
        if (Cx >= listLEFT[Cy].posColumns.Length - 1) return false;

        float targetX = listLEFT[Cy].posColumns[Cx + 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false;
            }
        }
        return true;
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
    public void RecieveList(List<LEFTWolrdVec2> list)
    {
        listLEFT = list;
    }

    /// <summary>
    /// �������ׂ����`�F�b�N���A��������
    /// </summary>
    private void CheckAndClearPuzzle()
    {
        // �������̃`�F�b�N
        for (int y = 0; y < listLEFT.Count; y++)
        {
            for (int x = 0; x < listLEFT[y].posColumns.Length; x++)
            {
                if (ClearPuzzleIfSurrounded(x, y, 1, 0)) // �������̃`�F�b�N (dx=1, dy=0)
                {
                    puzzleCleared = true;
                }
            }
        }

        // �c�����̃`�F�b�N
        for (int x = 0; x < listLEFT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listLEFT.Count; y++)
            {
                if (ClearPuzzleIfSurrounded(x, y, 0, 1)) // �c�����̃`�F�b�N (dx=0, dy=1)
                {
                    puzzleCleared = true;
                }
            }
        }
    }


    // �A������
    private void ChainPuzzle()
    {
        // ��������ꍇ�A�R�}�����ɋl�߂���ɏ�����ꏊ��T��
        if (puzzleCleared)
        {
            switch (num)
            {
                case 0:
                    ShiftPuzzlesDown();
                    timer += Time.deltaTime;
                    if (timer >= 0.25f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 1:
                    CheckAndClearPuzzle();
                    timer += Time.deltaTime;
                    if (timer >= 0.25f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 2:
                    ShiftPuzzlesDown();
                    timer = 0.25f; // ���̏����X�L�b�v
                    puzzleCleared = false;
                    num = 0;
                    break;
            }
        }
    }

    /// <summary>
    /// �w�肳�ꂽ�ʒu��2�̋���������ǂ����𔻒肵�A��������
    /// </summary>
    /// <param name="x">�`�F�b�N����X���W</param>
    /// <param name="y">�`�F�b�N����Y���W</param>
    /// <param name="dx">X�������̈ړ���</param>
    /// <param name="dy">Y�������̈ړ���</param>
    /// <returns>��������ꍇtrue</returns>
    private bool ClearPuzzleIfSurrounded(int x, int y, int dx, int dy)
    {
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        int oppositeX = x + dx * 2;
        int oppositeY = y + dy * 2;

        GameObject middlePuzzle = GetPuzzleAt(x + dx, y + dy);

        GameObject endPuzzle = GetPuzzleAt(oppositeX, oppositeY);

        if (endPuzzle == null) return false;

        Color startColor = GetPuzzleColor(startPuzzle);
        Color endColor = GetPuzzleColor(endPuzzle);

        if (startColor == endColor && startColor != Color.clear)
        {

            if (middlePuzzle != null && GetPuzzleColor(middlePuzzle) != startColor)
            {
                RemovePuzzle(startPuzzle);
                RemovePuzzle(middlePuzzle);
                RemovePuzzle(endPuzzle);
                return true; // ����������ꍇ��true��Ԃ�
            }
        }

        return false;
    }
    private void RemovePuzzle(GameObject puzzle)
    {
        if (mypuzzleObj.Contains(puzzle))
        {
            mypuzzleObj.Remove(puzzle);
            Destroy(puzzle);
        }
    }
    /// <summary>
    /// ��̐F���擾����
    /// </summary>
    private Color GetPuzzleColor(GameObject puzzle)
    {
        SpriteRenderer spriteRenderer = puzzle.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.color : Color.clear;
    }

    /// <summary>
    /// �R�}�����ɃV�t�g������
    /// </summary>
    private void ShiftPuzzlesDown()
    {
        for (int x = 0; x < listLEFT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listLEFT.Count; y++)
            {
                Vector2 currentPos = listLEFT[y].posColumns[x];
                GameObject currentPuzzle = GetPuzzleAtPosition(currentPos);

                if (currentPuzzle == null)
                {
                    for (int aboveY = y + 1; aboveY < listLEFT.Count; aboveY++)
                    {
                        Vector2 abovePos = listLEFT[aboveY].posColumns[x];
                        GameObject abovePuzzle = GetPuzzleAtPosition(abovePos);

                        if (abovePuzzle != null)
                        {
                            Vector2 targetPosition = currentPos;
                            abovePuzzle.transform.position = targetPosition;
                            UpdatePuzzlePosition(abovePuzzle, abovePos, targetPosition);
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// �w��ʒu�̃p�Y���I�u�W�F�N�g���擾
    /// </summary>
    private GameObject GetPuzzleAt(int x, int y)
    {
        if (y < 0 || y >= listLEFT.Count || x < 0 || x >= listLEFT[y].posColumns.Length)
            return null;

        Vector2 targetPos = listLEFT[y].posColumns[x];
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetPos.x) &&
                Mathf.Approximately(puzzle.transform.position.y, targetPos.y))
            {
                return puzzle;
            }
        }
        return null;
    }

    /// <summary>
    /// ����ʒu�̃p�Y�����擾
    /// </summary>
    private GameObject GetPuzzleAtPosition(Vector2 position)
    {
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, position.x) &&
                Mathf.Approximately(puzzle.transform.position.y, position.y))
            {
                return puzzle;
            }
        }
        return null;
    }

    /// <summary>
    /// �p�Y���̈ʒu���X�V����
    /// </summary>
    private void UpdatePuzzlePosition(GameObject puzzle, Vector2 oldPos, Vector2 newPos)
    {
        puzzle.transform.position = newPos;
    }
}