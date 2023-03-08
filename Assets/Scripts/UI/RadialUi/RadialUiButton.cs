using UnityEngine;
using UnityEngine.UI;

public class RadialUiButton : MonoBehaviour
{
    enum ButtonType { Create, Upgrade, Move, Delete };

    [SerializeField] ButtonType buttonType;
    [SerializeField] Platform platform;

    void Start()
    {
        gameObject.GetComponent<Button>().image.alphaHitTestMinimumThreshold = 0.5f;
    }

    void clicked()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (buttonType == ButtonType.Move)
        {
            platform.setCursorPos(cursorPos);
            if(platform.towerExists())
            {

            }
        }
        else if(buttonType == ButtonType.Delete)
        {
            platform.setCursorPos(cursorPos);
            if(platform.towerExists())
            {
                GameObject tower = platform.delete();
                Destroy(tower);
            }
        }
    }
}
