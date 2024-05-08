

public interface IObserver 
{
    public  void  OnNotify();
    public void OnEnable();
    public void OnStart();
    public  void RegisterListeners();
    public  void RemoveListeners();
    public  void OnRelease();

   
}

