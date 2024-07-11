using YarYab.Common;

namespace YarYab.Service.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
