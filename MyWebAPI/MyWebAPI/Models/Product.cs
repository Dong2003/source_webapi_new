using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; }

        public string Description { get; set; }

        public double DonGia { get; set; }
    }
}
