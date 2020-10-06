using System.Collections;
using UnityEngine;

// ex)  yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1));

namespace TWOPRO.Utils
{
    public static class CoroutineUtilities
    {
        public static IEnumerator WaitForRealTime(float delay)
        {
            while (true)
            {
                float pauseEndTime = Time.realtimeSinceStartup + delay;
                while (Time.realtimeSinceStartup < pauseEndTime)
                {
                    yield return 0;
                }
                break;
            }
        }
    }

}

