# Notification Application

## Overview

- Manage users and notifications from a console UI
- Support email and SMS notification workflows
- Validate input in the FE layer before persisting data
- Use EF Core repositories over a PostgreSQL database

## Tech Stack

- .NET 10
- Entity Framework Core 8
- PostgreSQL
- Npgsql EF Core provider
- Layered architecture: FE, BAL, DAL, Model

## Solution Structure

- NotificationApp.FEApplication
  - Console menus and application flow
  - Validators for name, email, mobile number, and message
- NotificationApp.BALLibrary
  - Business services for users and notifications
  - Notification sender logic
- NotificationApp.DALLibrary
  - EF Core `DbContext`
  - Repository implementations for users and notifications
  - PostgreSQL configuration and entity mapping
- NotificationApp.ModelLibrary
  - Domain models
  - Notification inheritance model
  - Custom validation exceptions

## Current Features

### User Features

- Create a user
- Get user details by mobile number
- Update user details by mobile number
- Delete user by mobile number

### Notification Features

- Send SMS notification to an existing user
- Send email notification to an existing user
- Get all notifications
- Get SMS notifications only
- Get email notifications only

If a matching user is not found, notification creation is skipped and the application prompts to create the user first.

## Data Access

- EF Core is used for all CRUD operations in the DAL
- `NotificationAppContext` configures the PostgreSQL connection
- `User` and `Notification` entities are mapped with Fluent API
- `EmailNotification` and `SMSNotification` are modeled as derived notification types
- `NotificationType` is used as the discriminator for the hierarchy

## Domain Models

- User
  - UserId, UserName, MobileNumber, EmailId, Notifications
- Notification
  - Id, Message, UsertoNotify, SentDate, NotificationType, User
- EmailNotification
  - Inherits `Notification`
- SMSNotification
  - Inherits `Notification`
- NotiType enum
  - EmailNotification = 1
  - SMSNotification = 2

## Validation Rules

- Email
  - Cannot be empty
  - Must be valid email format
- Mobile number
  - Cannot be empty
  - Must contain only digits
  - Must be exactly 10 digits
- Name
  - Cannot be empty
  - Must contain only letters and spaces
  - Minimum length is 3
- Message
  - Cannot be empty or whitespace
  - Must be between 5 and 160 characters

## Run Instructions

From the solution root:

1. Restore and build
   - `dotnet build NotificationApp.sln`
2. Run the console app
   - `dotnet run --project NotificationApp.FEApplication`

