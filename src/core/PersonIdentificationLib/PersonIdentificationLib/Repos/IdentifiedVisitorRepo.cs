﻿using CoreLib.Repos;
using Microsoft.Azure.Documents;
using PersonIdentificationLib.Abstractions;
using PersonIdentificationLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonIdentificationLib.Repos
{
    public class IdentifiedVisitorRepo : CosmosDbRepository<IdentifiedVisitor>, IIdentifiedVisitorRepository
    {
        private static string dbCollectionName;

        //public IdentifiedVisitorRepo(ICosmosDbClientFactory cosmosDbClientFactory) : base(cosmosDbClientFactory) { }

        public IdentifiedVisitorRepo(ICosmosDbClientFactory cosmosDbClientFactory, string collectionName) : base(cosmosDbClientFactory) 
        {
            dbCollectionName = collectionName;
        }

        public override string CollectionName { get; } = dbCollectionName;

        public override string GenerateId(IdentifiedVisitor entity) => $"{Guid.NewGuid()}";

        // Initially I opted to use month-year as the partition key. You can partition the data in different way.
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey($"{entityId.Substring(entityId.LastIndexOf('-') + 1)}");
    }
}
