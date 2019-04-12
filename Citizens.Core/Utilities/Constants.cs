

namespace Citizens.Core
{
    using Citizens.Core.Models;
    using Microsoft.SqlServer.Server;
    using System.Data;
    using System.Data.SqlTypes;
    using System.Collections.Generic;
    public static class Constants
    {
        public static readonly SqlMetaData[] SKUWorkItemStructure = new SqlMetaData[]
        {
            new SqlMetaData("Id",SqlDbType.BigInt),
            new SqlMetaData("DealId",SqlDbType.NVarChar,50),
            new SqlMetaData("State",SqlDbType.NVarChar,100),
            new SqlMetaData("CreatedTime",SqlDbType.BigInt),
            new SqlMetaData("ChangedDate",SqlDbType.BigInt),
            new SqlMetaData("InitialSubmissionDate",SqlDbType.BigInt),
            new SqlMetaData("WorkOrderSent2PartnerforSignature",SqlDbType.BigInt),
            new SqlMetaData("WorkOrderPartnerSignature",SqlDbType.BigInt),
            new SqlMetaData("MSBASent2PartnerforSignature",SqlDbType.BigInt),
            new SqlMetaData("MBSASignatureDate",SqlDbType.BigInt),
            new SqlMetaData("TrackingID",SqlDbType.NVarChar,100),
            new SqlMetaData("SAMName",SqlDbType.NVarChar,100),
            new SqlMetaData("SAMLeader",SqlDbType.NVarChar,100),
            new SqlMetaData("PartnerRegion",SqlDbType.NVarChar,100),
            new SqlMetaData("PartnerName",SqlDbType.NVarChar,100),
            new SqlMetaData("CompanyName",SqlDbType.NVarChar,100),
            new SqlMetaData("TPID",SqlDbType.NVarChar,100),
            new SqlMetaData("ContractID",SqlDbType.NVarChar,100),
            new SqlMetaData("CompassOneLink",SqlDbType.NVarChar,100),
            new SqlMetaData("PEGOOwner",SqlDbType.NVarChar,100),
            new SqlMetaData("OffineWorkOrders",SqlDbType.NVarChar,100),
            new SqlMetaData("DealStatus",SqlDbType.NVarChar,100),
            new SqlMetaData("SubState",SqlDbType.NVarChar,100),
            new SqlMetaData("ContractStatus",SqlDbType.NVarChar,100),
            new SqlMetaData("OrderState",SqlDbType.NVarChar,100),
            new SqlMetaData("Metadata",SqlDbType.NVarChar,SqlMetaData.Max)
        };

