using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCnpm.Models.CommentViewModal
{
    public class CommentViewModel
    {
        public long ID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? ProductID { get; set; }
        public int? UserID { get; set; }
        public int? ParentID { get; set; }
        public int? Rate { get; set; }
    }
}