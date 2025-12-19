using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public void swapCanvas(GameObject offCanvas, GameObject onCanvas)
    {
        offCanvas.SetActive(false);
        onCanvas.SetActive(true);
    }

    public void enableCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }

    public void disableCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }
}
