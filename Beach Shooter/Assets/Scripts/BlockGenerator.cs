using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    private const int initialPositionZ = 20;
    private const int firstColumnPositionX = -7;
    private const int secondColumnPositionX = 7;
    private const int blockSpacing = 10;
    private const int numberOfBlocks = 6;

    private void Awake()
    {
        SpawnBlocksInColumn(firstColumnPositionX);
        SpawnBlocksInColumn(secondColumnPositionX);
    }

    private void SpawnBlocksInColumn(int positionX)
    {
        int currentPositionZ = initialPositionZ;

        for (int i = 0; i < numberOfBlocks; i++)
        {
            Vector3 spawnPosition = new Vector3(positionX, 1f, currentPositionZ);
            Instantiate(blockPrefab, spawnPosition, blockPrefab.transform.rotation);
            currentPositionZ += blockSpacing;
        }
    }
}
