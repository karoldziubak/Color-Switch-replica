using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce= 5f;
    public Rigidbody2D rb;

    //background color change
    public GameObject BackgroundSprite;
    public GameObject infotext;
    public float ColorChangeRate = 0.01f;
    private SpriteRenderer backgroundSprite;
    private Color newcol;

    //Player color change
    public string currentColor;
    public Color cCyan;
    public Color cYellow;
    public Color cMagenta;
    public Color cPink;
    private SpriteRenderer centerSprite;

    void Start()
    {
        backgroundSprite = BackgroundSprite.GetComponent<SpriteRenderer>();
        centerSprite = transform.Find("centerCircle").GetComponent<SpriteRenderer>();
        SetRandomColor();

        infotext = new GameObject();
        TextMesh t = infotext.AddComponent<TextMesh>();
        t.text = "Click LEFT MOUSE BUTTON or SPACE to JUMP :)";
        t.fontSize = 50;
        t.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        t.transform.localPosition = new Vector3(-6f, -4f, 0f);
        
    }

    private Color getNewColor() //background color change
    {
        float cH, cS, cV;
        newcol = backgroundSprite.color; //get current color
        Color.RGBToHSV(newcol, out cH, out cS, out cV); //convert color to hsv color space
        cH += ColorChangeRate; //change colors hue
        return Color.HSVToRGB(cH, cS, cV); //convert back to RGB and return as Color
    } 

    void Update()
    {
        // if click detected
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity =  Vector2.up * jumpForce; //changes velocity to unit vector times jumpforce
            backgroundSprite.color = getNewColor(); //background color change
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == "colorChanger")
        {
            SetRandomColor();
            collision.gameObject.SetActive(false);
        } 
        else if (collision.tag != currentColor)
        {
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene(0);
        }
    }

    void SetRandomColor()
    {
        int index = Random.Range(0, 4);
        Debug.Log(index);
        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                centerSprite.color = cCyan;
                break;
            case 1:
                currentColor = "Yellow";
                centerSprite.color = cYellow;
                break;
            case 2:
                currentColor = "Magenta";
                centerSprite.color = cMagenta;
                break;
            case 3:
                currentColor = "Pink";
                centerSprite.color = cPink;
                break;
        }
    }
}
