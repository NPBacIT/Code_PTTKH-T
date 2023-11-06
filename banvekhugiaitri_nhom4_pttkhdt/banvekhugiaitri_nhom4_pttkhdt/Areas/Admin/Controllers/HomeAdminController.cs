using banvekhugiaitri_nhom4_pttkhdt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace banvekhugiaitri_nhom4_pttkhdt.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        PlayGroundContext ResDb = new PlayGroundContext();

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

		[Route("thongke")]
		[HttpPost]
		public IActionResult ThongKe(DateTime ngayTao)
		{
			// Lấy danh sách các hóa đơn được tạo từ ngày ngayTao trở đi
			List<HoaDon> hds = ResDb.HoaDons.Where(x => x.NgayTao >= ngayTao).ToList();

			// Lấy danh sách mã các món ăn từ các hóa đơn này
			List<int> ticketsIds = hds.Select(x => (int)x.MaVe).Distinct().ToList();

			// Lấy danh sách chi tiết các món ăn và tên các món ăn
			List<ThongKe> tks = new List<ThongKe>();
			var thongKes = (from hd in ResDb.HoaDons
							join ts in ResDb.VeKhuVuiChois on hd.MaVe equals ts.MaVe
							where ticketsIds.Contains((int)hd.MaVe)
							select new { hd.MaVe, hd.SoLuong, ts.TenVe, ts.AnhKvc }).ToList();

			// Khởi tạo danh sách ThongKe, mỗi phần tử tương ứng với một món ăn
			foreach (int ticketId in ticketsIds)
			{
				tks.Add(new ThongKe(ticketId));
			}

			// Tính tổng số lượng của mỗi món ăn
			foreach (ThongKe tk in tks)
			{
				int ticketId = tk.ticketId;
				int count = 0;
				foreach (var thongKe in thongKes)
				{
					if (thongKe.MaVe == ticketId)
					{
						count += (int)thongKe.SoLuong;
						tk.ticketName = thongKe.TenVe;
						tk.ticketImg = thongKe.AnhKvc;
					}
				}
				tk.soLuong = count;
			}

			// Sắp xếp danh sách theo số lượng giảm dần và trả về view
			List<ThongKe> tt = tks.OrderByDescending(x => x.soLuong).ToList();
			return View(tt);
		}
		[Route("listTicket")]
		public IActionResult LishTicket(string price)
		{
			var lstTicket = ResDb.VeKhuVuiChois.ToList();
			if (price == "all")
			{
				lstTicket = ResDb.VeKhuVuiChois.AsNoTracking().ToList();
			}
			if (price == "above")
			{
				lstTicket = ResDb.VeKhuVuiChois.AsNoTracking().Where(x => x.Gia > 200).ToList();
			}
			else if (price == "below")
			{
				lstTicket = ResDb.VeKhuVuiChois.AsNoTracking().Where(x => x.Gia <= 200).ToList();
			}
			return View(lstTicket);
		}

        [Route("listbookTicket")]
        public IActionResult KHTheoTicket(int khTicket)
        {
            var kh = (from c in ResDb.KhachHangs
                      join t in ResDb.HoaDons on c.MaKh equals t.MaKh
                      join q in ResDb.VeKhuVuiChois on t.MaVe equals q.MaVe
                      where q.MaVe == khTicket
                      select c);
            return View(kh);
        }

        [Route("addTicket")]
        [HttpGet]
        public IActionResult AddTicket()
        {
            ViewBag.MaDaiLy = new SelectList(ResDb.DaiLies.ToList(), "MaDaiLy", "TenDaiLy");
            ViewBag.MaTp = new SelectList(ResDb.ThanhPhos.ToList(), "MaTp", "TenTp");
            return View();
        }
        [ValidateAntiForgeryToken]
        [Route("addTicket")]
        [HttpPost]
        public IActionResult AddTicket(VeKhuVuiChoi ticket)
        {

            ticket.AnhKvc = ticket.formFile.FileName.Split(".")[0];
            ResDb.VeKhuVuiChois.Add(ticket);
            ResDb.SaveChanges();
            return RedirectToAction("listTicket");


            return View(ticket);
        }

        [Route("editTicket")]
        public IActionResult EditTicket(int eticket)
        {
            var ticketedit = ResDb.VeKhuVuiChois.SingleOrDefault(x => x.MaVe == eticket);
            ViewBag.MaDaiLy = new SelectList(ResDb.DaiLies.ToList(), "MaDaiLy", "TenDaiLy");
            ViewBag.MaTp = new SelectList(ResDb.ThanhPhos.ToList(), "MaTp", "TenTp");
            return View(ticketedit);
        }

        [Route("editTicket")]
        [HttpPost]
        public IActionResult EditTicket(VeKhuVuiChoi edTicket)
        {
            var ticket = ResDb.VeKhuVuiChois.Find(edTicket.MaVe);


            ticket.MaVe = edTicket.MaVe;
            ticket.TenVe = edTicket.TenVe;
            ticket.MoCua = edTicket.MoCua;
            ticket.DongCua = edTicket.DongCua;
            ticket.AnhKvc = edTicket.formFile.FileName.Split(".")[0];
            ticket.Gia = edTicket.Gia;
            ticket.Slvcl = edTicket.Slvcl;
            ticket.MaDaiLy = edTicket.MaDaiLy;
            ticket.MaTp = edTicket.MaTp;
            ticket.Active = ticket.Active;
            ticket.IsDeleted = ticket.IsDeleted;
            ResDb.SaveChanges();
            return RedirectToAction("listTicket");

            return View(edTicket);
        }


        [Route("deleteTicket")]
        public IActionResult DeleteTicket(int matickket,int madl,int matp)
        {
            // Lấy danh sách tour cần xóa dựa trên mã tour
            var listticket = ResDb.VeKhuVuiChois.Where(x => x.MaVe == matickket);
            //var cttour = Tourdb.Cttours.Where(x => x.MaCttour == mact).FirstOrDefault();
            var dl = ResDb.DaiLies.Where(x => x.MaDaiLy == madl).FirstOrDefault();
            var tp = ResDb.ThanhPhos.Where(x => x.MaTp == matp).FirstOrDefault();
            if (dl != null && tp !=null)
            {
                foreach (var item in listticket)
                {
                    if (item.MaDaiLy == madl && item.MaTp == matp)
                    {
                        return RedirectToAction("listTicket");
                    }
                }
            }

            // Update trường IsDeleted của các vé trong danh sách và lưu thay đổi
            if (listticket != null)
            {
                foreach (var item in listticket)
                {
                    item.IsDeleted = 1;
                }
                ResDb.SaveChanges();
            }

            // Update trường IsDeleted của vé dựa trên mã món ăn  và lưu thay đổi
            var ticket = ResDb.VeKhuVuiChois.FirstOrDefault(x => x.MaVe == matickket && x.MaDaiLy == madl && x.MaTp== matp);
            if (ticket != null)
            {
                ticket.IsDeleted = 1;
                ResDb.SaveChanges();
            }

            // Chuyển hướng đến trang danh sách món ăn
            return RedirectToAction("listTicket");
        }
    }
}
