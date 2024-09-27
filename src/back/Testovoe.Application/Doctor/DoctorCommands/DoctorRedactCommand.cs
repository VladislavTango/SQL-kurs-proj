
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Infrastructure.AppContext;
using Testovoe.Models;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DoctorRedactCommand : IRequestHandler<DoctorRedactRequest, Unit>
    {
        private readonly ApplicationContext _context;
        public DoctorRedactCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DoctorRedactRequest request, CancellationToken cancellationToken)
        {

            string commandStr = $@"if not exists(select * from Specializations where SpecializationName = '{request.Specialization}')
                               begin
                                   insert Specializations(SpecializationName) values ('{request.Specialization}')
                               end
                               if not exists(select * from Regions where RegionNumber = {request.DoctorsRegion})
                               begin
                                   insert Regions(RegionNumber) values ({request.DoctorsRegion})
                               end
                               if not exists(select * from DoctorsRooms where RoomNumber = {request.DoctorsRoom})
                               begin
                                   insert DoctorsRooms (RoomNumber, DoctorId) values ({request.DoctorsRoom}, {request.RedactId})
                               end
                               update Doctors set FIO = '{request.FIO}',
                               DoctorsRoomId = (select DoctorsRooms.Id from DoctorsRooms where DoctorsRooms.RoomNumber = {request.DoctorsRoom}),
                               SpecializationId = (select Specializations.Id from Specializations where SpecializationName = '{request.Specialization}'),
                               DoctorsRegionId = (select Regions.Id from Regions where RegionNumber = {request.DoctorsRegion}) 
                               where id = {request.RedactId};";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandStr, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

           
            return Unit.Value;

        }
    }
}
