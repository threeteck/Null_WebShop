using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop_NULL.Models.DomainModels
{
	public class City
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string Name { get; set; }
	}
}