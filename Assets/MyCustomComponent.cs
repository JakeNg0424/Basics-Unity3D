using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCustomComponent : MonoBehaviour
{
    [Header("Speed!!!")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    float speed = 0.0f;

    public Button button;

    int score_number = 0;

    public Slider slider;
    public Text score;

    public float color_direction = -1.0f;

    Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        this.transform = this.GetComponent<Transform>();

        this.button.onClick.AddListener(this.GoFaster);
    }

    public void GoFaster()
    {
        Debug.Log("GoFaster Was Called!");
        this.speed += 10;
    }

    public void SetSpeed(float new_speed)
    {
        this.speed = new_speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }*/

        this.score_number += 1;
        this.score.text = "Frames: " + this.score_number.ToString();

        var new_position = this.transform.position;
        new_position.y += this.speed / 100 * Time.deltaTime;
        this.transform.position = new_position;

        var new_scale = this.transform.localScale;
        new_scale.x += this.speed / 100 * Time.deltaTime;
        this.transform.localScale = new_scale;

        var new_rotation = this.transform.localRotation;
        new_rotation = Quaternion.Euler(speed * Time.deltaTime * 10, 0.0f, 0.0f) * new_rotation;
        this.transform.localRotation = new_rotation;

        var mesh_renderer = this.GetComponent<MeshRenderer>();
        var material = mesh_renderer.materials[0];
        var color = material.color;

        color.r += 0.005f * this.color_direction;
        color.g += 0.005f * this.color_direction;
        color.b += 0.005f * this.color_direction;

        if (color.r <= 0.0f)
        {
            this.color_direction = 1.0f;
        }
        else if (color.r >= 1.0f)
        {
            this.color_direction = -1.0f;
        }

        material.color = color;
    }

    public void Save()
    {
        Debug.Log("Game saved!");
        PlayerPrefs.SetFloat("cube-speed", this.speed);
    }

    public void Load()
    {
        Debug.Log("Game loaded!");
        this.speed = PlayerPrefs.GetFloat("cube-speed", 0.0f);
    }
}