
using Gridify;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Application.Patient.PatientResponse;
using Testovoe.Enums;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientListCommand : IRequestHandler<PatientListRequest, List<PatientListResponse>>
    {
        private readonly ApplicationContext _context;
        public PatientListCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<PatientListResponse>> Handle(PatientListRequest request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy
            };

            string CommandString = $@"
SELECT 
    p.Id,
    p.Surname,
    p.Name,
    p.Patronymic,
    p.Address,
    p.BornTime,
    p.Sex,
    r.RegionNumber 
FROM 
    applicationdb.dbo.Patients p
JOIN 
    applicationdb.dbo.Regions r ON p.PatientRegionId = r.RegionNumber;
";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            List<PatientListResponse> responseList = new List<PatientListResponse>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(CommandString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PatientListResponse responseElem = new();
                            responseElem.Id = (int)reader["Id"];
                            responseElem.Surname = (string)reader["Surname"];
                            responseElem.Name = (string) reader["Name"];
                            responseElem.Patronymic = (string) reader["Patronymic"];
                            responseElem.Address = (string) reader["Address"];
                            responseElem.BornTime = (DateTime) reader["BornTime"];
                            responseElem.Sex = (Sex) reader["Sex"];
                            responseElem.PatientRegionNumber = (int) reader["RegionNumber"];
                            responseList.Add(responseElem);
                        }
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Server error");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving doctor data", ex);
                }
            }

            var filteredAndSortedQuery = responseList
        .AsQueryable()
        .ApplyFiltering(gridifyQuery)
        .ApplyOrdering(gridifyQuery);

            var paginatedResult = await Task.Run(() => filteredAndSortedQuery
                .Skip((request.Page - 1) * 20)
                .Take(20)
                .ToList());

            return paginatedResult;
        }
    }
}
