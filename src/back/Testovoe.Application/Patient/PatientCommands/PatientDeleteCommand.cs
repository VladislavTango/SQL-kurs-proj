
using MediatR;
using Microsoft.Data.SqlClient;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientDeleteCommand : IRequestHandler<PatientDeleteRequest, int>
    {
        private readonly ApplicationContext _context;
        public PatientDeleteCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(PatientDeleteRequest request, CancellationToken cancellationToken)
        {

            string CommandString = $@"delete from Patients where Id = {request.Id}";
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(CommandString, connection);
                    int num = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("patient not found");
                }
            }

          

            return request.Id;
        }
    }
}
