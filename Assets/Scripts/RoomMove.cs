using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraOffset;
    public Vector3 playerOffset;
    private CameraMovement cam;
    public TextMeshProUGUI roomName;
    public GameObject text;
    public bool needRoomTitle;
    public string roomNameText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraOffset;
            cam.maxPosition += cameraOffset;
            other.transform.position += playerOffset;

            if (needRoomTitle)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
    }

    private IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        roomName.text = roomNameText;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }
}
