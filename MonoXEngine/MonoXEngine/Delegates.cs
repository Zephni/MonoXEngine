namespace MonoXEngine
{
    /// <summary>
    /// MethodAction
    /// </summary>
    public delegate void MethodAction();

    /// <summary>
    /// BooleanAction
    /// </summary>
    public delegate bool BooleanAction();

    /// <summary>
    /// MatrixAction
    /// </summary>
    public delegate Microsoft.Xna.Framework.Matrix MatrixAction();

    /// <summary>
    /// Single parameter T Method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="param"></param>
    public delegate void SingleParamAction<T>(T param);

    /// <summary>
    /// Method that passes itself
    /// </summary>
    /// <param name="repeatCallback"></param>
    public delegate void RepeatCallback(RepeatCallback repeatCallback);
}