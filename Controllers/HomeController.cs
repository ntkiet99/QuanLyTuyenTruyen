using QuanLyThongTinTuyenTruyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThongTinTuyenTruyen.Controllers
{
    public class HomeController : Controller
    {
        public readonly DataContext _context;
        public HomeController()
        {
            _context = new DataContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadTable(string ten, int trangthai = -1)
        {
            var data = _context.ThongTin.AsQueryable();
            if (!string.IsNullOrEmpty(ten))
                data = data.Where(x => x.Ten.Contains(ten));
            if (trangthai != -1)
                data = data.Where(x => x.TrangThai == trangthai);
            return View(data.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdate(int id = 0)
        {
            var TrangThai = new List<TrangThai> { 
                new TrangThai(1,"Khởi tạo"),
                new TrangThai(2,"Đã duyệt"),
                new TrangThai(3,"Đã khóa")
            };
         
            if (id == 0)
            {
                ViewBag.TrangThai = new SelectList(TrangThai, "Id", "Name", 1);
                return View(new ThongTin());
            }
            else
            {
                var entity = _context.ThongTin.Where(x => x.ThongTinId == id).FirstOrDefault();
                if (entity == default(ThongTin))
                    throw new Exception("Không tìm thấy dữ liệu");
                ViewBag.TrangThai = new SelectList(TrangThai, "Id", "Name", entity.TrangThai);
                return View(entity);
            }
        }
        public ActionResult LichSu(int id)
        {
            var list = _context.LichSus.Where(x => x.ThongTinId == id).ToList();
            if (list == default(List<LichSu>))
                throw new Exception("Không tìm thấy dữ liệu");

            return View(list);
        }
        private void AddLichSu(int ThongTinId)
        {
            var lichSu = _context.LichSus.Where(x => x.ThongTinId == ThongTinId).ToList();
            if(lichSu.Count() > 0)
            {
                _context.LichSus.RemoveRange(lichSu);
                _context.SaveChanges();
            }
            var thongTin = _context.ThongTin.Where(x => x.ThongTinId == ThongTinId).FirstOrDefault();
            if(thongTin != default(ThongTin))
            {
                var time = thongTin.NgayHetHan.Subtract(thongTin.NgayBatDau);
                if(time.TotalDays > 0)
                {
                    var timeCount = time.TotalDays + 1;
                    var LichSus = new List<LichSu>();
                    for (int i = 0; i < timeCount; i++)
                    {
                        var ls = new LichSu();
                        ls.ThongTinId = ThongTinId;
                        ls.NgayPhat = thongTin.NgayBatDau.AddDays(i);
                        ls.SoLanPhat = 1;
                        if(ls.NgayPhat.CompareTo(DateTime.Now) <=  0)
                        {
                            ls.TrangThai = 2;
                        }
                        LichSus.Add(ls);
                    }
                    _context.LichSus.AddRange(LichSus);
                    _context.SaveChanges();
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrUpdate(ThongTin model)
        {
            try
            {
                if (model.ThongTinId == 0)
                {
                    _context.ThongTin.Add(model);
                    _context.SaveChanges();
                    AddLichSu(model.ThongTinId);
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Thêm thành công!",
                        MessageType = GenericMessage.success,
                        Data = { } 
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var entity = _context.ThongTin.Where(x => x.ThongTinId == model.ThongTinId).FirstOrDefault();
                    if (entity == default(ThongTin))
                        throw new Exception("Không tìm thấy dữ liệu.");
                    entity.Ten = model.Ten;
                    entity.MoTa = model.MoTa;
                    entity.NgayBatDau = model.NgayBatDau;
                    entity.NgayHetHan = model.NgayHetHan;
                    entity.TenDonViSuDung = model.TenDonViSuDung;
                    entity.TrangThai = model.TrangThai;

                    _context.SaveChanges();
                    //AddLichSu(entity.ThongTinId);
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Cập nhật thành công!",
                        MessageType = GenericMessage.success,
                        Data = { }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new GenericMessageVM()
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    MessageType = GenericMessage.error
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = _context.ThongTin.Where(x => x.ThongTinId == id).FirstOrDefault();
                _context.ThongTin.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPublish(int id = 0)
        {
            var entity = _context.ThongTin.Where(x => x.ThongTinId == id).FirstOrDefault();
            if (entity == default(ThongTin))
                throw new Exception("Không tìm thấy dữ liệu");
            return View(entity);
        }

        [HttpPost]
        public ActionResult Duyet(ThongTin model)
        {
            try
            {
                var data = _context.ThongTin.Where(x => x.ThongTinId == model.ThongTinId).FirstOrDefault();
                data.TrangThai = 2;
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}