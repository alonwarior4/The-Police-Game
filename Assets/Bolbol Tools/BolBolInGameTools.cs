using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BolBolUtility
{
    namespace BolBolInGameTools
    {       
        public class BolBolInGameTools
        {            
            /// <summary>
            /// Return a vector3 in curve position between two points with control point , use it in coroutine and give is pass time , then equal position to this to move in curve between points
            /// </summary>
            /// <param name="firstPoint"></param>
            /// <param name="secondPoint"></param>
            /// <param name="ControlPoint"></param>
            /// <param name="timePassed"></param>
            /// <returns></returns>
            public static Vector3 CurveMoveBetweenTwoPoint(Vector3 firstPoint, Vector3 secondPoint, Vector3 ControlPoint, float timePassed)
            {
                Vector3 m1 = Vector3.Lerp(firstPoint, ControlPoint, timePassed);
                Vector3 m2 = Vector3.Lerp(ControlPoint, secondPoint, timePassed);
                return Vector3.Lerp(m1, m2, timePassed);
            }

            public static Vector3 GetTouchWorldPosition(int touchIndex)
            {
                return UnityEngine.Camera.main.ScreenToWorldPoint(Input.GetTouch(touchIndex).position);
            }

            public static void PlayClipAtPointFixed(AudioClip clip , Vector3 playPos , float volume)
            {
                float currentTimeScale = Time.timeScale;
                Time.timeScale = 1;
                AudioSource.PlayClipAtPoint(clip, playPos, volume);
                Time.timeScale = currentTimeScale;
            }

            public static void playOneShotFixed(AudioSource audioSource , AudioClip clip , float volume)
            {
                float currentTimeScale = Time.timeScale;
                Time.timeScale = 1;
                audioSource.PlayOneShot(clip, volume);
                Time.timeScale = currentTimeScale;
            }

            public static void PlaySoundFixed(AudioSource audioSource , AudioClip clip , float volume , bool isLoop)
            {
                audioSource.clip = clip;
                float currentTimeScale = Time.timeScale;
                Time.timeScale = 1;
                audioSource.volume = volume;
                audioSource.Play();
                audioSource.loop = isLoop;
                Time.timeScale = currentTimeScale;
            }

            public static void SetTimer(float time , Action action)
            {
                if(time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    action();
                }
            }

            public static Vector3 RandomPointAround2DArc(float minAnlge , float maxAngle , float radious , Vector3 axis)
            {
                return RotateVector3AroundAxis(Vector3.right, UnityEngine.Random.Range(minAnlge, maxAngle), axis) * radious;
            }

            public static Vector3 RotateVector3AroundAxis(Vector3 vector , float angle , Vector3 axis)
            {
                return Quaternion.AngleAxis(angle, axis) * vector;
            }
        }
    }

}
