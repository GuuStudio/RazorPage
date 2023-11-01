

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Album.Model {
    public class Article {
        [Key]
        public int ID {set;get;}
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Bạn chưa thêm {0}")]
        public string Title {set;get;} = "";    
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bạn chưa chọn {0}")]
        [DisplayName("Ngày tạo")]
        public DateTime CreateTime {set;get;}
        [DisplayName("Nội dung")]
        public string Content {set;get;} ="";
    }
}