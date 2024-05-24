using FireDN.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Thêm namespace này để sử dụng đối tượng IFormFile cho hình ảnh

namespace FireDN.Controllers
{
    public class HomeController : Controller
    {
        private readonly FireKLContext _context;

        public HomeController(FireKLContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name; // Lấy tên người dùng hiện đang đăng nhập

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.Username == userName); // Lấy thông tin của người dùng dựa trên tên người dùng

            var danhSachNguoiDung = _context.NguoiDungs.ToList(); // Lấy danh sách tất cả người dùng

            ViewBag.DanhSachNguoiDung = danhSachNguoiDung;

            return View(nguoiDung);
        }

        [HttpPost]
        public IActionResult AddUser(NguoiDung nguoiDung, IFormFile hinhAnh)
        {
            if (ModelState.IsValid)
            {
                // Xử lý hình ảnh
                if (hinhAnh != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        hinhAnh.CopyTo(memoryStream);
                        nguoiDung.HinhAnh = memoryStream.ToArray();
                    }
                }

                _context.NguoiDungs.Add(nguoiDung);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var nguoiDung = _context.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            var model = new NguoiDung
            {
                Iduser = nguoiDung.Iduser,
                Username = nguoiDung.Username,
                PassWord = nguoiDung.PassWord,
                RoleUser = nguoiDung.RoleUser,
                HinhAnh = nguoiDung.HinhAnh,
                Gmail = nguoiDung.Gmail
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NguoiDung model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var nguoiDung = _context.NguoiDungs.Find(model.Iduser);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin người dùng từ form chỉnh sửa
            nguoiDung.Username = model.Username;
            nguoiDung.RoleUser = model.RoleUser;
            nguoiDung.Gmail = model.Gmail;

            // Xử lý hình ảnh nếu có
            if (model.HinhAnh != null)
            {
                nguoiDung.HinhAnh = model.HinhAnh;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(int id, string newPassword)
        {
            var nguoiDung = _context.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            // Thực hiện kiểm tra và xử lý liên quan đến mật khẩu mới ở đây
            nguoiDung.PassWord = newPassword;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var nguoiDung = _context.NguoiDungs.Find(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
