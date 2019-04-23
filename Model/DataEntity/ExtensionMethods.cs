using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.basis;
using Model.Locale;

namespace Model.DataEntity
{
    public static partial class ExtensionMethods
    {
        public static InvoiceUserCarrierType GetDefaultUserCarrierType<TEntity>(this  GenericManager<EIVOEntityDataContext, TEntity> mgr, String typeName) 
            where TEntity : class,new()
        {
            return mgr.GetTable<InvoiceUserCarrierType>().Where(t => t.CarrierType == typeName).FirstOrDefault();
        }

        public static int? GetMemberCodeID<TEntity>(this  GenericManager<EIVOEntityDataContext, TEntity> mgr, String hashID)
            where TEntity : class,new()
        {
            var item = mgr.GetTable<MemberCode>().Where(m => m.HashID == hashID).FirstOrDefault();
            return item != null ? item.CodeID : (int?)null;
        }

        public static String GetMaskInvoiceNo(this InvoiceItem item)
        {
            return String.Format("{0}{1}", item.TrackCode, item.DonateMark == "0" ? item.No : item.No.Substring(0, 5) + "***");
        }

        public static string WinningTypeTransform(this String typeValue)
        {
            switch(typeValue)
            {
                case "0":
                    return "特獎";
                case "1":
                    return "頭獎";
                case "2":
                    return "二獎";
                case "3":
                    return "三獎";
                case "4":
                    return "四獎";
                case "5":
                    return "五獎";
                case "6":
                    return "六獎";
                default:
                    return typeValue;
            }
        }

        public static void ResetDocumentDispatch(this CDS_Document item, Naming.DocumentTypeDefinition docType)
        {
            if (!item.DocumentDispatches.Any(d => d.TypeID == (int)docType))
            {
                item.DocumentDispatches.Add(new DocumentDispatch
                {
                    TypeID = (int)docType
                });
            }
        }

        public static bool IsB2C(this InvoiceBuyer buyer)
        {
            return buyer.ReceiptNo == "0000000000";
        }

