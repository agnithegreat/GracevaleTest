namespace Tools.Config
{
    public interface IConfigLoader
    {
        T LoadConfig<T>(string path);
    }
}