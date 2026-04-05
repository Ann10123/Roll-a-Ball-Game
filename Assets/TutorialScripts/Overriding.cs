using UnityEngine;
using System.Collections;

public class Fruit1
{
    public Fruit1()
    {
        Debug.Log("1sr Fruit Constructor Called");
    }

    public virtual void Chop()
    {
        Debug.Log("The fruit has been chopped.");
    }

    public virtual void SayHello()
    {
        Debug.Log("Hello, I am a fruit.");
    }
}

public class Apple1 : Fruit1
{
    public Apple1()
    {
        Debug.Log("1sr Apple Constructor Called");

    }

    public override void Chop()
    {
        base.Chop();
        Debug.Log("The apple has been chopped.");
    }

    public override void SayHello()
    {
        base.SayHello();
        Debug.Log("Hello, I am an apple.");
    }
}

public class FruitSalad1 : MonoBehaviour
{
    void Start()
    {
        Apple1 myApple = new Apple1();

        myApple.SayHello();
        myApple.Chop();

        Fruit1 myFruit = new Apple1();
        myFruit.SayHello();
        myFruit.Chop();
    }
}
