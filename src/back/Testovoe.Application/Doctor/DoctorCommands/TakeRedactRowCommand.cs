
using MediatR;
using Microsoft.Data.SqlClient;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;


namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class TakeRedactRowCommand : IRequestHandler<TakeReadactRowRequest, TakeRedactRowResponse>
    {
        public async Task<TakeRedactRowResponse> Handle(TakeReadactRowRequest request, CancellationToken cancellationToken)
        {
            TakeRedactRowResponse doctor = new();
            string getDoctorString = $"SELECT * FROM dbo.Doctors WHERE Id = {request.Id}";
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(getDoctorString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        doctor.DoctorID = (int)reader["Id"];
                        doctor.FIO = (string)reader["FIO"];
                        doctor.DoctorsRoomId = (int)reader["DoctorsRoomId"];
                        doctor.SpecializationID = (int)reader["SpecializationID"];
                        doctor.DoctorsRegionID = (int)reader["DoctorsRegionID"];
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Doctor not found. {request.Id}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving doctor data", ex);
                }
            }


            return doctor;
        }
    }
}
