using UnityEngine;
using System.Collections;

public class SomeClass1
{ 
    public T GenericMethod<T>(T param)
    {
        return param;
    }
}

public class SomeOtherClass1 : MonoBehaviour
{
    void Start()
    {
        SomeClass1 myClass = new SomeClass1();

        myClass.GenericMethod<int>(5);
    }
}

public class GenericClass<T>
{
    T item;

    public void UpdateItem(T newItem)
    {
        item = newItem;
    }
}

public class GenericClassExample : MonoBehaviour
{
    void Start()
    {
        GenericClass<int> myClass = new GenericClass<int>();

        myClass.UpdateItem(5);
    }
}