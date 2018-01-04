using UnityEngine;
using UnityEngine.UI;

public class IconHolderBehaviour : MonoBehaviour {

    private Image icon;

    public void Initialize(Sprite icon)
    {
        this.GetComponent<Image>().sprite = icon;
    }

    // Update is called once per frame
    void Update () {
        this.transform.position = Input.mousePosition;
    }
}
