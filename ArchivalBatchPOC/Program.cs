using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchivalBatchPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the table and Get the Proejctcode and from&To date 
            // Then move the reocrds from primary table to secondary table (alias primary table with _Archival)
            // Delete the records from primary table 

            //Assume that there is a entry in Archival_Schedule table with necessary details
            //get the details using linq and pass the details to move or insert those set of records 
            //to secondary table and delete from the primary table efficiently
            //if the process is successfull move completed in status
            //else if result is failed then failed
            //if it is performing then display in progress
            //if not yet started then open
            //pass the remarks based on the status and move the datetime value when this is 
            //completed to lastupdated
            selectionBasedOnArchivalSchedule();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //get the projectcode, date range from ArchivalSchedule
        private static void selectionBasedOnArchivalSchedule()
        {
            using (var context = new LIMSDevContext())
            {
                try
                {
                    //get all the data as to list
                    var archivalRange = context.ArchivalSchedules.ToList();

                    //loop through each item and call each funciton for each table
                    foreach (var selected in archivalRange)
                    {
                        string projectCode = selected.ProjectCode;
                        // Extract the date part alone
                        DateTime? fromDate = selected.FromDate.HasValue ? (DateTime?)selected.FromDate.Value.Date : null;
                        DateTime? toDate = selected.ToDate.HasValue ? (DateTime?)selected.ToDate.Value.Date : null;

                        //call funciton to move AuditHistory
                        moveAuditHistoryRecords(projectCode, fromDate, toDate);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Inside Archival Schedule Exception: {ex}");
                }
            }
        }

        //Move Audit history records from primary to secondary
        //add function to update the status,remarks,last update in archival schedule table
        private static void moveAuditHistoryRecords(string projectCode, DateTime? fromDate, DateTime? toDate)
        {
            Console.WriteLine($"first :{fromDate} >>> {toDate}");
            using (var limsContext = new LIMSDevContext())
            {
                try
                {
                    // Define the query to filter records based on ProjectCode and CreatedDateTime range
                    var filteredRecords = limsContext.AuditHistorys
                        .Where(ah => ah.ProjectCode == projectCode 
                                && ah.CreatedDateTime >= fromDate
                                && ah.CreatedDateTime <= toDate)
                        .ToList();

                    if (filteredRecords.Any())
                    {
                        Console.WriteLine($"Records for ProjectCode: {projectCode} between {fromDate} and {toDate}:");
                        using(var archivalContext = new ArchivalContext())
                        {
                            //convert to list
                            var archivalRecords = filteredRecords.Select(r => new AuditHistoryArchival
                            {
                                SystemId = r.SystemId,
                                LoginId = r.LoginId,
                                ProjectCode = r.ProjectCode,
                                PageName = r.PageName,
                                ModuleName = r.ModuleName,
                                CreatedDateTime = r.CreatedDateTime,
                                SystemRemarks = r.SystemRemarks,
                                UserRemarks = r.UserRemarks,
                                TransId = r.TransId
                            }).ToList();

                            // Insert into the secondary table
                            archivalContext.AuditHistoryArchivals.AddRange(archivalRecords);
                            int result_1 = archivalContext.SaveChanges();
                            Console.WriteLine($"Audit history archival inserted: {result_1}");

                            // Optionally delete the records from the primary table
                            limsContext.AuditHistorys.RemoveRange(filteredRecords);
                            int result_2 = limsContext.SaveChanges();
                            Console.WriteLine($"Audit history deleted: {result_2}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found matching the criteria.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                }
            }
        }
    }
}
