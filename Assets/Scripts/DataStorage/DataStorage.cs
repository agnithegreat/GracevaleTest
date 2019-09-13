using Tools.Config;

namespace DataStorage
{
    public class DataStorage : IDataStorage<Data>
    {
        private Data _data;
        public Data Data => _data;

        private readonly IConfigLoader _loader;

        public DataStorage(IConfigLoader loader)
        {
            _loader = loader;
        }
        
        public void Load(string path)
        {
            _data = _loader.LoadConfig<Data>(path);
        }
    }
}