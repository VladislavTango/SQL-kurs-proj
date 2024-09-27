using Testovoe.Infrastructure.AppContext;
using MediatR;
using Testovoe.Models;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class AddDoctorCommand : IRequestHandler<AddDoctorRequest, AddDoctorResponse>
    {
        private readonly ApplicationContext _context;
        public AddDoctorCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AddDoctorResponse> Handle(AddDoctorRequest request, CancellationToken cancellationToken)
        {
            string commandStr = $@"
IF NOT EXISTS (SELECT 1 FROM Specializations WHERE SpecializationName = '{request.Specialization}')
BEGIN
    INSERT INTO Specializations (SpecializationName)
    VALUES ('{request.Specialization}')
END
DECLARE @specializationId INT = (SELECT Id FROM Specializations WHERE SpecializationName = '{request.Specialization}')

IF NOT EXISTS (SELECT 1 FROM Regions WHERE RegionNumber = {request.Region})
BEGIN
    INSERT INTO Regions (RegionNumber)
    VALUES ({request.Region})
END
DECLARE @regionId INT = (SELECT Id FROM Regions WHERE RegionNumber = {request.Region})

IF NOT EXISTS (SELECT 1 FROM DoctorsRooms WHERE RoomNumber = {request.RoomNumber})
BEGIN
    INSERT INTO DoctorsRooms (RoomNumber)
    VALUES ({request.RoomNumber})
END
DECLARE @roomId INT = (SELECT Id FROM DoctorsRooms WHERE RoomNumber = {request.RoomNumber})

INSERT INTO Doctors (FIO, SpecializationId, DoctorsRegionId, DoctorsRoomId)
VALUES ('{request.FIO}', @specializationId, @regionId, @roomId)
select top 1 Id from Doctors order by Id desc ";
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            AddDoctorResponse id = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandStr, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        id.Id = (int)reader["Id"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }
            return id;
        }

    }
}
