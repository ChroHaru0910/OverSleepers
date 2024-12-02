using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P1Controller : MonoBehaviour
{
    // �ϐ��܂Ƃ�
    #region Field
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
    float defaultDrop= 0.5f; // �f�t�H���g�̒l(�f�[�^���ǂݍ��܂Ȃ��ꍇ)
    float dropTime = 0.5f;      // ���Ԑݒ�
    float dropTimerCnt = 0.0f;  // �J�E���g�p

    // �ړ�����
    int Cy = 9;
    int Cx = 3;

    // ���̃R�}�̐����^�C�~���O
    bool canNext = true;
    // ��������
    bool gameLoseFlg = false;

    // �폜����
    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;

    // �l�N�X�g�\��
    NextPuzzleUI next;
    [SerializeField] Image[] images;
    #endregion

    // GameManager�ŌĂяo��
    public void Game()
    {
        P1Input();
        ChainPuzzle();
        Droppuzzle();
    }

    /// <summary>
    /// �������x�̐ݒ�
    /// </summary>
    public float SetUp
    {
        set { dropTime = value; }
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
            dropTime = defaultDrop;  // �ʏ�̗������x�ɖ߂�
        }
    }

    /// <summary>
    /// �ՖʂɃR�}�𐶐�����
    /// </summary>
    public void GeneObj()
    {
        // �A�����̏ꍇ�͐������Ȃ�
        if (puzzleCleared) { return; }
        NextOBJ();
        timer = 0;
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P1.transform;
        mypuzzleObj.Add(ParentObj);
        canNext = false;
    }

    // �l�N�X�g�\���֘A���\�b�h
    #region NEXTOBJ
    /// <summary>
    /// �C���X�^���X�������\�b�h
    /// </summary>
    private void Instance()
    {
        // �C���X�^���X����
        next = new NextPuzzleUI();
    }
    // �ŏ��̕\��
    private void SetNextObj()
    {
        // ���X�g�Ɋi�[
        List<GameObject> listObj = new List<GameObject>(puzzleQueue);
        images[0].sprite = listObj[0].GetComponent<SpriteRenderer>().sprite;
        images[1].sprite = listObj[1].GetComponent<SpriteRenderer>().sprite;
    }

    public void STARTSET()
    {
        Instance();
        SetNextObj();
    }

    private void NextOBJ()
    {
        next.NextObjUI(puzzleQueue, images);
    }
    #endregion

    // ��������֘A���\�b�h
    #region LOSE
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

    public bool IsLose
    {
        get { return gameLoseFlg; }
    }
    #endregion

    // �R�}�N���X�����

    // �R�}�̈ړ��Ɋւ��郁�\�b�h�i�Ֆʂ��܂ށj
    #region PUZZLEMOVE
    // ��������
    private void Droppuzzle()
    {
        if (canNext) { return; }
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                canNext = true;
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
                    canNext = true;
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
    public bool CanNextPuzzle { get { return canNext; } }

    /// <summary>
    /// �Ֆʂ̍��W��Ⴄ
    /// </summary>
    public void RecieveList(List<LEFTWolrdVec2> list)
    {
        listLEFT = list;
    }
    #endregion

    // �������R�}�̍폜�ƘA���`�F�b�N
    #region PUZZLEDELETE
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
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 1:
                    CheckAndClearPuzzle();
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 2:
                    ShiftPuzzlesDown();
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        puzzleCleared = false;
                        CheckAndClearPuzzle();
                        timer = 0;
                        num++;
                    }
                    break;
                case 3:
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num = 0;
                    }
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
    private bool ClearPuzzleIfSurrounded(float x, float y, float dx, float dy)
    {
        Debug.Log("�F�̒T�����J�n");

        // �n�_�̋���擾
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        // �n�_�̃X�v���C�g
        Sprite startSprite = GetPuzzleSprite(startPuzzle);
        if (startSprite == null) return false;

        // ���Ԃ̋�X�g
        List<GameObject> middlePuzzles = new List<GameObject>();
        List<Sprite> middleSprites = new List<Sprite>();

        // �I�_�̋�
        GameObject endPuzzle = null;

        // ���ԋ�̒T��
        int maxSearchRange = 10; // �ő�T���͈�
        for (int i = 1; i < maxSearchRange; i++)
        {
            // ���̋�̍��W���v�Z
            float checkX = x + dx * i;
            float checkY = y + dy * i;

            // ����擾
            GameObject currentPuzzle = GetPuzzleAt(checkX, checkY);
            if (currentPuzzle == null) break;

            // �X�v���C�g���擾
            Sprite currentSprite = GetPuzzleSprite(currentPuzzle);
            if (currentSprite == null) break;

            // �n�_�Ɠ����X�v���C�g�𔭌� �� �I�_�Ƃ���
            if (currentSprite == startSprite)
            {
                endPuzzle = currentPuzzle; // �I�_��ݒ�
                break;
            }

            // ���Ԃ̋�Ƃ��ă��X�g�ɒǉ�
            middlePuzzles.Add(currentPuzzle);
            middleSprites.Add(currentSprite);
        }

        // �I�_��������Ȃ��ꍇ�A�����ł��Ȃ�
        if (endPuzzle == null)
        {
            Debug.Log("�I�_��������Ȃ����ߏ����ł��܂���");
            return false;
        }

        // ���Ԃ̋�Ȃ��ꍇ�A�����ł��Ȃ�
        if (middlePuzzles.Count == 0)
        {
            Debug.Log("���Ԃ̋�Ȃ����ߏ����ł��܂���");
            return false;
        }

        // ���Ԃ̋���ׂē����F������
        Sprite firstMiddleSprite = middleSprites[0];
        foreach (Sprite sprite in middleSprites)
        {
            if (sprite != firstMiddleSprite)
            {
                Debug.Log("���Ԃ̋�����̐F�������Ă��܂�");
                return false; // �قȂ�F������ΏI��
            }
        }

        // ���Ԃ̋�̐F���n�_��I�_�̐F�ƈقȂ邩�m�F
        if (firstMiddleSprite == startSprite)
        {
            Debug.Log("���Ԃ̋�̐F���n�_�Ɠ������ߏ����ł��܂���");
            return false;
        }

        // ����폜����
        Debug.Log("����������܂�");
        foreach (GameObject puzzle in middlePuzzles)
        {
            RemovePuzzle(puzzle); // ���ԋ���폜
        }
        RemovePuzzle(startPuzzle); // �n�_���폜
        RemovePuzzle(endPuzzle); // �I�_���폜

        return true; // ����������ꍇ��true��Ԃ�
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
    private Sprite GetPuzzleSprite(GameObject puzzle)
    {
        SpriteRenderer spriteRenderer = puzzle.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.sprite : null;
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
    private GameObject GetPuzzleAt(float x, float y)
    {
        if (y < 0 || y >= listLEFT.Count || x < 0 || x >= listLEFT[(int)y].posColumns.Length)
            return null;

        Vector2 targetPos = listLEFT[(int)y].posColumns[(int)x];
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
    #endregion
}