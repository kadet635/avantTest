using System.ComponentModel.DataAnnotations;
using AvantTest.Models;

namespace AvantTest.Attributes
{
    public class KppValidAttribute : ValidationAttribute
    {
        public KppValidAttribute()
        {
            ErrorMessage = "Для юр лица КПП должен быть заполнен";
        }
        public override bool IsValid(object value)
        {
            Contractor c = value as Contractor;

            if (c.Type==TypeContractor.Company && string.IsNullOrEmpty(c.KPP))
            {
                return false;
            }
            return true;
        }
    }
}