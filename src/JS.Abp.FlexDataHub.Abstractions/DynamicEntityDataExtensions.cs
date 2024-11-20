using System.Reflection;

namespace JS.Abp.FlexDataHub
{
    public static class DynamicEntityDataExtensions
    {
        public static T ToEntity<T>(this DynamicEntityData dynamicEntityData) where T : new()
        {
            var entity = new T();
            var entityType = typeof(T);

            foreach (var kvp in dynamicEntityData.Data)
            {
                var property = entityType.GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(entity, kvp.Value);
                }
            }

            return entity;
        }

        public static IEnumerable<T> ToEntities<T>(this IEnumerable<DynamicEntityData> dynamicEntityDataCollection) where T : new()
        {
            return dynamicEntityDataCollection.Select(dynamicEntityData => dynamicEntityData.ToEntity<T>());
        }
    }
}