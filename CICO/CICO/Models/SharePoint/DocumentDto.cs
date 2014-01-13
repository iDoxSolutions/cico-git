using System;

namespace Cico.Models.SharePoint
{
    public class DocumentDto
    {
        public DateTime? ActivityDate { get; set; }
        public string Activity { get; set; }
        public string DocumentType { get; set; }
        public int ReqId { get; set; }
        public int Status { get; set; }
        public string DocCategory { get; set; }
        public string ContractNo { get; set; }
        public string Url { get; set; }
    }
}