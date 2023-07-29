using Abp.Application.Services.Dto;

namespace Lee.EMS.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

