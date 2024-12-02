using UnityEngine;

public class PuzzlePiece
{
    public GameObject PieceObject { get; private set; } // 駒のゲームオブジェクト
    public Vector2 Position => PieceObject.transform.position; // 駒の現在位置

    public PuzzlePiece(GameObject pieceObject)
    {
        PieceObject = pieceObject;
    }

    // 駒のスプライトを取得
    public Sprite GetSprite()
    {
        SpriteRenderer spriteRenderer = PieceObject.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.sprite : null;
    }

    // 駒の位置を変更
    public void SetPosition(Vector2 newPosition)
    {
        PieceObject.transform.position = newPosition;
    }

    // 駒を削除
    public void Destroy()
    {
        Object.Destroy(PieceObject);
    }
}
