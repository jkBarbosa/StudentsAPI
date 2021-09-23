FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY StudentsDemoApp/*.csproj ./StudentsDemoApp/
COPY StudentsDemoApp.Core/*.csproj ./StudentsDemoApp.Core/
COPY Domain/*.csproj ./Domain/
COPY EntityFramework/*.csproj ./EntityFramework/
COPY EFMigrationUtility/*.csproj ./EFMigrationUtility/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY StudentsDemoApp/. ./StudentsDemoApp/
COPY StudentsDemoApp.Core/. ./StudentsDemoApp.Core/
COPY Domain/. ./Domain/ 
COPY EntityFramework/. ./EntityFramework/
COPY EFMigrationUtility/. ./EFMigrationUtility/
#
WORKDIR /app/StudentsDemoApp
RUN dotnet publish -c Release -o out 
#
WORKDIR /app/EFMigrationUtility
RUN dotnet build "EFMigrationUtility.csproj" -c Release -o migration
#
#FROM build AS final
#WORKDIR /migration
#COPY --from=build /app/EFMigrationUtility/migration ./
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /migration
#
COPY --from=build /app/EFMigrationUtility/migration ./
#
WORKDIR /app 
#
COPY --from=build /app/StudentsDemoApp/out ./
ENTRYPOINT ["dotnet", "Api.Rest.dll"]

