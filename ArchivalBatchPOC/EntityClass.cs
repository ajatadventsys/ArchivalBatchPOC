using System;
using System.ComponentModel.DataAnnotations;

//New Table Archival_Schedule
public class ArchivalSchedule
{
    [Key]
    public long? SystemId { get; set; }

    public string ProjectCode { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string Status { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string Remarks { get; set; }
    public DateTime? LastUpdate { get; set; }
}

//Primary Tables
public class AuditHistory
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string LoginId { get; set; }
    public string ProjectCode { get; set; }
    public string PageName { get; set; }
    public string ModuleName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string SystemRemarks { get; set; }
    public string UserRemarks { get; set; }
    public long TransId { get; set; }
}

public class BarCodeResult
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string ProjectCode { get; set; }
    public string RequisitionId { get; set; }
    public string BarCodeValue { get; set; }
    public string TestResult { get; set; }
    public string TestResult1 { get; set; }
    public string TestResult2 { get; set; }
    public string SectionName { get; set; }
    public string TestName { get; set; }
    public string TestMatrix { get; set; }
    public string TestCode { get; set; }
    public string DefaultUnit { get; set; }
    public string Methodology { get; set; }
    public string TestType { get; set; }
    public string AliasName { get; set; }
    public string ResultType { get; set; }
    public string ResultRemarks { get; set; }
    public string DefaultRemarks { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string InstrumentName { get; set; }
    public string TestFlag { get; set; }
    public string ReportComments { get; set; }
    public string CheckedBy { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? CheckedDateTime { get; set; }
    public DateTime? ApprovedDateTime { get; set; }
    public bool? ProcessingStatus { get; set; }
    public DateTime? ProcessingDateTime { get; set; }
    public string EditReason { get; set; }
    public string AGFlag { get; set; }
    public bool? WorkSheetStatus { get; set; }
    public string LowValue { get; set; }
    public string HighValue { get; set; }
    public string SAPProjectCode { get; set; }
    public string SAPTestCode { get; set; }
    public string WorkSheetValue { get; set; }
    public bool? IsCalculated { get; set; }
    public long? ReportGroup { get; set; }
    public string ResultInRange { get; set; }
    public string ResultOutRange { get; set; }
    public string Status { get; set; }
    public string QCApprovedBy { get; set; }
    public DateTime? QCApprovedDateTime { get; set; }
    public bool? IsTransferResultCompleted { get; set; }
    public bool? IsCalculateFlaggingCompleted { get; set; }
    public bool? IsCalculateValuesCompleted { get; set; }
    public bool? IsApproveResultsCompled { get; set; }
    public bool? IsGenerateReportsCompleted { get; set; }
}

public class RegBarCode
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string RequisitionId { get; set; }
    public string ProjectCode { get; set; }
    public string BarCodeValue { get; set; }
    public string Status { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string ReceivedCondition { get; set; }
    public string Remarks { get; set; }
    public string TubeName { get; set; }
    public string Panel { get; set; }
    public string CustomField1 { get; set; }
    public string CustomField2 { get; set; }
    public string EditReason { get; set; }
    public string FreezerName { get; set; }
    public string FreezerPosition { get; set; }
    public string ReferanceLab { get; set; }
    public string AnalysisType { get; set; }
    public string TubeType { get; set; }
    public bool? BarCodePrintStatus { get; set; }
    public bool? DiscardStatus { get; set; }
}

public class RegistrationInfo
{
    [Key]
    public long SystemId { get; set; } // Primary Key


    public string RequisitionId { get; set; }
    public string ProjectCode { get; set; }
    public string VisitName { get; set; }
    public string PatientId { get; set; }
    public string PatientInitials { get; set; }
    public string PatientName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public long? AgeValue { get; set; }
    public string Gender { get; set; }
    public string FastingStatus { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public string PsoriaticStatus { get; set; }
    public string DrugIntake { get; set; }
    public DateTime? DateLastIntake { get; set; }
    public string ReferringDoctor { get; set; }
    public string AccessionerInitials { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? CollectionDate { get; set; }
    public DateTime? ProcessingDate { get; set; }
    public DateTime? ReceivedDateTime { get; set; }
    public string Remarks { get; set; }
    public string RegField1 { get; set; }
    public string RegField2 { get; set; }
    public string RegField3 { get; set; }
    public string RegField4 { get; set; }
    public string RegField5 { get; set; }
    public string RegField6 { get; set; }
    public string SiteNo { get; set; }
    public string SiteName { get; set; }
    public string ProtocolNumber { get; set; }
    public string SiteAddress { get; set; }
    public string StudyId { get; set; }
    public string SubjectId { get; set; }
    public bool? RegistrationCheckStatus { get; set; }
    public bool? ResultCheckStatus { get; set; }
    public bool? ApprovedStatus { get; set; }
    public bool? HoldStatus { get; set; }
    public bool? IsLock { get; set; }
    public bool? RegistrationStatus { get; set; }
    public string CheckedComments { get; set; }
    public string ApprovalComments { get; set; }
    public string EditReason { get; set; }
    public bool? ReportGeneratedStatus { get; set; }
    public DateTime? ReportGeneratedOn { get; set; }
    public string RegisteredBy { get; set; }
    public long? ProfileNo { get; set; }
    public bool? PrintStatus { get; set; }
    public bool? EmailStatus { get; set; }
    public string EmailID { get; set; }
    public string QCApprovalComments { get; set; }
    public bool? QCApprovedStatus { get; set; }
    public bool? Temp { get; set; }
}

public class AutoApprovalLog
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string RequisitionId { get; set; }
    public int? CountBCResults { get; set; }
    public int? CountTRF { get; set; }
    public int? CountBCMatch { get; set; }
    public string SectionName { get; set; }
    public string ApprovedBy { get; set; }
    public string RegField6 { get; set; }
    public DateTime AutoProcessingDateTime { get; set; }
}

//Secondary Tables
public class AuditHistoryArchival
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string LoginId { get; set; }
    public string ProjectCode { get; set; }
    public string PageName { get; set; }
    public string ModuleName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string SystemRemarks { get; set; }
    public string UserRemarks { get; set; }
    public long TransId { get; set; }
}

public class BarCodeResultArchival
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string ProjectCode { get; set; }
    public string RequisitionId { get; set; }
    public string BarCodeValue { get; set; }
    public string TestResult { get; set; }
    public string TestResult1 { get; set; }
    public string TestResult2 { get; set; }
    public string SectionName { get; set; }
    public string TestName { get; set; }
    public string TestMatrix { get; set; }
    public string TestCode { get; set; }
    public string DefaultUnit { get; set; }
    public string Methodology { get; set; }
    public string TestType { get; set; }
    public string AliasName { get; set; }
    public string ResultType { get; set; }
    public string ResultRemarks { get; set; }
    public string DefaultRemarks { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string InstrumentName { get; set; }
    public string TestFlag { get; set; }
    public string ReportComments { get; set; }
    public string CheckedBy { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? CheckedDateTime { get; set; }
    public DateTime? ApprovedDateTime { get; set; }
    public bool? ProcessingStatus { get; set; }
    public DateTime? ProcessingDateTime { get; set; }
    public string EditReason { get; set; }
    public string AGFlag { get; set; }
    public bool? WorkSheetStatus { get; set; }
    public string LowValue { get; set; }
    public string HighValue { get; set; }
    public string SAPProjectCode { get; set; }
    public string SAPTestCode { get; set; }
    public string WorkSheetValue { get; set; }
    public bool? IsCalculated { get; set; }
    public long? ReportGroup { get; set; }
    public string ResultInRange { get; set; }
    public string ResultOutRange { get; set; }
    public string Status { get; set; }
    public string QCApprovedBy { get; set; }
    public DateTime? QCApprovedDateTime { get; set; }
    public bool? IsTransferResultCompleted { get; set; }
    public bool? IsCalculateFlaggingCompleted { get; set; }
    public bool? IsCalculateValuesCompleted { get; set; }
    public bool? IsApproveResultsCompled { get; set; }
    public bool? IsGenerateReportsCompleted { get; set; }
}

public class RegBarCodeArchival
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string RequisitionId { get; set; }
    public string ProjectCode { get; set; }
    public string BarCodeValue { get; set; }
    public string Status { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string ReceivedCondition { get; set; }
    public string Remarks { get; set; }
    public string TubeName { get; set; }
    public string Panel { get; set; }
    public string CustomField1 { get; set; }
    public string CustomField2 { get; set; }
    public string EditReason { get; set; }
    public string FreezerName { get; set; }
    public string FreezerPosition { get; set; }
    public string ReferanceLab { get; set; }
    public string AnalysisType { get; set; }
    public string TubeType { get; set; }
    public bool? BarCodePrintStatus { get; set; }
    public bool? DiscardStatus { get; set; }
}

public class RegistrationInfoArchival
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    
public string RequisitionId { get; set; }
    public string ProjectCode { get; set; }
    public string VisitName { get; set; }
    public string PatientId { get; set; }
    public string PatientInitials { get; set; }
    public string PatientName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public long? AgeValue { get; set; }
    public string Gender { get; set; }
    public string FastingStatus { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public string PsoriaticStatus { get; set; }
    public string DrugIntake { get; set; }
    public DateTime? DateLastIntake { get; set; }
    public string ReferringDoctor { get; set; }
    public string AccessionerInitials { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? CollectionDate { get; set; }
    public DateTime? ProcessingDate { get; set; }
    public DateTime? ReceivedDateTime { get; set; }
    public string Remarks { get; set; }
    public string RegField1 { get; set; }
    public string RegField2 { get; set; }
    public string RegField3 { get; set; }
    public string RegField4 { get; set; }
    public string RegField5 { get; set; }
    public string RegField6 { get; set; }
    public string SiteNo { get; set; }
    public string SiteName { get; set; }
    public string ProtocolNumber { get; set; }
    public string SiteAddress { get; set; }
    public string StudyId { get; set; }
    public string SubjectId { get; set; }
    public bool? RegistrationCheckStatus { get; set; }
    public bool? ResultCheckStatus { get; set; }
    public bool? ApprovedStatus { get; set; }
    public bool? HoldStatus { get; set; }
    public bool? IsLock { get; set; }
    public bool? RegistrationStatus { get; set; }
    public string CheckedComments { get; set; }
    public string ApprovalComments { get; set; }
    public string EditReason { get; set; }
    public bool? ReportGeneratedStatus { get; set; }
    public DateTime? ReportGeneratedOn { get; set; }
    public string RegisteredBy { get; set; }
    public long? ProfileNo { get; set; }
    public bool? PrintStatus { get; set; }
    public bool? EmailStatus { get; set; }
    public string EmailID { get; set; }
    public string QCApprovalComments { get; set; }
    public bool? QCApprovedStatus { get; set; }
    public bool? Temp { get; set; }
}

public class AutoApprovalLogArchival
{
    [Key]
    public long SystemId { get; set; } // Primary Key

    public string ProjectCode { get; set; }
    public string RequisitionId { get; set; }
    public int? CountBCResults { get; set; }
    public int? CountTRF { get; set; }
    public int? CountBCMatch { get; set; }
    public string SectionName { get; set; }
    public string ApprovedBy { get; set; }
    public string RegField6 { get; set; }
    public DateTime AutoProcessingDateTime { get; set; }

}