        public static bool MoveToNextStep(this CDS_Document item, GenericManager<EIVOEntityDataContext> mgr)
        {
            //var flowStep = item.DocumentFlowStep;
            using (EIVOEntityManager<CDS_Document> worker = new EIVOEntityManager<CDS_Document>())
            {
                var DocItem = worker.GetTable<CDS_Document>().Where(d => d.DocID.Equals(item.DocID)).FirstOrDefault();
                var flowStep = DocItem.DocumentFlowStep;
                if (flowStep != null)
                {
                    var currentStep = worker.GetTable<DocumentFlowControl>().Where(f => f.StepID == flowStep.CurrentFlowStep).First();
                    if (currentStep.NextStep.HasValue)
                    {
                        var nextStep = currentStep.NextStepItem;
                        flowStep.CurrentFlowStep = nextStep.StepID;
                        DocItem.CurrentStep = nextStep.LevelID;

                        worker.GetTable<DocumentProcessLog>().InsertOnSubmit(new DocumentProcessLog
                        {
                            DocID = flowStep.DocID,
                            StepDate = DateTime.Now,
                            FlowStep = nextStep.LevelID
                        });

                        worker.SubmitChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsEnterpriseGroupMember(this Organization org)
        {
            return org.EnterpriseGroupMember.Count > 0;
        }

        public static Organization GetOrganizationByThumbprint(this GenericManager<EIVOEntityDataContext> mgr, String thumbprint)
        {
            Organization item = mgr.GetTable<Organization>().Where(t => t.OrganizationToken.Thumbprint == thumbprint).FirstOrDefault();
            if (item == null)
            {
                item = mgr.GetTable<Organization>().Where(t => t.OrganizationStatus.UserToken.Thumbprint == thumbprint).FirstOrDefault();
            }
            return item;
        }

        public static IQueryable<UserProfile> GetUserListByCompanyID(this GenericManager<EIVOEntityDataContext> mgr, int? companyID)
        {
            return mgr.GetTable<OrganizationCategory>()
                .Where(c => c.CompanyID == companyID
                    && c.CategoryID == (int)Naming.CategoryID.COMP_ENTERPRISE_GROUP)
                .Join(mgr.GetTable<UserRole>(), c => c.OrgaCateID, r => r.OrgaCateID, (c, r) => r)
                .Select(r => r.UserProfile);
        }

    }

    public partial class EIVOEntityManager<TEntity> : GenericManager<EIVOEntityDataContext, TEntity>
        where TEntity : class,new()
    {
        public EIVOEntityManager() : base() { }
        public EIVOEntityManager(GenericManager<EIVOEntityDataContext> manager) : base(manager) { }

        protected virtual void applyProcessFlow(CDS_Document doc, int ownerID, Naming.B2BInvoiceDocumentTypeDefinition typeID, Naming.InvoiceCenterBusinessType businessID)
        {
            var flow = this.GetTable<DocumentTypeFlow>().Where(f => f.TypeID == (int)typeID
                && f.CompanyID == ownerID && f.BusinessID == (int)businessID).FirstOrDefault();

            if (flow != null && flow.DocumentFlow.InitialStep.HasValue)
            {
                var initialStep = flow.DocumentFlow.DocumentFlowControl;
                doc.CurrentStep = initialStep.LevelID;

                doc.DocumentFlowStep = new DocumentFlowStep
                {
                    CurrentFlowStep = initialStep.StepID
                };
            }
        }

        protected virtual void applyProcessFlow(CDS_Document doc, Naming.B2BInvoiceDocumentTypeDefinition typeID)
        {
            var flow = this.GetTable<CommonDocumentTypeFlow>().Where(f => f.TypeID == (int)typeID).FirstOrDefault();

            if (flow != null && flow.DocumentFlow.InitialStep.HasValue)
            {
                var initialStep = flow.DocumentFlow.DocumentFlowControl;
                doc.CurrentStep = initialStep.LevelID;

                doc.DocumentFlowStep = new DocumentFlowStep
                {
                    CurrentFlowStep = initialStep.StepID
                };
            }
        }

        public EIVOEntityDataContext Context
        {
            get
            {
                return (EIVOEntityDataContext)this._db;
            }
        }

    }

    public partial class InvoiceDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceItem> { }
    public partial class UserProfileDataSource : LinqToSqlDataSource<EIVOEntityDataContext, UserProfile> { }
    public partial class OrganizationDataSource : LinqToSqlDataSource<EIVOEntityDataContext, Organization> { }
    public partial class InvoiceUserCarrierDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceUserCarrier> { }
    public partial class ExceptionLogDataSource : LinqToSqlDataSource<EIVOEntityDataContext, ExceptionLog> { }
    public partial class AllowanceDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceAllowance> { }
    public partial class InvoiceTrackCodeDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceTrackCode> { }
    public partial class UserRoleDefinitionDataSource : LinqToSqlDataSource<EIVOEntityDataContext, UserRoleDefinition> { }
    public partial class CategoryDefinitionDataSource : LinqToSqlDataSource<EIVOEntityDataContext, CategoryDefinition> { }
    public partial class MenuControlDataSource : LinqToSqlDataSource<EIVOEntityDataContext, MenuControl> { }
    public partial class UserMenuDataSource : LinqToSqlDataSource<EIVOEntityDataContext, UserMenu> { }
    public partial class OrganizationCategoryDataSource : LinqToSqlDataSource<EIVOEntityDataContext, OrganizationCategory> { }
    public partial class UserRoleDataSource : LinqToSqlDataSource<EIVOEntityDataContext, UserRole> { }
    public partial class OrganizationCategoryUserRoleDataSource : LinqToSqlDataSource<EIVOEntityDataContext, OrganizationCategoryUserRole> { }
    public partial class DocumentDataSource : LinqToSqlDataSource<EIVOEntityDataContext, CDS_Document> { }
    public partial class EnterpriseGroupMemberDataSource : LinqToSqlDataSource<EIVOEntityDataContext, EnterpriseGroupMember> { }
    public partial class BusinessRelationshipDataSource : LinqToSqlDataSource<EIVOEntityDataContext, BusinessRelationship> { }
    public partial class EnterpriseGroupDataSource : LinqToSqlDataSource<EIVOEntityDataContext, EnterpriseGroup> { }
    public partial class InvoiceNoIntervalDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceNoInterval> { }
    public partial class InvoiceProductItemDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceProductItem> { }
    public partial class AllowanceDetailDataSource : LinqToSqlDataSource<EIVOEntityDataContext, InvoiceAllowanceDetail> { }
    public partial class OrganizationTokenDataSource : LinqToSqlDataSource<EIVOEntityDataContext, OrganizationToken> { }
    public partial class CALogDataSource : LinqToSqlDataSource<EIVOEntityDataContext, CALog> { }
    public partial class ReceiptDataSource : LinqToSqlDataSource<EIVOEntityDataContext, ReceiptItem> { }
    public partial class ReceiptDetailDataSource : LinqToSqlDataSource<EIVOEntityDataContext, ReceiptDetail> { }
    public partial class AnnouncementRECDataSource : LinqToSqlDataSource<EIVOEntityDataContext, Announcement_REC> { }
   
   
}
