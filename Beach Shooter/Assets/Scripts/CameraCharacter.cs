using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class CameraCharacter : MonoBehaviour
{
    [SerializeField] private float spawnHelper = 4.5f;
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private float ballForce = 700f;
    [SerializeField] private float ballCount;
    [SerializeField] private float camMove;

    [Header("UI Elements")]
    [SerializeField] private Image cursor;
    [SerializeField] private TextMeshProUGUI ballCountText;
    [SerializeField] private TextMeshProUGUI mainText;

    [Header("Game State")]
    [SerializeField] private bool playState;

    [Header("Statistics")]
    public int hitCount;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        Cursor.visible = false;
        playState = true;
        ballCountText.text = ballCount.ToString();
    }

    private void Update()
    {
        if (playState)
        {
            cursor.transform.position = Input.mousePosition;
            UpdateCameraPosition(camMove);

            if (Input.GetMouseButtonDown(0) && ballCount > 0)
            {
                Vector3 targetLoc = GetRayDirection();
                ballCount--;

                LaunchBall(objPrefab, targetLoc, ballForce, spawnHelper);
                ballCountText.text = ballCount.ToString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            playState = false;
            HandleFinish();
        }
    }

    private void UpdateCameraPosition(float moveAmount)
    {
        transform.Translate(Vector3.forward * moveAmount * Time.deltaTime);
    }

    private Vector3 GetRayDirection()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        return ray.direction;
    }

    private void LaunchBall(GameObject prefab, Vector3 targetLoc, float force, float spawnOffset)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 ballInstantiatePoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + spawnOffset));

        GameObject ballRigid = Instantiate(prefab, ballInstantiatePoint, transform.rotation);
        ballRigid.GetComponent<Rigidbody>().AddForce(targetLoc * force*2);
    }

    private void HandleFinish()
    {
        if (hitCount >= 10)
        {
            mainText.text = "You Win!";
            mainText.transform.DOScale(1, 1).SetEase(Ease.OutBack);
        }
        else
        {
            mainText.text = "You Failed!";
            mainText.transform.DOScale(1, 1).SetEase(Ease.OutBack);
        }

        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(RestartLevelAfterDelay(4));
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
