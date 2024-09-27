using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Application.Patient.PatientResponse;
using Testovoe.Enums;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class TakerPatientRowCommand : IRequestHandler<TakePatientRowRequest, TakePatientRowResponse>
    {
        private readonly ApplicationContext _context;
        public TakerPatientRowCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TakePatientRowResponse> Handle(TakePatientRowRequest request, CancellationToken cancellationToken)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            string CommandString = $"select * from Patients where Id = {request.id}";
            var responce = new TakePatientRowResponse();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(CommandString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        responce.Id = (int)reader["Id"];
                        responce.Surname = (string)reader["Surname"];
                        responce.Name = (string)reader["Name"];
                        responce.Patronymic = (string)reader["Patronymic"];
                        responce.Address = (string)reader["Address"];
                        responce.BornTime = (DateTime)reader["BornTime"];
                        responce.Sex = (Sex)reader["Sex"];
                        responce.PatientRegionID = (int)reader["PatientRegionId"];
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Patient not found. {request.id}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving doctor data", ex);
                }
            }
            return responce;
        }
    }
}
