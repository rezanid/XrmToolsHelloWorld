using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using XrmTools.Meta.Attributes;

namespace XrmToolsHelloWorld
{
    public interface IAccountQueryService
    {
        ICollection<Account> GetAccountsByIndustryCode(int industryCode);
    }

    public class AccountQueryService : IAccountQueryService
    {
        private readonly IOrganizationService _service;

        public AccountQueryService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ICollection<Account> GetAccountsByIndustryCode(int industryCode)
        {
            var query = new QueryExpression("account")
            {
                ColumnSet = new ColumnSet("name"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("industrycode", ConditionOperator.Equal, industryCode)
                    }
                },
                PageInfo = new PagingInfo
                {
                    Count = 5000,
                    PageNumber = 1,
                    ReturnTotalRecordCount = false
                },
                NoLock = true
            };

            var results = new List<Account>();

            while (true)
            {
                var page = _service.RetrieveMultiple(query);
                results.AddRange(page.Entities.Select(e => e.ToEntity<Account>()));

                if (!page.MoreRecords) break;

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = page.PagingCookie;
            }

            return results;
        }
    }
}