﻿using System.Collections.Generic;
using System.Collections;

namespace MonoXEngine
{
    public static class Coroutines
    {
        public static List<IEnumerator> routines = new List<IEnumerator>();

        public static void Start(IEnumerator routine)
        {
            Coroutines.routines.Add(routine);
        }

        public static void StopAll()
        {
            Coroutines.routines.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < routines.Count; i++)
            {
                if (routines[i].Current is IEnumerator)
                    if (MoveNext((IEnumerator)routines[i].Current))
                        continue;
                if (!routines[i].MoveNext())
                    routines.RemoveAt(i--);
            }
        }

        private static bool MoveNext(IEnumerator routine)
        {
            if (routine.Current is IEnumerator)
                if (MoveNext((IEnumerator)routine.Current))
                    return true;
            return routine.MoveNext();
        }

        public static int Count
        {
            get { return routines.Count; }
        }

        public static bool Running
        {
            get { return routines.Count > 0; }
        }

    }
}
