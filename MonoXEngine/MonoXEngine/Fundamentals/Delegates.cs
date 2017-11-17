namespace MonoXEngine
{
    /// <summary>
    /// BooleanAction
    /// </summary>
    public delegate bool BooleanAction();

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