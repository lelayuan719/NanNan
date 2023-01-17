using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] float threshDist = 8f;
    [SerializeField] Image progressImg;
    [SerializeField] TextMeshProUGUI text;

    bool active = false;
    float startDistToPlayer;
    int letterI;
    string letter;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Start moving
        rb.velocity = new Vector2(-EndlessRunnerController.scrollSpeed, 0f);
    }

    public void Init(int letterI, string letter)
    {
        this.letterI = letterI;
        this.letter = letter;
        text.text = letter;
    }

    void Update()
    {
        // Get distance
        float distToPlayer = transform.position.x - GameManager.GM.player.transform.position.x;

        // Only activates when close enough
        if (!active
            && Mathf.Abs(distToPlayer) < threshDist)
        {
            active = true;
            startDistToPlayer = distToPlayer;
        }

        if (active)
        {
            // Progress
            float progress = distToPlayer / startDistToPlayer;
            if (progress < 0)
            {
                EndlessRunnerController.Instance.MissQTE();
                Destroy(gameObject);
            }

            UpdateProgress(progress);

            // Check for pressing letter
            if (Input.inputString.ToUpper() == letter)
            {
                EndlessRunnerController.Instance.CollectLetter(letterI);
                Destroy(gameObject);
            }
        }
    }

    void UpdateProgress(float progress)
    {
        progressImg.fillAmount = progress;
    }
}
