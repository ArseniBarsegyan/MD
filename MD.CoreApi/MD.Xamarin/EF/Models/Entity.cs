using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.Xamarin.EF.Models
{
    /// <summary>
    /// Base entity.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Primary key, int number.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}