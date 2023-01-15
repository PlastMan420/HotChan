param (
    [string]$migmsg 
)

dotnet ef --startup-project ..\..\HotChan.Api\HotChan.Api.csproj migrations add $migmsg --context HotChanContext