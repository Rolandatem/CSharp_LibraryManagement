# LibraryManagement (Unit Testing Example)
This project demonstrates unit testing practices on a simple console application designed
for library management. While the application itself is straightforward, the primary focus
was on exploring and applying **Test Driven Development (TDD)** practives to ensure robust
and reliable software development.

## Overview
The LibraryManagement application allows users to manage a collection of books, with core
functionalities such as adding, removing, listing, and searching for books. The project serves
as a practical excercise in implementing and showcasing various unit testing patterns and
practices.

## Features
### Application Features
- **Add Books**: Register new books in the library by title and author.
- **Remove Books**: Delete books from the library using a unique identifier.
- **List All Books**: Display all the books available in the library collection.
- **Search Books**: Find books by their title.

### Testing and Development Highlights
- **Test Driven Development (TDD)**: The development process was guided by test-first principles to create
functions that were well-defined and validated through testing before implementation.
- **Comprehensive Unit Testing with xUnit**:
	- Tests were written extensively using xUnit to validate both synchronous and asynchronous library operations.
- **Mocking with Moq**:
	- Dependencies such as notification services were effectively mocked, isolating the business logic
	  and ensuring accurate test validations with Moq.
- **Advanced Testing Techniques**:
	- **Fixtures and Collection Fixtures**: Employed xUnit's fixtures and collection fixtures to manage
	  shared context and setup across multiple test cases, facilitating organized and efficient test
	  execution.
	- **Parameterized Tests**: Utilized `[InlineData]` and `[ClassData]` attributes to run tests with
	  varying inputs, enhancing test coverage by iterating over multiple datasets seamlessly.
- **In-Memory Database for Testing**:
	- Used Entity Framwork Core's in-memory database feature to simulate database interactions, ensuring
	  isolated and repeatable test environments without affecting a real database.

## Tool and Technologies
- **.NET 9**: The projects included were built using .NET 9, taking advantage of modern features.
- **xUnit**: Leveraged for its comprehensive testing capabilities in the .NET ecosystem, including
  support for theory-based testing.
- **Moq**: Utilized for creating mock objects and stubs in unit testing, aiding in the isolation of
  system components and dependencies.
- **Entity Framework Core**: Applied to abstract data access and facilitate an in-memory database for testing.

## Project Structure
- **LibraryManagementApp**: Contains the core application logic handling library operations (console app).
- **LibraryManagement.Data**: Maintains data models and configuration settings.
- **LibraryManagement.Tests**: Hosts the unit tests, demonstrating the application of xUnit and Moq, along
  with advanced techniques like fixtures, custom traits, and parameterized tests.

## Learning Outcomes
Working on this project has deepened my understanding of effective unit tests underpinned by TDD principles.
Using advanced techniques such as fixtures and parameterized tests has improved my ability to write
comprehensive and maintainable test suites. This disciplined approach emphasizes the importance of building
resilient and well-validated code.