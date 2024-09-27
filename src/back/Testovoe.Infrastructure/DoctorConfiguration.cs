using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testovoe.Models;

namespace Testovoe.Infrastructure
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<DoctorModel>
    {
        public void Configure(EntityTypeBuilder<DoctorModel> builder)
        {
            builder.HasOne(x => x.DoctorsRoom)
                   .WithMany(x => x.Doctor)
                   .HasForeignKey(x => x.DoctorsRoomId);
        }
    }
}
