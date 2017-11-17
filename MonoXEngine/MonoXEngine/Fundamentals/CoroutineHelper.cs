using System;
using System.Collections;

namespace MonoXEngine
{
    /// <summary>
    /// Utilizes the static Coroutines class for ease of use in a gaming environment
    /// </summary>
    public static class CoroutineHelper
    {
        /// <summary>
        /// Action will run every update
        /// </summary>
        /// <param name="action"></param>
        public static void Always(Action action)
        {
            CoroutineHelper.RunUntil(() => false, action);
        }

        /// <summary>
        /// Until boolean returns false action will run every update, when boolean returns true callback will be called
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        public static void RunUntil(System.Func<bool> boolean, Action action, Action callback = null)
        {
            Coroutines.Start(CoroutineHelper.Run(boolean, action, callback));
        }

        /// <summary>
        /// Repeat run takes a method, but passes the same method back into itself, then during the method it can call itself again and pass 
        /// any method back into it.
        /// </summary>
        /// <param name="repeatCallback"></param>
        public static void RepeatRun(RepeatCallback repeatCallback)
        {
            repeatCallback(repeatCallback);
        }

        /// <summary>
        /// Waits until timer is reached since call, then calls the callback method
        /// </summary>
        /// <param name="Timer"></param>
        /// <param name="callback"></param>
        public static void WaitRun(float Timer, Action callback = null)
        {
            CoroutineHelper.RunUntil(Timer, null, callback);
        }

        /// <summary>
        /// Waits until boolean is true, and then calls the action method
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="action"></param>
        public static void RunWhen(System.Func<bool> boolean, Action action)
        {
            CoroutineHelper.RunUntil(boolean, () => { }, () => action());
        }

        /// <summary>
        /// After timer amount of time, call the action method and repeat.
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="action"></param>
        public static void Every(float timer, Action action)
        {
            CoroutineHelper.RunUntil(timer, null, () => {
                action();
                CoroutineHelper.Every(timer, action);
            });
        }

        /// <summary>
        /// Until timer is reached since call run action, after timer is reached call callback
        /// </summary>
        /// <param name="Timer"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        public static void RunUntil(float Timer, Action action, Action callback = null)
        {
            float CurrentTimer = 0;
            
            CoroutineHelper.RunUntil(() => CurrentTimer >= Timer, () => {
                CurrentTimer += Global.DeltaTime;

                if(action != null)
                    action();
            }, () => {
                if (callback != null)
                    callback();
            });
        }

        public static void RunOverX(float Timer, int Frames, SingleParamAction<int> action, Action callback = null, int CurrentX = 0)
        {
            float CurrentTimer = 0;
            int Frame = CurrentX;

            if(callback != null && Frame >= Frames)
            {
                callback();
                return;
            }

            action(Frame);

            CoroutineHelper.RunUntil(() => CurrentTimer >= Timer / Frames, () => {
                CurrentTimer += Global.DeltaTime;
            }, () => {
                CoroutineHelper.RunOverX(Timer, Frames, action, callback, Frame + 1);
            });
        }

        /// <summary>
        /// Will run action for timer amount of time, a percentage float from 0 to 1 will be passed into the action method. After time callback is called
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        public static void RunFor(float timer, SingleParamAction<float> action, Action callback = null)
        {
            float CurrentTimer = 0;

            CoroutineHelper.RunUntil(() => CurrentTimer >= timer, () => {
                CurrentTimer += Global.DeltaTime;
                action(CurrentTimer / timer);
            }, () => {
                if(callback != null)
                    callback();
            });
        }

        /// <summary>
        /// Will run while booleanWhile is true, for timer amount of time, passing the percentage progress to the action method, and callback when finished
        /// </summary>
        /// <param name="booleanWhile"></param>
        /// <param name="Timer"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        public static void RunWhileFor(System.Func<bool> booleanWhile, float Timer, SingleParamAction<float> action, Action callback = null)
        {
            float CurrentTimer = 0;

            CoroutineHelper.RunWhileUntil(booleanWhile, () => CurrentTimer >= Timer, () => {
                CurrentTimer += Global.DeltaTime;
                float percentage = CurrentTimer / Timer;
                action(percentage);
            }, callback);
        }

        /// <summary>
        /// While booleanWhile is false run action until booleanUntil, then callback
        /// </summary>
        /// <param name="booleanWhile"></param>
        /// <param name="booleanUntil"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        public static void RunWhileUntil(System.Func<bool> booleanWhile, System.Func<bool> booleanUntil, Action action, Action callback = null)
        {
            Coroutines.Start(CoroutineHelper.RunOn(booleanWhile, booleanUntil, action, callback));
        }

        /// <summary>
        /// (Private) While booleanWhile is false run action until booleanUntil, then callback
        /// </summary>
        /// <param name="booleanWhile"></param>
        /// <param name="booleanUntil"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private static IEnumerator RunOn(System.Func<bool> booleanWhile, System.Func<bool> booleanUntil, Action action, Action callback = null)
        {
            while(booleanUntil() != true)
            {
                yield return null;

                if(booleanWhile() != true)
                    action();
            }

            if(callback != null)
                callback();
        }

        /// <summary>
        /// (Private) Until boolean is false, run action, callback when boolean is true
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private static IEnumerator Run(System.Func<bool> boolean, Action action, Action callback = null)
        {
            while (boolean() != true)
            {
                yield return null;
                action();
            }

            if(callback != null)
                callback();
        }
    }
}
