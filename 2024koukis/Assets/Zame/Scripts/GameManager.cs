using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region field
    // �p�Y���̔Ֆʂ�ۑ�����
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // �t�F�[�h������UI
    [SerializeField] Image img;
    // �t�F�[�h���x
    [SerializeField] float spdImg=0.03f;
    // �t�F�[�h�N���X
    ColFade col;

    // �Q�[���J�n�̍��}���L��
    [SerializeField] Text text;

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

    // �Q�[����ԊǗ��̗񋓑�
    enum GAME
    {
        READY,
        START,
        PLAY,
        RESULT,
    }

    // �񋓑̂̕ϐ�
    private GAME game;
    #endregion

    void Start()
    {
        // �C���X�^���X����
        col = new ColFade();

        // �����x����
        col.ImgFade(img, 0.5f);

        // ���}�̏���
        text.text = "���f�B�[!!";

        // �X�C�b�`��������
        game = GAME.READY;

        // �Ֆʕۑ�
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();

        // �ۑ��������X�g��Ⴂ�󂯂�
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();

        // ��𐶐�
        generator.GENERATOR();

        // �v���C���[�ɍ��W���X�g���v���[���g
        p1Controller.RecieveList(listLEFT);
        p2Controller.RecieveList(listRIGHT);

    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����[�v�͂�����
        switch(game)
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
                    game = GAME.START;
                }

                break;
            case GAME.START: // �Q�[���X�^�[�g�̍��}�𑗂�
                {
                    if (img.color.a >= 0)
                    {
                        col.ImgFade(img, -spdImg);
                    }
                    else
                    {
                        text.text = "�X�^�[�g�I�I";
                        timer += Time.deltaTime;
                        if (timer >= maxTime)
                        {
                            // �^�C�}�[�̒l�����ʂ��
                            timer = 0;

                            // ���͂̎�t�J�n
                            game = GAME.PLAY;
                            Destroy(text);
                        }
                    }
                }
                break;
            case GAME.PLAY: // �Q�[���J�n
                // ��𐶐�
                if(p1Controller.LOSEFLAG)
                {
                    game = GAME.RESULT;
                    return;
                }
                generator.GENERATOR();
                if (p1Controller.IsFLAG)
                {
                    p1Controller.GeneObj();
                }
                if (p2Controller.IsFLAG)
                {
                    p2Controller.GeneObj();
                }
                p1Controller.Game();
                p2Controller.Game();
                break;
            case GAME.RESULT:
                Debug.Log("���ʔ��\");
                break;
        }
    }
}
