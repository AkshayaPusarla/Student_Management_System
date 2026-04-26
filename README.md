# Student Management System

A full-stack Student Management System built with React, ASP.NET Core Web API, SQL Server, JWT Authentication, and Azure Functions.

## Features
- User Registration and Login
- Password Reset Functionality
- JWT Authentication and Authorization
- Role-Based Access Control (Admin/User)
- Add / Update / Delete / View Students
- Search Students by Name / Roll Number
- Protected Frontend Routes
- Azure Timer Trigger for Automated Student Retrieval
- Azure HTTP Trigger for Get Student By ID
- Azure Service Bus Trigger for Queue Message Processing

## Tech Stack
- Frontend: React (Vite)
- Backend: ASP.NET Core Web API
- Database: SQL Server
- Authentication: JWT
- Cloud Services: Azure Functions
- Messaging Queue: Azure Service Bus

## Project Structure
- StudentRepository – Backend API
- studentui-react – React Frontend
- StudetAzureFunction – Azure Functions
- StudentRepository.Tests – Unit Tests

## How to Run
1. Configure SQL Server connection string in appsettings.json
2. Run Backend API
3. Run React Frontend using npm run dev
4. Start Azure Functions / Azurite if required

## Future Improvements
- Deploy Full Stack Application to Cloud
- Add Pagination for Student Listing
- Implement Profile Management for Users
- Add Dashboard Analytics / Charts
- Enhance UI/UX with Responsive Design Improvements
