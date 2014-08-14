using UnityEngine;
using System.Collections;

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


    void Start()
    {
        destinationScale = Vector3.one;
        controlComponent = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuController>();
        textView = gameObject.GetComponentInChildren<TextMesh>();
        sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        // ColorFading
        StartCoroutine(delayFade());
        statusColor = colorAlpha;
        setColor(statusColor);
    }

    void Update()
    {
        transform.localScale = Vector3.Slerp(transform.localScale, destinationScale,0.2f);

        if (isFadeActive)
        {
            statusColor = Color32.Lerp(textView.color, Color.white, 0.15f);
            setColor(statusColor);
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
