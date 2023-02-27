using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    /// <summary>옵저버 등록 함수</summary>
    public void RegisterObserver(IObserver observer);
    /// <summary>옵저버 해지 함수</summary>
    public void RemoveObserver(IObserver observer);
    /// <summary>옵저버 정보 전달 함수</summary>
    void NotifyObservers();
}

public interface IObserver
{
    public void UpdateDate(GameObject data);
}



