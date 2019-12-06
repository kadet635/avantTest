using System.Collections;
using AvantTest.Models;
using System.Collections.Generic;

namespace AvantTest.Services
{
    public interface IDatasource
    {
        Contractor Create(Contractor _data);
        IEnumerable<Contractor> GetAll();
        bool CheckExist(string _inn, string _kpp);
    }
}