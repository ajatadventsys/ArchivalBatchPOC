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

                        ArchiveAndUpdateRecords(projectCode, fromDate, toDate);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Inside Archival Schedule Exception: {ex}");
                }
            }
        }

        private static void ArchiveAndUpdateRecords(string projectCode, DateTime? fromDate, DateTime? toDate)
        {
            // Context for primary table operations
            using (var limsContext = new LIMSDevContext())
            {
                try
                {
                    var archivalSchedule = limsContext.ArchivalSchedules
                        .FirstOrDefault(aiu => aiu.ProjectCode == projectCode
                        && aiu.FromDate >= fromDate
                        && aiu.ToDate <= toDate);

                    if (archivalSchedule == null)
                    {
                        Console.WriteLine("Archival schedule not found.");
                        return;
                    }

                    // Reset remarks
                    archivalSchedule.Remarks = "";

                    // Inprogress status update
                    archivalSchedule.Status = "Inprogress";
                    archivalSchedule.Remarks = "Executing Selected Criteria";
                    archivalSchedule.LastUpdate = DateTime.Now;
                    limsContext.SaveChanges();

                    bool allOperationsSuccessful = true;

                    archivalSchedule.Remarks = "";

                    // Handle AuditHistory
                    var auditFilteredRecords = limsContext.AuditHistorys
                        .Where(ah => ah.ProjectCode == projectCode
                                && ah.CreatedDateTime >= fromDate
                                && ah.CreatedDateTime <= toDate)
                        .ToList();

                    if (auditFilteredRecords.Any())
                    {
                        using (var archivalContext = new ArchivalContext())
                        {
                            var archivalAuditRecords = auditFilteredRecords.Select(r => new AuditHistoryArchival
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

                            archivalContext.AuditHistoryArchivals.AddRange(archivalAuditRecords);
                            int auditHistoryCount = archivalContext.SaveChanges();

                            limsContext.AuditHistorys.RemoveRange(auditFilteredRecords);
                            limsContext.SaveChanges();

                            archivalSchedule.Remarks += $"Audit History Archived {auditHistoryCount} records,";
                        }
                    }
                    else
                    {
                        archivalSchedule.Remarks += "No records found in Audit History,";
                        allOperationsSuccessful = false;
                    }

                    // Handle BarCodeResults
                    var barcodeFilteredRecords = limsContext.BarCodeResults
                        .Where(br => br.ProjectCode == projectCode
                                && br.CheckedDateTime >= fromDate
                                && br.CheckedDateTime <= toDate)
                        .ToList();

                    if (barcodeFilteredRecords.Any())
                    {
                        using (var archivalContext = new ArchivalContext())
                        {
                            var archivalBarcodeRecords = barcodeFilteredRecords.Select(r => new BarCodeResultArchival
                            {
                                SystemId = r.SystemId,
                                ProjectCode = r.ProjectCode,
                                CreatedDateTime = r.CreatedDateTime,
                                RequisitionId = r.RequisitionId,
                                BarCodeValue = r.BarCodeValue,
                                TestResult = r.TestResult,
                                TestResult1 = r.TestResult1,
                                TestResult2 = r.TestResult2,
                                SectionName = r.SectionName,
                                TestName = r.TestName,
                                TestMatrix = r.TestMatrix,
                                TestCode = r.TestCode,
                                DefaultUnit = r.DefaultUnit,
                                Methodology = r.Methodology,
                                TestType = r.TestType,
                                AliasName = r.AliasName,
                                ResultType = r.ResultType,
                                ResultRemarks = r.ResultRemarks,
                                DefaultRemarks = r.DefaultRemarks,
                                InstrumentName = r.InstrumentName,
                                TestFlag = r.TestFlag,
                                ReportComments = r.ReportComments,
                                CheckedBy = r.CheckedBy,
                                ApprovedBy = r.ApprovedBy,
                                CheckedDateTime = r.CheckedDateTime,
                                ApprovedDateTime = r.ApprovedDateTime,
                                ProcessingStatus = r.ProcessingStatus,
                                ProcessingDateTime = r.ProcessingDateTime,
                                EditReason = r.EditReason,
                                AGFlag = r.AGFlag,
                                WorkSheetStatus = r.WorkSheetStatus,
                                LowValue = r.LowValue,
                                HighValue = r.HighValue,
                                SAPProjectCode = r.SAPProjectCode,
                                SAPTestCode = r.SAPTestCode,
                                WorkSheetValue = r.WorkSheetValue,
                                IsCalculated = r.IsCalculated,
                                ReportGroup = r.ReportGroup,
                                ResultInRange = r.ResultInRange,
                                ResultOutRange = r.ResultOutRange,
                                Status = r.Status,
                                QCApprovedBy = r.QCApprovedBy,
                                QCApprovedDateTime = r.QCApprovedDateTime,
                                IsTransferResultCompleted = r.IsTransferResultCompleted,
                                IsCalculateFlaggingCompleted = r.IsCalculateFlaggingCompleted,
                                IsCalculateValuesCompleted = r.IsCalculateValuesCompleted,
                                IsApproveResultsCompled = r.IsApproveResultsCompled,
                                IsGenerateReportsCompleted = r.IsGenerateReportsCompleted
                            }).ToList();

                            archivalContext.BarCodeResultArchivals.AddRange(archivalBarcodeRecords);
                            int barCodeResultCount = archivalContext.SaveChanges();

                            limsContext.BarCodeResults.RemoveRange(barcodeFilteredRecords);
                            limsContext.SaveChanges();

                            archivalSchedule.Remarks += $"BarCodeResult Archived {barCodeResultCount} records,";
                        }
                    }
                    else
                    {
                        archivalSchedule.Remarks += "No records found in BarCodeResult,";
                        allOperationsSuccessful = false;
                    }

                    // Handle RegBarCode
                    var regbarcodeFilteredRecords = limsContext.RegBarCodes
                        .Where(rb => rb.ProjectCode == projectCode
                                && rb.CollectionDate >= fromDate
                                && rb.CollectionDate <= toDate)
                        .ToList();

                    if (regbarcodeFilteredRecords.Any())
                    {
                        using (var archivalContext = new ArchivalContext())
                        {
                            var archivalRegBarcodeRecords = regbarcodeFilteredRecords.Select(r => new RegBarCodeArchival
                            {
                                SystemId = r.SystemId,
                                RequisitionId = r.RequisitionId,
                                ProjectCode = r.ProjectCode,
                                BarCodeValue = r.BarCodeValue,
                                Status = r.Status,
                                CreatedDateTime = r.CreatedDateTime,
                                CollectionDate = r.CollectionDate,
                                ReceivedCondition = r.ReceivedCondition,
                                Remarks = r.Remarks,
                                TubeName = r.TubeName,
                                Panel = r.Panel,
                                CustomField1 = r.CustomField1,
                                CustomField2 = r.CustomField2,
                                EditReason = r.EditReason,
                                FreezerName = r.FreezerName,
                                FreezerPosition = r.FreezerPosition,
                                ReferanceLab = r.ReferanceLab,
                                AnalysisType = r.AnalysisType,
                                TubeType = r.TubeType,
                                BarCodePrintStatus = r.BarCodePrintStatus,
                                DiscardStatus = r.DiscardStatus
                            }).ToList();

                            archivalContext.RegBarCodeArchivals.AddRange(archivalRegBarcodeRecords);
                            int regBarCodeCount = archivalContext.SaveChanges();

                            limsContext.RegBarCodes.RemoveRange(regbarcodeFilteredRecords);
                            limsContext.SaveChanges();

                            archivalSchedule.Remarks += $"RegBarCode Archived {regBarCodeCount} records,";
                        }
                    }
                    else
                    {
                        archivalSchedule.Remarks += "No records found in RegBarCode,";
                        allOperationsSuccessful = false;
                    }

                    // Handle RegistrationInfo
                    var reginfoFilteredRecords = limsContext.RegistrationInfos
                        .Where(ri => ri.ProjectCode == projectCode
                                && ri.CollectionDate >= fromDate
                                && ri.CollectionDate <= toDate)
                        .ToList();

                    if (reginfoFilteredRecords.Any())
                    {
                        using (var archivalContext = new ArchivalContext())
                        {
                            var archivalRegInfoRecords = reginfoFilteredRecords.Select(r => new RegistrationInfoArchival
                            {
                                SystemId = r.SystemId,
                                RequisitionId = r.RequisitionId,
                                ProjectCode = r.ProjectCode,
                                VisitName = r.VisitName,
                                PatientId = r.PatientId,
                                PatientInitials = r.PatientInitials,
                                PatientName = r.PatientName,
                                DateOfBirth = r.DateOfBirth,
                                AgeValue = r.AgeValue,
                                Gender = r.Gender,
                                FastingStatus = r.FastingStatus,
                                Height = r.Height,
                                Weight = r.Weight,
                                PsoriaticStatus = r.PsoriaticStatus,
                                DrugIntake = r.DrugIntake,
                                DateLastIntake = r.DateLastIntake,
                                ReferringDoctor = r.ReferringDoctor,
                                AccessionerInitials = r.AccessionerInitials,
                                CreatedOn = r.CreatedOn,
                                CollectionDate = r.CollectionDate,
                                ProcessingDate = r.ProcessingDate,
                                ReceivedDateTime = r.ReceivedDateTime,
                                Remarks = r.Remarks,
                                RegField1 = r.RegField1,
                                RegField2 = r.RegField2,
                                RegField3 = r.RegField3,
                                RegField4 = r.RegField4,
                                RegField5 = r.RegField5,
                                RegField6 = r.RegField6,
                                SiteNo = r.SiteNo,
                                SiteName = r.SiteName,
                                ProtocolNumber = r.ProtocolNumber,
                                SiteAddress = r.SiteAddress,
                                StudyId = r.StudyId,
                                SubjectId = r.SubjectId,
                                RegistrationCheckStatus = r.RegistrationCheckStatus,
                                RegistrationStatus = r.RegistrationStatus,
                                CheckedComments = r.CheckedComments,
                                ApprovalComments = r.ApprovalComments,
                                EditReason = r.EditReason,
                                ReportGeneratedStatus = r.ReportGeneratedStatus,
                                ReportGeneratedOn = r.ReportGeneratedOn,
                                RegisteredBy = r.RegisteredBy,
                                ProfileNo = r.ProfileNo,
                                PrintStatus = r.PrintStatus,
                                EmailStatus = r.EmailStatus,
                                EmailID = r.EmailID,
                                QCApprovalComments = r.QCApprovalComments,
                                QCApprovedStatus = r.QCApprovedStatus,
                                Temp = r.Temp
                            }).ToList();

                            archivalContext.RegistrationInfoArchivals.AddRange(archivalRegInfoRecords);
                            int registartionInfoCount = archivalContext.SaveChanges();

                            limsContext.RegistrationInfos.RemoveRange(reginfoFilteredRecords);
                            limsContext.SaveChanges();

                            archivalSchedule.Remarks += $"RegistrationInfo Archived {registartionInfoCount} records,";
                        }
                    }
                    else
                    {
                        archivalSchedule.Remarks += "No records found in RegistrationInfo,";
                        allOperationsSuccessful = false;
                    }

                    // Handle AutoApprovalLog
                    // Join AutoApprovalLogs with RegistrationInfos based on RequisitionId
                    var autoapprovalFilteredRecords = from al in limsContext.AutoApprovalLogs
                                                      join ri in limsContext.RegistrationInfos
                                                      on al.RequisitionId equals ri.RequisitionId
                                                      where al.AutoProcessingDateTime >= fromDate
                                                            && al.AutoProcessingDateTime <= toDate
                                                      select new
                                                      {
                                                          AutoApproval = al,
                                                          Registration = ri
                                                      };

                    // Convert the result to a list
                    var autoapprovalFilteredRecordsList = autoapprovalFilteredRecords.ToList();

                    if (autoapprovalFilteredRecordsList.Any())
                    {
                        using (var archivalContext = new ArchivalContext())
                        {
                            // Prepare archival records
                            var archivalAutoApprovalRecords = autoapprovalFilteredRecordsList.Select(record => new AutoApprovalLogArchival
                            {
                                SystemId = record.AutoApproval.SystemId,
                                RequisitionId = record.AutoApproval.RequisitionId,
                                ProjectCode = record.Registration.ProjectCode, // Use ProjectCode from RegistrationInfos
                                CountBCResults = record.AutoApproval.CountBCResults,
                                CountTRF = record.AutoApproval.CountTRF,
                                CountBCMatch = record.AutoApproval.CountBCMatch,
                                SectionName = record.AutoApproval.SectionName,
                                ApprovedBy = record.AutoApproval.ApprovedBy,
                                RegField6 = record.AutoApproval.RegField6,
                                AutoProcessingDateTime = record.AutoApproval.AutoProcessingDateTime
                            }).ToList();

                            // Insert records into archival database
                            archivalContext.AutoApprovalLogArchivals.AddRange(archivalAutoApprovalRecords);
                            int autoApprovalLogCount = archivalContext.SaveChanges(); // Count of records inserted

                            // Delete records from original database
                            limsContext.AutoApprovalLogs.RemoveRange(autoapprovalFilteredRecordsList.Select(record => record.AutoApproval));
                            limsContext.SaveChanges(); // Ensure deletion is saved

                            // Update remarks with the count of archived and deleted records
                            archivalSchedule.Remarks += $"AutoApprovalLog Archived {autoApprovalLogCount} records,";
                        }
                    }
                    else
                    {
                        // Update remarks for cases where no records were found
                        archivalSchedule.Remarks += "No records found in AutoApprovalLog,";
                        allOperationsSuccessful = false;
                    }

                    // Final update of status
                    archivalSchedule.Status = "Completed";
                    archivalSchedule.LastUpdate = DateTime.Now;
                    limsContext.SaveChanges();

                    Console.WriteLine($"Archival Schedule status and Remarks Updated: {archivalSchedule.Remarks}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                }
            }
        }
    }
}
