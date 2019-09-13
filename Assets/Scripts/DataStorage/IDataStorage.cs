namespace DataStorage
{
    public interface IDataStorage<T>
    {
        T Data { get; }
        void Load(string path);
    }
}