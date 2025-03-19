## Add Migration
| Action | Command |
| --- | --- |
| Add-Migration | EntityFrameworkCore\Add-Migration MigrationName -Context KingPriceContext -Project KingPriceDemo.Persistence -StartupProject KingPriceDemo.WebApi -Verbose -o Data/Migrations |

## Remove Migration
| Action | Command |
| --- | --- |
| Remove-Migration | EntityFrameworkCore\Remove-Migration -Context KingPriceContext -Project KingPriceDemo.Persistence -StartupProject KingPriceDemo.WebApi -Verbose |

## Update Database
| Action | Command |
| --- | --- |
| Update-Database | EntityFrameworkCore\Update-Database -Context KingPriceContext -Project KingPriceDemo.Persistence -StartupProject KingPriceDemo.WebApi -Verbose |