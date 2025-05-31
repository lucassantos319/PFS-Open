namespace PFS.Domain.Interfaces;

public interface IApplication<T> where T : class
{
    protected int Create(T obj);
    public void Update(T obj);
    public void DeleteById(int id);

}