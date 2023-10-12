using DoAnCnpm.Models;
using DoAnCnpm.Models.CommentViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        DoAnPMEntities database = new DoAnPMEntities();
        public ActionResult Index()
        {
            return View();
        }
        public bool Insert(Comment entity)
        {
            database.Comments.Add(entity);
            database.SaveChanges();
            return true;
        }

        public List<Comment> ListComment(long parentId, long productId)
        {
            return database.Comments.Where(x => x.ParentID == parentId && x.XeId == productId).ToList();
        }

        public List<CommentViewModel> ListCommentViewModel(long parentId, long productId)
        {
            var model = (from a in database.Comments
                         join b in database.Customers
                             on a.UserID equals b.IDCus
                         where a.ParentID == parentId && a.XeId == productId

                         select new
                         {
                             ID = a.ID,
                             CommentMsg = a.CommentMsg,
                             CommentDate = a.CommentDate,
                             ProductID = a.XeId,
                             UserID = a.UserID,
                             FullName = b.UserCus,
                             ParentID = a.ParentID,
                             Rate = a.Rate
                         }).AsEnumerable().Select(x => new CommentViewModel()
                         {
                             ID = x.ID,
                             CommentMsg = x.CommentMsg,
                             CommentDate = x.CommentDate,
                             ProductID = x.ProductID,
                             UserID = x.UserID,
                             ParentID = x.ParentID,
                             Rate = x.Rate
                         });
            return model.OrderByDescending(y => y.ID).ToList();
        }


    }
}