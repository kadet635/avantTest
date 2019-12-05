using LiteDB;
using System.Collections;
using AvantTest.Models;
using System.Collections.Generic;

namespace AvantTest.Services
{
    public class Datasource : IDatasource
    {
        private static readonly LiteDatabase database = new LiteDatabase(@"data.db");

        public IEnumerable<Contractor> GetAll()
        {
            var collection = database.GetCollection<Contractor>("contractors");
            var data = collection.FindAll();
            
            return data;
        }

        public Contractor Create(Contractor _data)
        {
            var collection = database.GetCollection<Contractor>("contractors");
            collection.Insert(_data);
            return _data;
        }
    }
}