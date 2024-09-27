using MediatR;
using Microsoft.Data.SqlClient;
using Testovoe.Application.Doctor.DoctorRequest;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DeleteDoctorCommand : IRequestHandler<DeleteDoctorRequest, int>
    {
        public async Task<int> Handle(DeleteDoctorRequest request, CancellationToken cancellationToken)
        {

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            string deleteString = @$"DELETE FROM Doctors WHERE Id = {request.Id};";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(deleteString, connection);
                    int num = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("doctor not found");
                }
            }
                
            return request.Id;
        }
    }
}
