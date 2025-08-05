using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        // Lấy theo toàn bộ danh sách GET
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        // Tìm kiếm theo id
        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            try { 
                // LINQ [Object] Query
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Không tìm thấy hàng hóa với ID: " + id
                    });
                }
                return Ok(new
                {
                    Success = true, Data = hanghoa
                });
            } catch
            {
                return BadRequest( new
                {
                    Success = false, Message = "Kieu dinh dang khong dung" + id
                });
            }
        }

        // Sử dụng POST để tạo mới
        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hanghoa = new HangHoa
            {
                MaHH = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };
            hangHoas.Add(hanghoa);

            return Ok( new
            {
                Success = true, Data = hanghoa
            });
        }

        // Sử dụng PUT để cập nhật theo id
        [HttpPut("{id}")]
        public IActionResult Update(string id, HangHoaVM hanghoaEdit)
        {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));

                if (hanghoa == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Không tìm thấy hàng hóa với ID: " + id
                    });
                }

                if (id != hanghoa.MaHH.ToString())
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "ID không khớp với hàng hóa cần cập nhật"
                    });
                }

                // Cập nhật thông tin hàng hóa
                hanghoa.TenHangHoa = hanghoaEdit.TenHangHoa;
                hanghoa.DonGia = hanghoaEdit.DonGia;

                return Ok(new
                {
                    Success = true,
                    Data = hanghoa
                });

            }
            catch 
            { 
                return BadRequest(new
                {
                    Success = false,
                    Message = "Kieu dinh dang khong dung" + id
                });
            }
        }

        // Sử dụng DELETE để xóa theo id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) 
        {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));

                if (hanghoa == null)
                {
                    return NotFound();
                }

                hangHoas.Remove(hanghoa);

                return Ok();

            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
