
using Azure;
using Gridify;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DoctorsListCommand : IRequestHandler<DoctorsListRequest, List<DoctorsListResponse>>
    {
        private readonly ApplicationContext _context;
        public DoctorsListCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorsListResponse>> Handle(DoctorsListRequest request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy
            };
            List<DoctorsListResponse> doctors = new List<DoctorsListResponse>();
            string CommandStr = @"
SELECT
    d.Id AS DoctorID,
    d.FIO,
    dr.RoomNumber,
    s.SpecializationName,
    drg.RegionNumber
FROM dbo.Doctors d
JOIN dbo.DoctorsRooms dr ON d.DoctorsRoomId = dr.Id
JOIN dbo.Specializations s ON d.SpecializationId = s.Id
JOIN dbo.Regions drg ON d.DoctorsRegionId = drg.Id";
                string connectionString = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command = new SqlCommand(CommandStr, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DoctorsListResponse doctor = new DoctorsListResponse();
                                doctor.Id = (int)reader["DoctorID"];
                                doctor.FIO = (string)reader["FIO"];
                                doctor.RoomNumber = (int)reader["RoomNumber"];
                                doctor.SpecializationName = (string)reader["SpecializationName"];
                                doctor.Region = (int)reader["RegionNumber"];
                                doctors.Add(doctor);
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

                var filteredAndSortedQuery = doctors
            .AsQueryable()
            .ApplyFiltering(gridifyQuery)
            .ApplyOrdering(gridifyQuery);

                var paginatedResult = await Task.Run(() => filteredAndSortedQuery
                    .Skip((request.Page - 1) * 50)
                    .Take(20)
                    .ToList());

                return paginatedResult;
        }
    }
}
