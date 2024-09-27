using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientRedactCommand : IRequestHandler<PatientRedactRequest, Unit>
    {
        private readonly ApplicationContext _context;

        public PatientRedactCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PatientRedactRequest request, CancellationToken cancellationToken)
        {
            string CommandString = @"
if not exists (select 1 from regions where regionnumber = @PatientRegion)
begin
    insert into regions (regionnumber)
    values (@PatientRegion)
end
declare @regionId int = (select id from regions where regionnumber = @PatientRegion)

update patients
set surname = @Surname,
    name = @Name,
    patronymic = @Patronymic,
    address = @Address,
    borntime = @BornTime,
    sex = @Sex,
    patientregionid = @regionId
where id = @PatientId

select @PatientId as id";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(CommandString, connection);
                command.Parameters.AddWithValue("@PatientRegion", request.PatientRegionNumber);
                command.Parameters.AddWithValue("@Surname", request.Surname);
                command.Parameters.AddWithValue("@Name", request.Name);
                command.Parameters.AddWithValue("@Patronymic", request.Patronymic);
                command.Parameters.AddWithValue("@Address", request.Address);
                command.Parameters.AddWithValue("@BornTime", request.BornTime);
                command.Parameters.AddWithValue("@Sex", request.Sex);
                command.Parameters.AddWithValue("@PatientId", request.RedactId); 

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync(); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                    throw; 
                }
            }

            return Unit.Value;
        }
    }
}
