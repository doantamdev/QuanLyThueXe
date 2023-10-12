using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class GetIDCusController : Controller
    {
        // GET: GetIDCus
        public JsonResult GetUserIDFromSession()
        {
            // Lấy giá trị ID từ Session
            var userID = Session["IDCus"] as int?;

            if (userID.HasValue)
            {
                return Json(userID, JsonRequestBehavior.AllowGet);
            }

            // Trả về một giá trị mặc định hoặc thông báo lỗi nếu không thể lấy giá trị từ Session
            return Json("Không thể lấy ID từ Session", JsonRequestBehavior.AllowGet);
        }

    }
}