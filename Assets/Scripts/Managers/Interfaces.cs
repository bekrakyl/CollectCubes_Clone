public class Interfaces
{
}


public interface ICollisionCube
{
    void OnInteractEnter(string layer);
    void OnInteractExit();  
}

public interface ICollisionCollector
{
    void OnInteractEnter(Cube cube);
}
