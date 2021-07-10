using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SpareParts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Appilation.UnitTests
{
    class FakeVehiclesMongoCollection : IMongoCollection<Vehicle>
    {
        public CollectionNamespace CollectionNamespace => throw new NotImplementedException();

        public IMongoDatabase Database => throw new NotImplementedException();

        public IBsonSerializer<Vehicle> DocumentSerializer => throw new NotImplementedException();

        public IMongoIndexManager<Vehicle> Indexes => throw new NotImplementedException();

        public MongoCollectionSettings Settings => throw new NotImplementedException();

        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session, PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session, PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void AggregateToCollection<TResult>(PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void AggregateToCollection<TResult>(IClientSessionHandle session, PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AggregateToCollectionAsync<TResult>(PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AggregateToCollectionAsync<TResult>(IClientSessionHandle session, PipelineDefinition<Vehicle, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public BulkWriteResult<Vehicle> BulkWrite(IEnumerable<WriteModel<Vehicle>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public BulkWriteResult<Vehicle> BulkWrite(IClientSessionHandle session, IEnumerable<WriteModel<Vehicle>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BulkWriteResult<Vehicle>> BulkWriteAsync(IEnumerable<WriteModel<Vehicle>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BulkWriteResult<Vehicle>> BulkWriteAsync(IClientSessionHandle session, IEnumerable<WriteModel<Vehicle>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long Count(FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long Count(IClientSessionHandle session, FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long CountDocuments(FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long CountDocuments(IClientSessionHandle session, FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountDocumentsAsync(FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountDocumentsAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(FilterDefinition<Vehicle> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(FilterDefinition<Vehicle> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(IClientSessionHandle session, FilterDefinition<Vehicle> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<Vehicle> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<Vehicle> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(FilterDefinition<Vehicle> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(FilterDefinition<Vehicle> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(IClientSessionHandle session, FilterDefinition<Vehicle> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<Vehicle> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<Vehicle> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<Vehicle, TField> field, FilterDefinition<Vehicle> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TField> Distinct<TField>(IClientSessionHandle session, FieldDefinition<Vehicle, TField> field, FilterDefinition<Vehicle> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<Vehicle, TField> field, FilterDefinition<Vehicle> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(IClientSessionHandle session, FieldDefinition<Vehicle, TField> field, FilterDefinition<Vehicle> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long EstimatedDocumentCount(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> EstimatedDocumentCountAsync(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<Vehicle> filter, FindOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, FindOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndDelete<TProjection>(FilterDefinition<Vehicle> filter, FindOneAndDeleteOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndDelete<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, FindOneAndDeleteOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<Vehicle> filter, FindOneAndDeleteOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, FindOneAndDeleteOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndReplace<TProjection>(FilterDefinition<Vehicle> filter, Vehicle replacement, FindOneAndReplaceOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndReplace<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, FindOneAndReplaceOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<Vehicle> filter, Vehicle replacement, FindOneAndReplaceOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, FindOneAndReplaceOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, FindOneAndUpdateOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndUpdate<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, FindOneAndUpdateOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, FindOneAndUpdateOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, FindOneAndUpdateOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<Vehicle> filter, FindOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(IClientSessionHandle session, FilterDefinition<Vehicle> filter, FindOptions<Vehicle, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<Vehicle> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IClientSessionHandle session, IEnumerable<Vehicle> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<Vehicle> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IClientSessionHandle session, IEnumerable<Vehicle> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(Vehicle document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(IClientSessionHandle session, Vehicle document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(Vehicle document, CancellationToken _cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(Vehicle document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(IClientSessionHandle session, Vehicle document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Vehicle, TResult> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Vehicle, TResult> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Vehicle, TResult> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Vehicle, TResult> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : Vehicle
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<Vehicle> filter, Vehicle replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<Vehicle> filter, Vehicle replacement, UpdateOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, UpdateOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<Vehicle> filter, Vehicle replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<Vehicle> filter, Vehicle replacement, UpdateOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, Vehicle replacement, UpdateOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<Vehicle> filter, UpdateDefinition<Vehicle> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IChangeStreamCursor<TResult> Watch<TResult>(PipelineDefinition<ChangeStreamDocument<Vehicle>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IChangeStreamCursor<TResult> Watch<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<Vehicle>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(PipelineDefinition<ChangeStreamDocument<Vehicle>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<Vehicle>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<Vehicle> WithReadConcern(ReadConcern readConcern)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<Vehicle> WithReadPreference(ReadPreference readPreference)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<Vehicle> WithWriteConcern(WriteConcern writeConcern)
        {
            throw new NotImplementedException();
        }
    }
}
