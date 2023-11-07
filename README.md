# AKWADRAT Interior Design Studio Web App

![App Screenshot](./images/app_screenshot.png)

## Table of Contents

- [Project Goal](#project-goal)
- [Business Use Case](#business-use-case)
- [Controllers](#controllers)
- [Models](#models)
- [Views](#views)
- [Database](#database)
- [Team Responsibilities](#team-responsibilities)

## Project Goal

The project's main goal is to create a web application for potential interior designers, which serves both marketing purposes and as a future database for a small business. The project aims to build a flexible framework that can be further developed and customized according to the client's needs. In the digital minimalism spirit, this website combines two crucial components for business owners.

## Business Use Case

The application targets entrepreneurs with small businesses. It's an ideal solution for individuals or small teams working online, allowing them to manage and access their data in one place, along with a secure storage solution for their data.

## Controllers

Each controller, except the Home controller, was generated based on models using MVC Controller with Views and Entity Framework. The documentation for each controller is included in the .xml file generated with the solution build.

## Models

Four models were created for the project:
- **Client Model**
- **Project Model**
- **Firm Model**
- **ProjectFirm Model**
Connected to the Project model through a one-to-many relationship. To handle many-to-many relationships, the ProjectFirm model was also generated, linking Project and Firm models. The documentation for model creation is included in the .xml file generated with the solution build.

## Views

Views corresponding to the models (Clients, Firms, ProjectFirms, Projects) were automatically generated, following the four fundamental CRUD (Create, Read, Update, Delete) operations. Custom layouts were used as needed.

## Database

A connection to a local database was established by adding a ConnectionString. Migrations were added, and the database was created based on ApplicationDbContext and the models.

## Team Responsibilities

- **ClientsController**: Aleksander Folfas
- **FirmsController**: Aleksander Folfas
- **HomeController**: Aleksander Folfas
- **ProjectFirmsController**: Aleksander Folfas
- **ProjectsController**: Natalia Łyś, Aleksander Folfas, Emilia Sroka
- **ApplicationDbContext**: Aleksander Folfas, Emilia Sroka
- **Migrations**: Aleksander Folfas, Emilia Sroka
- **Views**: Natalia Łyś, Aleksander Folfas, Emilia Sroka
- **Models**: Natalia Łyś, Aleksander Folfas, Emilia Sroka
- **Azure Hosting**: Aleksander Folfas
- **Frontend (Layout, CSS)**: Natalia Łyś
- **Local Database**: Emilia Sroka
- **Documentation**: Natalia Łyś

**Explore the application and manage your interior design projects seamlessly!**
