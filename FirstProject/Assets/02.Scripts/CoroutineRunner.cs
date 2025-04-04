/*
 * 코루틴의 원리를 이해하기 위한 간단한 러너
 */
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 1. 열거자를 등록한다.
/// 2. 현재 열거자의 Current 객체 타입이 뭔지 확인한다.
/// 3. Current 객체 타입에 따라 객체가 요구하는 MoveNext() 호출 조건을 확인한다.
/// 4. 3번 조건이 충족되면 MoveNext() 호출하여 Yield 지침을 갱신한다.
/// 5. 다음 지침이 없어질 때 까지 이벤트 함수 타이밍에 맞게 2번부터 실행한다.
/// </summary>
public class CoroutineRunner : MonoBehaviour
{
    IEnumerator _routine;
    Yield _current;

    /// <summary>
    /// 반복기 등록 함수
    /// </summary>
    /// <param name="routine"> 열거자 </param>
    public void StartCoroutine(IEnumerator routine)
    {
        _routine = routine;
        _routine.MoveNext();
        _current = _routine.Current as Yield;
    }
    
    private void Update()
    {
        // TODO : do game logic

        if(_routine != null)
            OnAfterUpdate();
    }

    private void OnAfterUpdate()
    {
        bool isCoroutineFinished = false;

        //current 의 객체타입마다 다른 로직 수행
        if (_current == null)
        {
            if (_routine.MoveNext())
                _current = _routine.Current as Yield;
            else
                isCoroutineFinished = true;
        }
        else if (_current is WaitForSecondsYield waitForSeconds)
        {
            if (waitForSeconds.IsTimeOver)
            {
                if (_routine.MoveNext())
                    _current = _routine.Current as Yield;
                else
                    isCoroutineFinished = true;
            }
        }

        if (isCoroutineFinished)
        {
            _routine = null;
            _current = null;
        }
    }

    private void FixedUpdate()
    {
        if (_routine != null)
            OnAfterFixedUpdate();
    }

    private void OnAfterFixedUpdate()
    {
        if (_current is WaitForFixedUpdateYield waitForFixedUpdateYield)
        {
            _routine.MoveNext();
            _current = _routine.Current as Yield;
        }
    }
}

public abstract class Yield { }

public class WaitForSecondsYield : Yield
{
    /// <summary>
    /// 처음 진입 시간 기록
    /// </summary>
    public float TimeMark { get; set; }

    /// <summary>
    /// 경과 시간
    /// </summary>
    public float ElapsedTime => Time.time - TimeMark;

    /// <summary>
    /// 시간 경과 되었는지
    /// </summary>
    public bool IsTimeOver => ElapsedTime >= _seconds;

    private float _seconds; // 기다릴 시간

    public WaitForSecondsYield(float second)
    {
        TimeMark = Time.time;
        _seconds = second;
    }
}

public class WaitForFixedUpdateYield : Yield { }