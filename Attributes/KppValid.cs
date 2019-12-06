using System.ComponentModel.DataAnnotations;
using AvantTest.Models;

namespace AvantTest.Attributes
{
    /// <summary>
    /// Атрибут осуществляющий валидацию модели Contractor на предмет наличия у Организации KPP
    /// </summary>
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