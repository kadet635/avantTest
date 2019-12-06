using LiteDB;
using System.Collections;
using AvantTest.Models;
using System.Collections.Generic;

namespace AvantTest.Services
{
    /// <summary>
    /// Класс для доступа к данным
    /// </summary>
    public class Datasource : IDatasource
    {
        private static readonly LiteDatabase database = new LiteDatabase(@"data.db");
        
        /// <summary>
        /// Метод для получения всех записей контрагентов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contractor> GetAll()
        {
            LiteCollection<Contractor> collection = database.GetCollection<Contractor>("contractors");
            IEnumerable<Contractor> data = collection.FindAll();            
            return data;
        }

        /// <summary>
        /// Метод создания контрагента
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public Contractor Create(Contractor _data)
        {
            LiteCollection<Contractor> collection = database.GetCollection<Contractor>("contractors");
            collection.Insert(_data);
            return _data;
        }

        
        /// <summary>
        /// Метод проверяет существует ли такой контрагент в базе
        /// </summary>
        /// <param name="_inn"></param>
        /// <param name="_kpp"></param>
        /// <returns></returns>
        public bool CheckExist(string _inn, string _kpp)
        {
            LiteCollection<Contractor> collection = database.GetCollection<Contractor>("contractors");
            collection.EnsureIndex(a=>a.INN);
            collection.EnsureIndex(a=>a.KPP);
            bool result = collection.Exists(a=>a.INN==_inn && a.KPP==_kpp);
            return result;
        }
    }
}