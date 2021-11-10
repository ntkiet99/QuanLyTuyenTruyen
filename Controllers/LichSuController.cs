using QuanLyThongTinTuyenTruyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThongTinTuyenTruyen.Controllers
{
    public class LichSuController : Controller
    {
        public readonly DataContext _context;
        public LichSuController()
        {
            _context = new DataContext();
        }
        public ActionResult Index(int Id)
        {
            ViewBag.ThongTinId = Id;
            var entity = _context.ThongTin.Where(x => x.ThongTinId == Id).FirstOrDefault();
            ViewBag.Ten = entity.Ten;
            return View();
        }
        public ActionResult LoadTable(int Id)
        {
            var data = _context.LichSus.Where(x => x.ThongTinId == Id).AsQueryable();
            return View(data.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdate(int id = 0, int ThongTinId = 0)
        {
            var TrangThai = new List<TrangThai> {
                new TrangThai(0,"Chưa phát"),
                new TrangThai(2,"Đã phát")
            };

            if (id == 0)
            {
                ViewBag.TrangThai = new SelectList(TrangThai, "Id", "Name", 0);
                return View(new LichSu() { ThongTinId = ThongTinId});
            }
            else
            {
                var entity = _context.LichSus.Where(x => x.LichSuId == id).FirstOrDefault();
                if (entity == default(LichSu))
                    throw new Exception("Không tìm thấy dữ liệu");
                ViewBag.TrangThai = new SelectList(TrangThai, "Id", "Name", entity.TrangThai);
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult AddOrUpdate(LichSu model)
        {
            try
            {
                if (model.LichSuId == 0)
                {
                    _context.LichSus.Add(model);
                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Thêm thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var entity = _context.LichSus.Where(x => x.LichSuId == model.LichSuId).FirstOrDefault();
                    if (entity == default(LichSu))
                        throw new Exception("Không tìm thấy dữ liệu.");
                    entity.NgayPhat = model.NgayPhat;
                    entity.SoLanPhat = model.SoLanPhat;
                    entity.TrangThai = model.TrangThai;

                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Cập nhật thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
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
                var data = _context.LichSus.Where(x => x.LichSuId == id).FirstOrDefault();
                _context.LichSus.Remove(data);
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