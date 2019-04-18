#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# We are going to be using a Windows container as part of this demo.
# Right now, for ease of infrastructure, we are using the sac-2016 container base image.
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-sac2016 AS build
WORKDIR /app
EXPOSE 80

# Lets do a restore of the NuGet packages, and then build the app in a container.
COPY Augustine.Senators.Web.csproj ./
RUN dotnet restore "Augustine.Senators.Web.csproj"
COPY . .
RUN dotnet build "Augustine.Senators.Web.csproj" -c Release -o /app

# Now publish the resulting app.
FROM build AS publish
RUN dotnet publish "Augustine.Senators.Web.csproj" -c Release -o /app

# Finally, take the built/published application, and drop it into the final container.
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-sac2016 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Augustine.Senators.Web.dll"]