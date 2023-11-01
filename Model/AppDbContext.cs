

using Album.Model;
using Album.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;

namespace Album.Data {
    public class AppDbContext : IdentityDbContext<AppUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Phương thức khởi tạo này chứa options để kết nối đến MS SQL Server
            // Thực hiện điều này khi Inject trong dịch vụ hệ thống
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

            foreach ( IMutableEntityType entityType in builder.Model.GetEntityTypes() ) {
                if (entityType != null  ) {
                    string tableName = entityType.GetTableName() ?? "";
                    if(!tableName.IsNullOrEmpty()) {
                        if(tableName.StartsWith("AspNet")) {
                            entityType.SetTableName(tableName.Substring(6));
                        }
                    }
                }
            }
        }
        public DbSet<Article> Article {set; get;}
    }
}