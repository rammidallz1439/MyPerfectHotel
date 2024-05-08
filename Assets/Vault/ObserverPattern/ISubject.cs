public interface ISubject
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
    void DeNotifyObservers();
    void NotifyTicks();
    void NotifyFixedTicks();
    void NotifyLateTicks();
}
