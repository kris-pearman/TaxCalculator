# TaxCalculator

### Requirements
- .NET 8 or above
- Angular 19
- Angular CLI
- Node / Node Package Manager (NPM)
- Visual Studio 2022

### Getting Started
1. **Build the Backend**
   - Open the `TaxCalculator.server` project in Visual Studio.
   - Click **Build** -> **Build Solution**.
2. **Setup the Database**
   - Open the Package Manager Console by navigating to **Tools** -> **NuGet Package Manager** -> **Package Manager Console**.
   - Run the following command:
     ```
     Update-Database
     ```
     - This will create a sqlite database (tax-bands.db) in your appdata/local folder.
3. **Start the Backend**
   - Click the **Run** icon (or press `F5`) in Visual Studio.
4. **Setup the Frontend**
   - Open a terminal or PowerShell window in the `TaxCalculator.client` project folder.
   - Run:
     ```
     npm install
     ```
   - Start the Angular app by running:
     ```
     ng serve
     ```
     *If the Angular CLI is unavailable, use:*
     ```
     npm run start
     ```

### Troubleshooting
- **Database Update Issues**
  - If the `Update-Database` command fails with an error about an ID already in use:
    1. Navigate to the following folder on your system:
       ```
       Users/{your username}/AppData/Local
       ```
    2. Delete any `tax-bands.db` files present.
    3. In the `TaxBandContext.cs` file, ensure the IDs start at `1` instead of `0`.

### Security Issues
- If you encounter security restrictions preventing the database from running, modify `Program.cs` as follows:
  - Locate lines 15 and 16:
    ```csharp
    //builder.Services.AddSingleton<ITaxBandsRepository, InMemoryTaxBandsRepository>();
    builder.Services.AddSingleton<ITaxBandsRepository, TaxBandsDBRepository>();
    ```
  - Swap them to:
    ```csharp
    builder.Services.AddSingleton<ITaxBandsRepository, InMemoryTaxBandsRepository>();
    //builder.Services.AddSingleton<ITaxBandsRepository, TaxBandsDBRepository>();
    ```

### Assumptions
- Tax is evenly divided over each month.
- Pence values follow standard rounding rules, based on the sample data in the brief.
- Edge case handling for tax bands:
  - **Example:**
    - Salary: `1000`
    - Tax Bands:
      - Lower Band: `0` - Upper Band: `1000` -> All `1000` is taxed at this rate.
      - Lower Band: `1000` - Upper Band: `2000` -> None of it is taxed at this rate.

