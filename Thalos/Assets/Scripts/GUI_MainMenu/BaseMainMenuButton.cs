using UnityEngine;
using System.Collections;
using Controller;
public abstract class BaseMainMenuButton : MonoBehaviour {

    private Vector3 destinationScale;

    [SerializeField]
    private Vector3 selectedScale;
    
    [SerializeField]
    private float delayTime;

    private bool isFadeActive = false;
    private TextMesh textView;
    private SpriteRenderer[] sprites;
    private MainMenuController controlComponent;
    private Color32  colorAlpha =  new Color32(0,0,0,0);

    private Color statusColor;

    public abstract void DoActionWhenActivated();
    private Color destinationColor;
    private Color transparentColor;
    private Color activeColor = Color.white;

    void Start()
    {
        destinationScale = Vector3.one;
        controlComponent = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuController>();
        textView = gameObject.GetComponentInChildren<TextMesh>();
        sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();


        destinationColor = activeColor;
        // ColorFading
        StartCoroutine(delayFade());
        statusColor = colorAlpha;
        setColor(statusColor);


        // FedeOut
        Gamestatemanager.CloseMainMenuScreenHandler += FadeOut;
    }

    void Update()
    {
        transform.localScale = Vector3.Slerp(transform.localScale, destinationScale,0.2f);

        if (isFadeActive)
        {
            statusColor = Color32.Lerp(textView.color, destinationColor, 0.15f);
            setColor(statusColor);

            if(statusColor == destinationColor)
            {
                isFadeActive = false;
            }
        }
    }

    void OnMouseOver()
    {
        destinationScale = selectedScale;
    }

    void OnMouseExit()
    {
        destinationScale = Vector3.one;
    }

    public void SelectItem()
    {
        OnMouseOver();
    }

    public void DeselectItem()
    {
        OnMouseExit();
    }

    public void DoActionItem()
    {
        DoActionWhenActivated();
    }

    public void FadeOut()
    {
        destinationColor = new Color(0, 0, 0, 0);
    }


    private void setColor(Color color)
    {
        textView.color = color;

            foreach (SpriteRenderer render in sprites)
            {
                render.color = color;
            }
    }

    IEnumerator delayFade()
    {
        yield return new WaitForSeconds(delayTime);
        isFadeActive = true;
    }

}
