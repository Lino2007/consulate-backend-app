using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;

namespace NSI.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Attachment> Attachment { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<DocumentType> DocumentType { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<Request> Request { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
    }
}
