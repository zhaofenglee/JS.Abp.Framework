using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.BlazorUI;

public abstract class BlazorUIComponentBase: AbpComponentBase
{
    protected int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    protected int CurrentPage { get; set; } = 1;
    protected string CurrentSorting { get; set; }
    protected int TotalCount { get; set; }
    protected bool CanCreate { get; set; }
    protected bool CanEdit { get; set; }
    protected bool CanDelete { get; set; }
    protected BlazorUIComponentBase()
    {
        LocalizationResource = typeof(AbpUiResource);
    }

    protected async Task SetPermissionsAsync(string policyName)
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(policyName + ".Create");
        CanEdit = await AuthorizationService
            .IsGrantedAsync(policyName + ".Edit");
        CanDelete = await AuthorizationService
            .IsGrantedAsync(policyName + ".Delete");
    }
}