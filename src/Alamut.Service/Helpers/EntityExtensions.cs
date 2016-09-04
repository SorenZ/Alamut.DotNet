using System;
using System.Collections.Generic;
using Alamut.Data.Entity;

namespace Alamut.Service.Helpers
{
    public static class EntityExtensions
    {
        public static void SetCreateDate(this IDateEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
        }

        public static void SetUpdateDate(this IDateEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
        }

        /// <summary>
        /// set order of a list
        /// </summary>
        /// <param name="collection"></param>
        public static void SetOrder<T>(this ICollection<T> collection)
            where T : class, IOrderedEntity
        {
            var order = 1;

            foreach (var entity in collection)
                entity.Order = order++;
        }
    }
}
