using System;
using System.Collections.Generic;

public interface ISubject
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer); 
    void Notify();                       
}

public interface IObserver
{
    void Update(ISubject subject);
}


public class NewsAgency : ISubject
{
    public string LatestNews { get; private set; }
    private readonly List<IObserver> _subscribers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        Console.WriteLine($"-> {observer} підписався на новини.");
        _subscribers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        _subscribers.Remove(observer);
        Console.WriteLine($"-> {observer} відписався від новин.");
    }

    public void Notify()
    {
        Console.WriteLine("Агентство сповіщає всіх підписників...");
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(this);
        }
    }

    public void PublishNews(string news)
    {
        LatestNews = news;
        Console.WriteLine($"\nАгентство публікує нову статтю: \"{LatestNews}\"");
        Notify();
    }
}

public class User : IObserver
{
    private readonly string _name;

    public User(string name)
    {
        _name = name;
    }

    public void Update(ISubject subject)
    {
        if (subject is NewsAgency agency)
        {
            Console.WriteLine($"   Користувач '{_name}' отримав сповіщення: \"{agency.LatestNews}\"");
        }
    }

    public override string ToString()
    {
        return $"Користувач '{_name}'";
    }
}



public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var newsAgency = new NewsAgency();
        var user1 = new User("Іван");
        var user2 = new User("Марія");

        newsAgency.Subscribe(user1);
        newsAgency.Subscribe(user2);

        newsAgency.PublishNews("C# 12 випущено з новими можливостями!");

        Console.WriteLine("\nМарія вирішила більше не читати новини.");
        newsAgency.Unsubscribe(user2);

        newsAgency.PublishNews("Патерни проектування: що нового у 2025?");
    }
}
