//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public interface a
//{
//    int s { get; set; }
//    void MyMethod();

//}

//public class IA : a
//{
//    public int s { get; set; }

//    public void MyMethod()
//    {
//    }
//}

//[CreateAssetMenu(menuName = "Enemy Data")]
//public class EnemyData : ScriptableObject
//{
//    public ScriptableObject[] Components;

//    public bool Is<T>() where T : ScriptableObject
//    {
//        foreach (var c in Components)
//            if (c is T)
//                return true;

//        return false;
//    }
//}

//[CreateAssetMenu(menuName = "Name Component")]
//public class NameComponent : ScriptableObject
//{
//    public string value;
//}