namespace DotnetAPI.Data
{
    public class UserRepository
    {
        DataContextEF _entityFramework;
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd) // <T> = type definition
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToAdd) // <T> = type definition
        {
            if (entityToAdd != null)
            {
                _entityFramework.Remove(entityToAdd);
            }
        }
    }
};