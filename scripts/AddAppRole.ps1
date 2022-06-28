$TenantID = ""
Connect-AzureAd -TenantId $TenantID

$BlazorWebApp=Get-AzureADServicePrincipal -SearchString "Blazor"
$SecuredFunction=Get-AzureADServicePrincipal -SearchString "SecuredFunction"

New-AzureADServiceAppRoleAssignment -Id $SecuredFunction.AppRoles[0].Id -ResourceId $SecuredFunction.ObjectId -PrincipalId $BlazorWebApp.ObjectId -ObjectId $BlazorWebApp.ObjectId
