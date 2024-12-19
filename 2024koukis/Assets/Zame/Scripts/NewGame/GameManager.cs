using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region field
    // �e�L�X�g�\���̃X�N���v�g
    [SerializeField] TextManager textMng;

    // �^�[�����Ǘ�
    [SerializeField] TurnManager turnMng;
    TurnManager.Turn turn;

    // �{�[�h�f�[�^�Ǘ�
    [SerializeField] BoardManager boardMng;
    List<WolrdVec2> vec2 = new List<WolrdVec2>();

    // �p�Y���̃R�}���Ǘ�
    [SerializeField] KomaManager komaMng;

    // ���ʂ𔽉f������
    [SerializeField] WinStar winStar;

    // �t�F�[�h�������̂��߂Ɏg���C���[�W
    [SerializeField] Image image;

    const float spd = 0.01f;        // �t�F�[�h���x
    const float timeSpan = 0.5f;    // �Ԋu
    float spdFade = 0;              // �t�F�[�h�l
    float timer = 0;                // ���Z�p

    // �t�F�[�h������N���X
    ColFade fade;

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

    void Update()
    {
        // �Q�[�����[�v�͂�����
        switch(gameState)
        {
            case GAME.READY:  // �Q�[���̏�Ԃ�ready �������f�[�^�����炦����X�^�[�g���Ă���
                StateReady();
                break;
            case GAME.START:  // �Q�[���X�^�[�g�̍��}�𑗂�
                StateStart();
                break;
            case GAME.PLAY:   // �Q�[���J�n
                StatePlay();
                break;
            case GAME.RESULT: // ���s
                StateResult();
                break;
        }
    }

    // �f�[�^�̃Z�b�g
    private bool SetUpStart()
    {
        fade = new ColFade();        // �C���X�^���X����

        turnMng.TurnSelect();        // ��U�����߂�

        winStar.SetWin();            // �������𔽉f

        // �^�[����Ⴄ
        turn = turnMng.GetTurn;

        // �{�[�h���W�擾
        boardMng.BoardSet();
        vec2 = boardMng.ReturnList();
        
        // �e�L�X�g�ɔ��f������
        textMng.SystemSetText(turn.ToString()+"����U�ł�");

        // ���W��n��
        komaMng.RecieveList(vec2);
        return true;
    }

    // gameState�̏������e
    #region State
    /// <summary>
    /// �X�^�[�g�O�̏���
    /// </summary>
    private void StateReady()
    {
        if (SetUpStart())
        {
            // �t�F�[�h�A�E�g������
            spdFade += spd;
            fade.ImgFade(image, -spdFade);
            if (image.color.a <= 0)
            {
                // ���ɐi��
                gameState = GAME.START;
            }
        }
    }

    /// <summary>
    /// �Q�[���X�^�[�g�̍��}�𑗂�
    /// </summary>
    private void StateStart()
    {
        timer += Time.deltaTime;
        if(timer>=timeSpan)
        {
            textMng.SystemSetText(true);  // ����������
            timer = 0;                    // �^�C�}�[���Z�b�g
            gameState = GAME.PLAY;        // ���ɐi��
        }
    }


    /// <summary>
    ///  �Q�[���J�n
    /// </summary>
    private void StatePlay()
    {
        if (komaMng.CanNextPuzzle&&!komaMng.IsChained)
        {
            // �^�[����Ⴄ
            turn = turnMng.GetTurn;
            // �^�[�����
            turnMng.TurnChange();

            komaMng.GeneObj(turn);

        }
        komaMng.Game();
    }

    /// <summary>
    /// ���s�\��
    /// </summary>
    private void StateResult()
    {
    
    }
    #endregion
}
