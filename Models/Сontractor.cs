using System.ComponentModel.DataAnnotations;
using AvantTest.Attributes;
using Newtonsoft.Json;

namespace AvantTest.Models
{
    [KppValid]
    public class Contractor
    {
        /// <summary>
        /// id записи
        /// </summary>
        /// <value></value>
         [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Имя записи
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage="Поле Name обязательно для заполнения")]
        public string Name { get; set; }
        /// <summary>
        /// Полное имя не указывается, будет получен из dadata
        /// </summary>
        /// <value></value>
        public string FullName { get; private set; }
        [Required(ErrorMessage="Поле type обязательно для заполнения")]
        /// <summary>
        /// тип контрагента
        /// </summary>
        /// <value></value>
        public TypeContractor Type { get; set; }
        [Required(ErrorMessage="Поле INN обязательно для заполнения")]
        /// <summary>
        /// инн
        /// </summary>
        /// <value></value>
        public string INN { get; set; }
        /// <summary>
        /// КПП
        /// </summary>
        /// <value></value>
        public string KPP { get; set; }

        public void SetFullname(string _name)
        {
            this.FullName = _name;
        }
    }
}