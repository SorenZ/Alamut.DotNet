using System;
using System.Collections.Generic;
using System.Linq;
using Alamut.Data.Structure;
using MongoDB.Bson;

namespace Alamut.Data.MongoDb.Helpers
{
    public static class StructureExtensions
    {
        /// <summary>
        /// assing a new id to an entity object
        /// </summary>
        /// <param name="struct"></param>
        public static void AssignId(this IIdBased @struct)
        {
            @struct.Id = ObjectId.GenerateNewId().ToString();
        }

        /// <summary>
        /// assing a new id if not provided
        /// </summary>
        /// <param name="struct"></param>
        public static void AssignIdIfNot(this IIdBased @struct)
        {
            if (string.IsNullOrEmpty(@struct.Id))
                @struct.Id = ObjectId.GenerateNewId().ToString();
        }

        /// <summary>
        /// assing Id for a collection if not have 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void AssingIdIfNot<T>(this ICollection<T> collection)
            where T : IIdBased
        {
            foreach (var entity in collection.Where(q => String.IsNullOrEmpty(q.Id)))
                entity.AssignId();
        }


    }
}