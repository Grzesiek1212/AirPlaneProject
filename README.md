**AirPlane Simulator and Data Visualization System**
Welcome to the AirPlane project, a comprehensive simulation and data visualization system designed to provide real-time insights into flight data.

**Overview**
The AirPlane project leverages object-oriented principles and design patterns to simulate and visualize flight data. It integrates multiple technologies and libraries, offering a robust framework for data processing, visualization, and simulation.

**Key Features**
Data Processing: Utilizes NetworkSourceSimulator to process and simulate flight data from files (example_data.ftr and example.ftre).

Object-Oriented Design: Implements a modular structure with Myobject entities and various service classes (DataSourceService) for efficient data management.

Visualization: Employs FlightTrackerGUI and Mapsui for interactive and real-time flight path visualization on graphical maps.

Logging and Reporting: Generates detailed logs (log_yyyy-MM-dd.txt) and supports commands (print, report, exit) for snapshots, reports, and application control.

**Technologies Used**
C# Programming: Utilizes .NET framework for backend development.
GUI: Implements visual interfaces for data visualization and user interaction.
Concurrency: Utilizes multi-threading (Thread class) for parallel processing and real-time updates.

**How to Use**
Setup: Clone the repository and ensure all dependencies are installed (NetworkSourceSimulator, FlightTrackerGUI, Mapsui).

Data Simulation: Run Program.Main() to initiate the simulation and data processing from example_data.ftr and example.ftre.

Visualization: Launch the GUI interface to visualize flight paths and real-time updates on geographical maps.

**Commands:**

- Type print to take a snapshot of the current data state.
- Type report to generate a media report based on flight data.
- Type exit to gracefully terminate the application.

  
**Conclusion**
The AirPlane project showcases advanced data processing, visualization, and simulation techniques tailored for aviation data analysis. It serves as a robust platform for both educational purposes and real-world applications in flight data management.

For more information and detailed documentation, please refer to the project repository at GitHub - AirPlane.
