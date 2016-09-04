using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;

namespace Alamut.Data.MongoDb.BsonSerializer
{
    /// <summary>
    /// Serialize JObject into BsonDocument and vise versa 
    /// </summary>
    public class JObjectSerializer : IBsonSerializer
    {
        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return MongoDB.Bson.Serialization.BsonSerializer.Deserialize<object>(context.Reader);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var json = value == null
                ? "{}"
                : (value is JObject) 
                    ? (value as JObject).ToString() 
                    : Newtonsoft.Json.JsonConvert.SerializeObject(value);

            var document = BsonDocument.Parse(json);

            MongoDB.Bson.Serialization.BsonSerializer.Serialize(context.Writer, typeof (BsonDocument), document);
        }

        public Type ValueType => typeof (object);
    }
}
