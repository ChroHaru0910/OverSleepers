using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region field
    // �����̊g����
    // �p�Y���̔Ֆʂ�ۑ�����
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // �t�F�[�h������UI
    [SerializeField] Image img;
    // �t�F�[�h���x
    [SerializeField] float spdImg=0.03f;
    // �t�F�[�h�N���X
    ColFade colClass;

    // �Q�[���J�n�̍��}���L��
    [SerializeField] Text Readytext;

    // ���s���f�e�L�X�g
    [SerializeField] Text Pl1text;
    [SerializeField] Text Pl2text;
    // ���s�v���C���[
    // TRUE=PL1,FALSE=PL2
    // �������v���C���Ŕ��f
    bool isLose = false;

    // ���W��ۑ����郊�X�g
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();

    // ����
    float timer = 0;
    // ���鏈�����玟�̏����ɂ܂Ō������Ԋu
    [SerializeField] const float maxTime = 1.0f;

    // ��������
    [SerializeField] PuzzleGenerator generator;

    // �v���C���[
    [SerializeField] P1Controller p1Controller;
    [SerializeField] P2Controller p2Controller;

    // json�f�[�^
    [SerializeField] VariableManager variable;
    // �ϐ��̒l���f�[�^�����ƂɕύX
    [SerializeField] VariableValue value;

    // �Q�[����ԊǗ��̗񋓑�
    enum GAME
    {
        READY,
        START,
        PLAY,
        RESULT,
    }

    // �񋓑̂̕ϐ�
    private GAME gameState;
    #endregion

    void Start()
    {
        // �f�[�^�ǂݍ���
        variable.LoadJson();

        // �ϐ��̃Z�b�g
        value.SETUP();

        // �v���C���[�P�ɒl���Z�b�g
        p1Controller.SetUp=value.DROPSPD;

        // �C���X�^���X����
        colClass = new ColFade();

        // �����x����
        colClass.ImgFade(img, 0.5f);

        // ���}�̏���
        Readytext.text = "���f�B�[!!";

        // �X�C�b�`��������
        gameState = GAME.READY;

        // �Ֆʕۑ�
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();

        // �ۑ��������X�g��Ⴂ�󂯂�
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();

        // ����񐶐��i�l�N�X�g�\���̂��߁j
        generator.GENERATOR();
        generator.GENERATOR();

        // �v���C���[�ɍ��W���X�g���v���[���g
        p1Controller.RecieveList(listLEFT);
        p2Controller.RecieveList(listRIGHT);

        // �l�N�X�g�\���̏���
        p1Controller.STARTSET();
        p2Controller.STARTSET();
    }

    void Update()
    {
        // �Q�[�����[�v�͂�����
        switch(gameState)
        {
            case GAME.READY: // �Q�[���̏�Ԃ�ready �������f�[�^�����炦����X�^�[�g���Ă���
                 // start���\�b�h�Ńf�[�^���������Ⴆ�ĂȂ��ꍇ��
                if (listLEFT.Count == 0 || listRIGHT.Count == 0)
                {
                    Debug.Log("�ēx���X�g��Ⴂ�܂�");
                    // �Ֆʕۑ�
                    boardLEFT.LeftSet();
                    boardRIGHT.RightSet();
                    // �ۑ��������X�g��Ⴂ�󂯂�
                    listLEFT = boardLEFT.ReturnList();
                    listRIGHT = boardRIGHT.ReturnList();
                }

                timer += Time.deltaTime;
                if (timer >= maxTime)
                {
                    // �^�C�}�[�̒l�����ʂ��
                    timer = 0;
                    gameState = GAME.START;
                }
                break;
            case GAME.START: // �Q�[���X�^�[�g�̍��}�𑗂�
                {
                    if (img.color.a >= 0)
                    {
                        colClass.ImgFade(img, -spdImg);
                    }
                    else
                    {
                        Readytext.text = "�X�^�[�g!!";
                        timer += Time.deltaTime;
                        if (timer >= maxTime)
                        {
                            // �^�C�}�[�̒l�����ʂ��
                            timer = 0;

                            // ���͂̎�t�J�n
                            gameState = GAME.PLAY;
                            Destroy(Readytext);
                        }
                    }
                }
                break;
            case GAME.PLAY: // �Q�[���J�n
                // ���s
                if (p1Controller.IsLose)
                {
                    isLose = true;
                    gameState = GAME.RESULT;
                    
                    return;
                }
                // p2�̎��S�����̓f�o�b�O���̂��߃R�����g
                //else if (p2Controller.LOSEFLAG)
                //{
                //    winLose = false;
                //    game = GAME.RESULT;
                //}

                // ��𐶐�
                generator.GENERATOR();

                // ���ꂼ��̔ՖʂɃR�}�𐶐�
                if (p1Controller.CanNextPuzzle)
                {
                    // �l�N�X�g�\�����X�V���ĐV�K����
                    p1Controller.GeneObj();
                }
                if (p2Controller.CanNextPuzzle)
                {
                    // �l�N�X�g�\�����X�V���ĐV�K����
                    p2Controller.GeneObj();
                }

                // �v���C���[�X�V
                p1Controller.Game();
                p2Controller.Game();
                break;
            case GAME.RESULT:
                // �t�F�[�h
                if (img.color.a <= 0.5f)
                {
                    colClass.ImgFade(img, spdImg);
                }
                else
                {
                    // ���s�̃e�L�X�g
                    if(isLose)
                    {
                        Pl1text.text = "LOSE";
                        Pl2text.text = "WIN!!";
                    }
                    else
                    {
                        Pl1text.text = "WIN!!";
                        Pl2text.text = "LOSE";
                    }
                }
                Debug.Log("���ʔ��\");

                // ��������Đ�ł���悤�ɂ���
                break;
        }
    }
}
