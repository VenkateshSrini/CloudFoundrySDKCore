using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{
    public class DC_ListTaskRequest
    {
        /// <summary>
    /// Mulytiple App Guid to be seperated by COmma
    /// </summary>
        public string AppGuid { get; set; }
        /// <summary>
        /// Multiple Task Names to be seperated by comma
        /// </summary>
        public string TaskNames { get; set; }
        /// <summary>
        /// Multiple Task Guid to be seperated by comma
        /// </summary>
        public string TaskGuids { get; set; }
        /// <summary>
        /// Multiple Organization needs to be seperated by Comma
        /// </summary>
        public string OrganizationGuids { get; set; }
        /// <summary>
        /// Multiple Space ID will be seperated by comma
        /// </summary>
        public string SpaceGuids { get; set; }
        /// <summary>
        /// Multiple Task states will have to seperated by comma
        /// </summary>
        public string TaskStates { get; set; }
        public int Page { get; set; }
        /// <summary>
        /// Can be =,>,<,>=,<=
        /// </summary>
        public string PageOperator { get; set; }
        /// <summary>
        /// can be min of 1 and amx of 5000
        /// </summary>
        public int PerPageRecords { get; set; }
        /// <summary>
        /// Laue to Sort. Defaults to Ascending. Possible values
        /// created_at Or up-dated_at
        /// </summary>
        public string OrderBy { get; set; }

        public string BuildQueryParams()
        {
            StringBuilder queryParam = new StringBuilder();
            queryParam.Append(string.IsNullOrWhiteSpace(AppGuid) ? string.Empty : $"app_guids={AppGuid}&");
            queryParam.Append(string.IsNullOrWhiteSpace(TaskNames) ? string.Empty : $"names={TaskNames}&");
            queryParam.Append(string.IsNullOrWhiteSpace(TaskGuids) ? string.Empty : $"guids={TaskGuids}&");
            queryParam.Append(string.IsNullOrWhiteSpace(OrganizationGuids) ? string.Empty : $"organization_guids={OrganizationGuids}&");
            queryParam.Append(string.IsNullOrWhiteSpace(SpaceGuids) ? string.Empty : $"space_guids={SpaceGuids}&");
            queryParam.Append(string.IsNullOrWhiteSpace(TaskStates) ? string.Empty : $"states={TaskStates}&");
            string pageOperator = string.IsNullOrWhiteSpace(PageOperator) ? "=" : PageOperator;
            queryParam.Append((Page<=0) ? string.Empty : $"page{pageOperator}{Page}&");
            queryParam.Append(((PerPageRecords<1)||(PerPageRecords>5000)) ? string.Empty : $"per_page={PerPageRecords}&");
            queryParam.Append(string.IsNullOrWhiteSpace(OrderBy) ? string.Empty : $"order_by={OrderBy}&");
            if (queryParam.Length  > 0)
            queryParam.Remove(queryParam.Length - 1, 1);
            return queryParam.ToString();

        }

    }

}
