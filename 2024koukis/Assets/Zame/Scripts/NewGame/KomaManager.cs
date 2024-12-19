using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaManager : MonoBehaviour
{
    // �ϐ��܂Ƃ�
    #region Field
    // ������������܂Ƃ߂�I�u�W�F�N�g
    [SerializeField] GameObject P;
    // ��������R�}�̏��
    [SerializeField] GameObject[] puzzleObj;
    // ���W���X�g
    List<WolrdVec2> listLEFT = new List<WolrdVec2>();
    // �Ֆʂ̃R�}�I�u�W�F�N�g�ۑ����X�g
    List<GameObject> mypuzzleObj = new List<GameObject>();
    
    // �������̍��W���v�Z
    private Vector2 Pv2;
    // ���쒆�̃R�}
    GameObject ParentObj;

    // �h���b�v������
    bool isDrop = false;

    // �ړ�����
    int Cy = 5;
    int Cx = 3;

    // ���̃R�}�̐����^�C�~���O
    bool canNext = true;

    // �폜����
    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;

    // �폜�\��̃I�u�W�F�N�g���܂Ƃ߂�
    List<GameObject> trash = new List<GameObject>();
    bool isCheck = false;
    #endregion

    // GameManager�ŌĂяo��
    public void Game()
    {
        PlInput();
        ChainPuzzle();
        Droppuzzle();
    }

    /// <summary>
    /// ���͏���
    /// </summary>
    private void PlInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Cx == 5) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isDrop = true;  // �h���b�v������
        }
    }

    /// <summary>
    /// �ՖʂɃR�}�𐶐�����
    /// </summary>
    public void GeneObj(TurnManager.Turn turn)
    {
        // �A�����̏ꍇ�͐������Ȃ�
        if (puzzleCleared) { return; }

        // ������Ԃɖ߂�
        isCheck = false;

        Cy = 5;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y);
        ParentObj = Instantiate(puzzleObj[(int)turn], initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P.transform;
        mypuzzleObj.Add(ParentObj);
        canNext = false;
    }

    // �R�}�̈ړ��Ɋւ��郁�\�b�h�i�Ֆʂ��܂ށj
    #region PUZZLEMOVE
    // ��������
    private void Droppuzzle()
    {
        if (canNext) { return; }

        if (isDrop)
        {
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                canNext = true;
                isDrop = false;
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
                    canNext = true;
                    isDrop = false;
                }
            }
        }
        return;
    }

    // ���ݑ��쒆�̃R�}�̉��ɕʂ̃R�}�����݂��邩
    private bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 1.5f;

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
    /// ���̃R�}�𐶐�����^�C�~���O��Ԃ�
    /// </summary>
    public bool CanNextPuzzle { get { return canNext; } }
    public bool IsChained { get { return puzzleCleared; } }
    /// <summary>
    /// �Ֆʂ̍��W��Ⴄ
    /// </summary>
    public void RecieveList(List<WolrdVec2> list)
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

        isCheck = true;
    }

    /// <summary>
    /// �w�肳�ꂽ�ʒu�̋���폜���ׂ������肵�A�폜���鏈��
    /// </summary>
    private bool ClearPuzzleIfSurrounded(float x, float y, float dx, float dy)
    {
        // �n�_�̋�擾
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        Sprite startSprite = GetPuzzleSprite(startPuzzle);
        if (startSprite == null) return false;

        // ���ԋ�X�g
        List<GameObject> middlePuzzles = new List<GameObject>();
        List<Sprite> middleSprites = new List<Sprite>();
        GameObject endPuzzle = null;

        // ���ԋ�̒T��
        int maxSearchRange = 10;
        for (int i = 1; i < maxSearchRange; i++)
        {
            float checkX = x + dx * i;
            float checkY = y + dy * i;

            GameObject currentPuzzle = GetPuzzleAt(checkX, checkY);
            if (currentPuzzle == null) break;

            Sprite currentSprite = GetPuzzleSprite(currentPuzzle);
            if (currentSprite == null) break;

            if (currentSprite == startSprite)
            {
                endPuzzle = currentPuzzle; // �I�_��ݒ�
                break;
            }

            middlePuzzles.Add(currentPuzzle);
            middleSprites.Add(currentSprite);
        }

        if (endPuzzle == null || middlePuzzles.Count == 0) return false;

        // ���ԋ�̐F�`�F�b�N
        Sprite firstMiddleSprite = middleSprites[0];
        foreach (Sprite sprite in middleSprites)
        {
            if (sprite != firstMiddleSprite) return false;
        }

        if (firstMiddleSprite == startSprite) return false;

        // ����폜
        foreach (GameObject puzzle in middlePuzzles) RemovePuzzle(puzzle);
        RemovePuzzle(startPuzzle);
        RemovePuzzle(endPuzzle);

        return true;
    }

    /// <summary>
    /// �w�肵������폜����
    /// </summary>
    private void RemovePuzzle(GameObject puzzle)
    {
        if (mypuzzleObj.Contains(puzzle))
        {
            //mypuzzleObj.Remove(puzzle);
            trash.Add(puzzle);
            //Destroy(puzzle);
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
    #endregion

    #region CHAINPUZZLE
    /// <summary>
    /// �A������
    /// </summary>
    private void ChainPuzzle()
    {
        if (puzzleCleared)
        {
            switch (num)
            {
                case 0:
                    if (isCheck)
                    {
                        foreach (GameObject obj in trash)
                        {
                            mypuzzleObj.Remove(obj);
                            Destroy(obj);
                        }
                        isCheck = false;
                    }
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

                case 1:
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
    /// ������ɃV�t�g������
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
