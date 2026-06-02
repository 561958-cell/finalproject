[System.Serializable]
public struct InputFrame
{
    public float horizontal;
    public bool jumpPressed;

    public InputFrame(float horizontal, bool jumpPressed)
    {
        this.horizontal = horizontal;
        this.jumpPressed = jumpPressed;
    }
}