﻿using WebShop.Notifications;
using WebShop;

public class ProductNotifier
{
    private readonly List<INotificationObserver> _observers = new();

    public void Attach(INotificationObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(INotificationObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(Product product)
    {
        foreach (var observer in _observers)
        {
            observer.Update(product);
        }
    }
}
