FROM mcr.microsoft.com/dotnet/core/runtime:3.1

LABEL version="1.0.0" description="Expense Management"

COPY dist /app

WORKDIR /app

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "ExpenseManagement.dll"]