using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private CameraCharacter cameraCharacter;
    private Material blockMaterial;

    private void Start()
    {
        cameraCharacter = FindObjectOfType<CameraCharacter>();
        blockMaterial = this.GetComponent<Material>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            HandleBlockCollision(collision.gameObject);
        }
    }

    private void HandleBlockCollision(GameObject block)
    {
        cameraCharacter.hitCount++;

        MeshRenderer blockRenderer = block.GetComponent<MeshRenderer>();
        if (blockRenderer != null)
        {
            blockRenderer.material = blockMaterial;
        }

        block.transform.DOMoveY(-6, 0.75f).SetEase(Ease.InBack).OnComplete(() =>
        {
            block.SetActive(false);
        });
    }
}
