using UnityEngine;

public class ButtonTween : MonoBehaviour
{
    public enum TweenType
    {
        SlideIn,
        SlideOut,
        PopUp,
        PopOut
    }
    public TweenType Tween;
    [SerializeField] private Vector3 statringOffset;
    [SerializeField] private Vector3 final;
    [SerializeField] private float time;

    public void Perform()
    {
        switch (Tween)
        {
            default:
            case TweenType.SlideIn: 
                SlideIn(); 
                break;
            case TweenType.SlideOut: 
                SlideOut();
                break;
            case TweenType.PopUp: 
                PopUp(); 
                break;
            case TweenType.PopOut: 
                PopOut();
                break;
        }
    }
    private void PopUp()
    {
        LeanTween.scale(gameObject, Vector3.one, time);
        Tween = TweenType.PopOut;
    }
    private void PopOut()
    {
        LeanTween.scale(gameObject, Vector3.zero, time).setOnComplete(OnFadeOut);
        Tween = TweenType.PopUp;
    }
    private void SlideIn()
    {
        LeanTween.moveX(gameObject, final.x, time);
        Tween = TweenType.SlideOut;
    }
    private void SlideOut()
    {
        LeanTween.moveX(gameObject, statringOffset.x, time);
        Tween = TweenType.SlideIn;
    }
    private void OnFadeOut()
    {
        gameObject.SetActive(false);
    }
}
