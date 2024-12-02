using UnityEngine;

public class PuzzlePiece
{
    public GameObject PieceObject { get; private set; } // ��̃Q�[���I�u�W�F�N�g
    public Vector2 Position => PieceObject.transform.position; // ��̌��݈ʒu

    public PuzzlePiece(GameObject pieceObject)
    {
        PieceObject = pieceObject;
    }

    // ��̃X�v���C�g���擾
    public Sprite GetSprite()
    {
        SpriteRenderer spriteRenderer = PieceObject.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.sprite : null;
    }

    // ��̈ʒu��ύX
    public void SetPosition(Vector2 newPosition)
    {
        PieceObject.transform.position = newPosition;
    }

    // ����폜
    public void Destroy()
    {
        Object.Destroy(PieceObject);
    }
}
