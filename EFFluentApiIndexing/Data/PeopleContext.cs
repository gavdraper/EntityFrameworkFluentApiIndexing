using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using EFFluentApiIndexing.Model;

namespace EFFluentApiIndexing.Data
{
    public class PeopleContext : DbContext
    {
        public DbSet<People> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set field types
            modelBuilder.Entity<People>().Property(x => x.Firstname).HasMaxLength(25);
            modelBuilder.Entity<People>().Property(x => x.Lastname).HasMaxLength(25);
            modelBuilder.Entity<People>().Property(x => x.PhoneNumber).HasMaxLength(25);
            modelBuilder.Entity<People>().Property(x => x.NationalInsuranceNo).HasMaxLength(25);

            //Single Coulmn Index On Firstname
            modelBuilder.Entity<People>()
                .Property(x => x.Firstname)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_people_firstname")));

            //Multi Column Index On Firstname, Surname
            modelBuilder.Entity<People>()
                .Property(x => x.Firstname)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_people_fullname", 1)));
            modelBuilder.Entity<People>()
                .Property(x => x.Lastname)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_people_fullname", 2)));

            //Unique Index On National Insurance No            
            modelBuilder.Entity<People>()
                .Property(x => x.NationalInsuranceNo)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("ix_people_nationalinsurance") {IsUnique = true}));

            //Clustered Index On Id     
            /*Commented out as currently EF by default puts a clustered index on the primary key so this will fails
              as a clustered index already exists. A work around for this is to use migrations and in the initial migration
              remove the clustered index from the primary key.
             
              modelBuilder.Entity<People>()
                .Property(x => x.Id)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("ix_people_id_clustered") { IsClustered = true}));
             */

            base.OnModelCreating(modelBuilder);
        }
    }
}
