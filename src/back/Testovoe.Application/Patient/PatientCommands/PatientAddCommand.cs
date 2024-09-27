
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Application.Patient.PatientResponse;
using Testovoe.Infrastructure.AppContext;
using Testovoe.Models;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientAddCommand : IRequestHandler<PatientAddRequest, PatientAddResponse>
    {
        private readonly ApplicationContext _context;
        public PatientAddCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PatientAddResponse> Handle(PatientAddRequest request, CancellationToken cancellationToken)
        {
            string CommandString = @"
IF NOT EXISTS (SELECT 1 FROM Regions WHERE RegionNumber = @PatientRegion)
BEGIN
    INSERT INTO Regions (RegionNumber)
    VALUES (@PatientRegion)
END
DECLARE @regionId INT = (SELECT Id FROM Regions WHERE RegionNumber = @PatientRegion)
INSERT INTO Patients (Surname, Name, Patronymic, Address, BornTime, Sex, PatientRegionId)
VALUES (@Surname, @Name, @Patronymic, @Address, @BornTime, @Sex, @regionId)
SELECT TOP 1 Id FROM Patients ORDER BY Id DESC";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            PatientAddResponse id = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(CommandString, connection);
                command.Parameters.AddWithValue("@PatientRegion", request.PatientRegion);
                command.Parameters.AddWithValue("@Surname", request.Surname);
                command.Parameters.AddWithValue("@Name", request.Name);
                command.Parameters.AddWithValue("@Patronymic", request.Patronymic);
                command.Parameters.AddWithValue("@Address", request.Address);
                command.Parameters.AddWithValue("@BornTime", request.BornTime);
                command.Parameters.AddWithValue("@Sex", request.Sex);

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
