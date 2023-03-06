using UnityEngine;
using UnityEngine.UI;

public class RadialUiButton : MonoBehaviour
{
    enum ButtonType { Create, Upgrade, Move, Delete };

    [SerializeField] ButtonType buttonType;

    void Start()
    {
        gameObject.GetComponent<Button>().image.alphaHitTestMinimumThreshold = 0.5f;
    }

    void Update()
    {
        
    }

    void clicked()
    {
        Debug.Log(buttonType);
        if(buttonType == ButtonType.Create)
        {

        }
    }
}
