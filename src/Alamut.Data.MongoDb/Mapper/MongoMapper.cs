using System;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.MongoDb.BsonSerializer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Alamut.Data.MongoDb.Mapper
{
    /// <summary>
    /// provide general mongod db mapper
    /// </summary>
    public static class MongoMapper
    {
        /// <summary>
        /// - map Id (string) to ObjectId in database
        /// - Ignore Extra Elementes set true
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static void MapId<TEntity>() where TEntity : IEntity
        {
            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                //cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.SetIgnoreExtraElements(true);
            });
        }

        public static void MapIdAndDynamicField<TEntity>(Expression<Func<TEntity, dynamic>> propertyLambda) where TEntity : IEntity
        {
            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapProperty(propertyLambda).SetSerializer(new JObjectSerializer());
            });
        }
    }
}