        public static readonly SqlMetaData[] C1ContractStructure = new SqlMetaData[] {
            new SqlMetaData("TPID"                      ,SqlDbType.NVarChar,100),
            new SqlMetaData("DealId",                   SqlDbType.NVarChar,50),
            new SqlMetaData("PackageType"               ,SqlDbType.NVarChar,100),
            new SqlMetaData("AccountName"               ,SqlDbType.NVarChar,100),
            new SqlMetaData("PackageTitle"              ,SqlDbType.NVarChar,100),
            new SqlMetaData("ContractHyperlink"         ,SqlDbType.NVarChar,100),
            new SqlMetaData("OpportunityID"             ,SqlDbType.NVarChar,100),
            new SqlMetaData("ContractTitle"             ,SqlDbType.NVarChar,100),
            new SqlMetaData("ClarifyId"                 ,SqlDbType.NVarChar,100),
            new SqlMetaData("DealOwnereMail"            ,SqlDbType.NVarChar,100),
            new SqlMetaData("ContractCreatedDate"       ,SqlDbType.BigInt),
            new SqlMetaData("ApprovalStartDate"         ,SqlDbType.BigInt),
            new SqlMetaData("ApprovalCompletedDate"     ,SqlDbType.BigInt),
            new SqlMetaData("PreSignatureReviewStartedDate" ,SqlDbType.BigInt),
            new SqlMetaData("PreSignatureReviewCompletedDate"   ,SqlDbType.BigInt),
            new SqlMetaData("ContractCustomerSignedDate"        ,SqlDbType.BigInt),
            new SqlMetaData("ContractFullyExecutedDate"         ,SqlDbType.BigInt),
            new SqlMetaData("PostSignatureReviewStartedDate"    ,SqlDbType.BigInt),
            new SqlMetaData("PostSignatureReviewCompletedDate"  ,SqlDbType.BigInt),
            new SqlMetaData("AgreementSetupStartedDate"         ,SqlDbType.BigInt),
            new SqlMetaData("AgreementSetupCompletedDate"       ,SqlDbType.BigInt),
            new SqlMetaData("EstimatedStartDate"                ,SqlDbType.BigInt),
            new SqlMetaData("EstimatedEndDate"                  ,SqlDbType.BigInt),
            new SqlMetaData("MSANumber"                         ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("ServicesExecutiveName"             ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("CreatedBy"                         ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("MSAName"                           ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("PaymentType"                       ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("FinanceSalesLocation"              ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("CurrencyCode"                      ,SqlDbType.NVarChar,100) ,
            new SqlMetaData("IsGlobalDeal"                      ,SqlDbType.Bit) ,
            new SqlMetaData("PublicSectorContractFlag"          ,SqlDbType.NVarChar,100),
            new SqlMetaData("DealCount"                        ,SqlDbType.Int),
            new SqlMetaData("PlannedContractPriceToCustomerUSD" ,SqlDbType.Decimal)
        };

        public static class VSOConfiguration
        {
            /* for DevOps test
            public const string Organization = "operationscsp-dryrun";
            public const string Project = "CSP";
            public const string Type = "SKU Purchase";
            public const string PersonalAccessToken = "wcto7h66nmnhnv6ncraq7damqmi7obb7uwetqztopcig6csnt73a";
            public const string Team = "ASfP";
            */

            public const string Organization = "PartnerPremierOfferings";
            public const string Project = "CSP";
            public const string Type = "SKU Purchase";
            public const string PersonalAccessToken = "kz3m7uchgefon7xr72altoyjwvblc6qt7lvihte3v7iyviry2jrq";
            public const string Team = "ASfP";
        }

        public const string AZURE_STORAGE_CONNECTIONSTRING = "DefaultEndpointsProtocol=https;AccountName=notifaction;AccountKey=o/RkaI41VSEZTiXGi8Ba5IIViANqvlLbMcUkgjE8ipFzemh/ggj9Jgdx9CfZVvp18dOo5LSgl7A46oz5xJU7IA==;EndpointSuffix=core.windows.net";

        public static AlertTypes[] ALLERTTYPES_DEALSTATUS = new AlertTypes[] {
             AlertTypes.AlertOrange, AlertTypes.AlertRed, AlertTypes.AlertYellow
        };
        public static AlertTypes[] ALLERTTYPES_RENEWAL = new AlertTypes[] {
             AlertTypes.Renewal30, AlertTypes.Renewal60
        };

        public static readonly Dictionary<AlertTypes, string> AlertSubjects = new Dictionary<AlertTypes, string>()
        {
            {AlertTypes.Renewal30,"ASfP 30 Day Reminder" },
            {AlertTypes.Renewal60,"ASfP 60 Day Reminder" },
            {AlertTypes.AlertRed,"ASfP Deal Status Alert: RED" },
            {AlertTypes.AlertOrange,"ASfP Deal Status Alert: Orange" },
            {AlertTypes.AlertYellow,"ASfP Deal Status Alert: Yellow" },
        };
        public static readonly Dictionary<AlertTypes, string> AlertBody= new Dictionary<AlertTypes, string>()
        {
            {AlertTypes.Renewal30,"The ASfP contract will end in the next 30 Days. Please make sure all partner information in VSO is current and up to date immediately." },
            {AlertTypes.Renewal60,"The ASfP contract will end in the next 60 Days. Please make sure all partner information is current and up to date in VSO." },
            {AlertTypes.AlertRed,"Immediate Action Required – Contract service start date may be delayed. Please make sure all updates have been completed. If you have any question, please contact the PEGO Owner." },
            {AlertTypes.AlertOrange,"Action Required: Please make sure all updates have been completed. If you have any question, please contact the PEGO Owner." },
            {AlertTypes.AlertYellow,"Action Required: Please make sure all updates have been completed." },
        };
    }
}
